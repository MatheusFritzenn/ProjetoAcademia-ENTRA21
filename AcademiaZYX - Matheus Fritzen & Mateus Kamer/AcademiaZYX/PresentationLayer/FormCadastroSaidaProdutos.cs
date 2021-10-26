using BusinessLogicalLayer;
using Entities;
using Entities.ViewModel;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class FormCadastroSaidaProdutos : Form
    {
        private SaidaProdutosBLL saidaProdutoBLL = new SaidaProdutosBLL();
        private UsuarioBLL usuarioBLL = new UsuarioBLL();
        private ProdutoBLL produtoBLL = new ProdutoBLL();
        private FormaPagamentoBLL formaPagamentoBLL = new FormaPagamentoBLL();

        BindingList<ItemSaida> items = new BindingList<ItemSaida>();

        private int idSelecionado = 0;

        public FormCadastroSaidaProdutos()
        {
            Usuario u = SystemParameters.GetCurrentUsuario();

            InitializeComponent();
            this.Load += FormCadastroSaidaProdutos_Load1;
        }

        private void FormCadastroSaidaProdutos_Load1(object sender, EventArgs e)
        {
            DataResponse<ProdutoQueryViewModel> response = produtoBLL.GetProdutos();
            if (response.Success)
            {
                dgvSaidasProduto.DataSource = response.Data;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
            DataResponse<FormaPagamento> responseFormaPagamento = formaPagamentoBLL.Select();
            if (response.Success)
            {
                cmbFormaPagamento.DataSource = responseFormaPagamento.Data;
                cmbFormaPagamento.ValueMember = "ID";
                cmbFormaPagamento.DisplayMember = "Descricao";
            }
        }

       


        private void btnRegistrarItem_Click_1(object sender, EventArgs e)
        {
            ItemSaida item = new ItemSaida();
            item.IDProduto = idSelecionado;
            item.Quantidade = double.Parse(txtQuantidade.Text);
            items.Add(item);

            comboBox1.DataSource = items;
            comboBox1.DisplayMember = "Nome";
        }

        private void btnInserirSaida_Click_1(object sender, EventArgs e)
        {
            SaidaProduto saida = new SaidaProduto();
            int idFormaPagamentoSelecionada = (int)cmbFormaPagamento.SelectedValue;
            saida.IDFormaPagamento = idFormaPagamentoSelecionada;
            saida.Items = items.ToList();

            Response response = saidaProdutoBLL.Registrar(saida);
            MessageBox.Show(response.Message);
        }

        private void dgvSaidasProduto_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            ProdutoQueryViewModel queryViewModel = (ProdutoQueryViewModel)this.dgvSaidasProduto.Rows[e.RowIndex].DataBoundItem;
                txtProduto.Text = queryViewModel.ID + " - " + queryViewModel.Nome;
                idSelecionado = queryViewModel.ID;
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }
    }
}

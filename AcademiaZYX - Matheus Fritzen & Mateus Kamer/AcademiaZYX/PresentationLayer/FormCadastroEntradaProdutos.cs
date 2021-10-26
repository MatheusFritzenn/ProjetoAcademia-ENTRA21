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
    public partial class FormCadastroEntradaProdutos : Form
    {
        private EntradaProdutosBLL entradaProdutoBLL = new EntradaProdutosBLL();
        private UsuarioBLL usuarioBLL = new UsuarioBLL();
        private ProdutoBLL produtoBLL = new ProdutoBLL();

        BindingList<ItemEntrada> items = new BindingList<ItemEntrada>();

        private int idSelecionado = 0;

        public FormCadastroEntradaProdutos()
        {
            Usuario u = SystemParameters.GetCurrentUsuario();

            InitializeComponent();
            this.Load += FormCadastroEntradaProdutos_Load1;
        }

        private void FormCadastroEntradaProdutos_Load1(object sender, EventArgs e)
        {
            DataResponse<ProdutoQueryViewModel> response = produtoBLL.GetProdutos();
            if (response.Success)
            {
                dgvEntradasProduto.DataSource = response.Data;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }

        private void dgvEntradasProduto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            ProdutoQueryViewModel queryViewModel = (ProdutoQueryViewModel)this.dgvEntradasProduto.Rows[e.RowIndex].DataBoundItem;
            txtProduto.Text = queryViewModel.ID + " - " + queryViewModel.Nome;
            idSelecionado = queryViewModel.ID;
        }

        private void btnRegistrarItem_Click(object sender, EventArgs e)
        {
            ItemEntrada item = new ItemEntrada();
            item.IDProduto = idSelecionado;
            item.Quantidade = double.Parse(txtQuantidade.Text);
            item.Valor= double.Parse(txtValor.Text);
            items.Add(item);

            comboBox1.DataSource = items;
            comboBox1.DisplayMember = "Nome";
        }

        private void btnInserirEntrada_Click(object sender, EventArgs e)
        {
            EntradaProduto entrada = new EntradaProduto();
            entrada.Items = items.ToList();

            Response response = entradaProdutoBLL.Registrar(entrada);
            MessageBox.Show(response.Message);
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
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

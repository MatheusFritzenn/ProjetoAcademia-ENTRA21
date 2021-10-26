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
    public partial class FormCadastroProduto : Form
    {
        private ProdutoBLL produtoBLL = new ProdutoBLL();
        private CategoriaBLL categoriaBLL = new CategoriaBLL();

        public FormCadastroProduto()
        {
            Usuario u = SystemParameters.GetCurrentUsuario();



            InitializeComponent();
            this.Load += FormCadastroProduto_Load;
            this.dgvProdutos.CellDoubleClick += DgvProdutos_CellDoubleClick;
        }

        private void DgvProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            ProdutoQueryViewModel p = (ProdutoQueryViewModel)this.dgvProdutos.Rows[e.RowIndex].DataBoundItem;
            FormEditDeleteProduto frm = new FormEditDeleteProduto(p);
            frm.ShowDialog();

            this.AtualizarGrid();

        }

        private void FormCadastroProduto_Load(object sender, EventArgs e)
        {
            DataResponse<Categoria> response = categoriaBLL.Select();
            if (response.Success)
            {
                cmbCategoria.DataSource = response.Data;
                cmbCategoria.ValueMember = "ID";
                cmbCategoria.DisplayMember = "Nome";
            }
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            DataResponse<ProdutoQueryViewModel> response = produtoBLL.GetProdutos();
            if (response.Success)
            {
                this.dgvProdutos.DataSource = response.Data;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }


        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            Produto p = new Produto();
            p.Nome = txtProduto.Text;
            p.Descricao = txtDescricao.Text;
            p.Valor = double.Parse(txtValor.Text);
            p.Estoque = double.Parse(txtEstoque.Text);
            int idCategoriaSelecionada = (int)cmbCategoria.SelectedValue;
            p.IDCategoria = idCategoriaSelecionada;


            Response response = produtoBLL.Insert(p);
            MessageBox.Show(response.Message);
            AtualizarGrid();
        }

        private void btnAtualizar_Click_1(object sender, EventArgs e)
        {
            AtualizarGrid();

        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void txtEstoque_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }
    }
}

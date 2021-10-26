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
    public partial class FormEditDeleteProduto : Form
    {
        private ProdutoBLL produtoBLL = new ProdutoBLL();

        public FormEditDeleteProduto(ProdutoQueryViewModel produto)
        {
            InitializeComponent();

            this.txtID.Text = produto.ID.ToString();
            this.txtProduto.Text = produto.Nome;
            this.txtDescricao.Text = produto.Descricao;
            this.txtEstoque.Text = produto.Estoque.ToString();
            this.txtValor.Text = produto.Valor.ToString();
        }


        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            Response r = produtoBLL.Update(new Produto()
            {
                ID = int.Parse(txtID.Text),
                Nome = txtProduto.Text,
                Descricao = txtDescricao.Text,
                Estoque = double.Parse(txtEstoque.Text),
                Valor = double.Parse(txtValor.Text)
            });
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            Response r = produtoBLL.Delete(int.Parse(txtID.Text));
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
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
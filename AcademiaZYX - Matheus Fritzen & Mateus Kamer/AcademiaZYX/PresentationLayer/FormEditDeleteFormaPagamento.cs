using BusinessLogicalLayer;
using Entities;
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
    public partial class FormEditDeleteFormaPagamento : Form
    {
        private FormaPagamentoBLL formaPagamentoBLL = new FormaPagamentoBLL();

        public FormEditDeleteFormaPagamento(FormaPagamento formaPagamento)
        {
            InitializeComponent();

            this.txtID.Text = formaPagamento.ID.ToString();
            this.txtFormaPagamento.Text = formaPagamento.Descricao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Response r = formaPagamentoBLL.Update(new FormaPagamento()
            {
                ID = int.Parse(txtID.Text),
                Descricao = txtFormaPagamento.Text
            });
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Response r = formaPagamentoBLL.Delete(int.Parse(txtID.Text));
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }
    }
}

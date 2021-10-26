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
    public partial class FormCadastroFormaPagamento : Form
    {
        private FormaPagamentoBLL formaPagamentoBLL = new FormaPagamentoBLL();


        public FormCadastroFormaPagamento()
        {
            InitializeComponent();
            this.Load += FormCadastroFormaPagamento_Load;
            this.dgvFormaPagamento.CellDoubleClick += DgvFormaPagamento_CellDoubleClick;
        }

        private void DgvFormaPagamento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            FormaPagamento f = (FormaPagamento)this.dgvFormaPagamento.Rows[e.RowIndex].DataBoundItem;
            FormEditDeleteFormaPagamento frm = new FormEditDeleteFormaPagamento(f);
            frm.ShowDialog();

            this.AtualizarGrid();
        }

        private void FormCadastroFormaPagamento_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            DataResponse<FormaPagamento> response = formaPagamentoBLL.Select();
            if (response.Success)
            {
                this.dgvFormaPagamento.DataSource = response.Data;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }


        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            FormaPagamento f = new FormaPagamento();
            f.Descricao = txtFormaPagamento.Text;
            Response response = formaPagamentoBLL.Insert(f);
            MessageBox.Show(response.Message);
            AtualizarGrid();
        }

        private void btnAtualizar_Click_1(object sender, EventArgs e)
        {
            AtualizarGrid();

        }
    }
}

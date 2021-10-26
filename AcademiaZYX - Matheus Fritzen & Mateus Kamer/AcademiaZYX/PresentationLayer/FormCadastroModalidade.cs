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
    public partial class FormCadastroModalidade : Form
    {
        private ModalidadeBLL modalidadeBLL = new ModalidadeBLL();

        public FormCadastroModalidade()
        {
            InitializeComponent();
            this.Load += FormCadastroModalidade_Load;
            this.dgvModalidades.CellDoubleClick += DgvModalidades_CellDoubleClick;
        }

        private void DgvModalidades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            Modalidade m = (Modalidade)this.dgvModalidades.Rows[e.RowIndex].DataBoundItem;
            FormEditDeleteModalidade frm = new FormEditDeleteModalidade(m);
            frm.ShowDialog();

            this.AtualizarGrid();

        }

        private void FormCadastroModalidade_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            DataResponse<Modalidade> response = modalidadeBLL.Select();
            if (response.Success)
            {
                this.dgvModalidades.DataSource = response.Data;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }
       

       
        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            Modalidade m = new Modalidade();
            m.Descricao = txtModalidade.Text;
            Response response = modalidadeBLL.Insert(m);
            MessageBox.Show(response.Message);
            AtualizarGrid();
        }

        private void btnAtualizar_Click_1(object sender, EventArgs e)
        {
            AtualizarGrid();

        }
    }
}

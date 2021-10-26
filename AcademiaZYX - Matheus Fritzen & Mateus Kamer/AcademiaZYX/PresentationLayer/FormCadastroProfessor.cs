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
    public partial class FormCadastroProfessor : Form
    {
        private ProfessorBLL professorBLL = new ProfessorBLL();
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        public FormCadastroProfessor()
        {
            InitializeComponent();
            this.Load += FormCadastroProfessor_Load;
            this.dgvProfessores.CellDoubleClick += DgvProfessores_CellDoubleClick;
        }

        private void DgvProfessores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            ProfessorQueryViewModel p = (ProfessorQueryViewModel)this.dgvProfessores.Rows[e.RowIndex].DataBoundItem;
            FormEditDeleteProfessor frm = new FormEditDeleteProfessor(p);
            frm.ShowDialog();

            this.AtualizarGrid();

        }

        private void FormCadastroProfessor_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            DataResponse<ProfessorQueryViewModel> response = professorBLL.GetProfessores();
            if (response.Success)
            {
                this.dgvProfessores.DataSource = response.Data;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }


        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Professor p = new Professor();
            p.CPF = txtCPF.Text;
            p.Cidade = txtCidade.Text;
            p.Bairro = txtBairro.Text;
            p.Rua = txtRua.Text;
            p.Salario = double.Parse(txtSalario.Text);
            p.RG = txtRG.Text;
            p.Telefone = txtTelefone.Text;
            p.IDUsuario = SystemParameters.GetCurrentUsuario().ID;




            Response response = professorBLL.Insert(p);
            MessageBox.Show(response.Message);
            AtualizarGrid();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarGrid();

        }

        private void txtSalario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void txtCPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }

        }

        private void txtRG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }

        }

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }

        }
    }
}

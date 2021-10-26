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
    public partial class FormEditDeleteProfessor : Form
    {
        private ProfessorBLL professorBLL = new ProfessorBLL();

        public FormEditDeleteProfessor(ProfessorQueryViewModel professor)
        {
            InitializeComponent();

            this.txtID.Text = professor.ID.ToString();
            this.txtBairro.Text = professor.Bairro;
            this.txtCidade.Text = professor.Cidade;
            this.txtRua.Text = professor.Rua;
            this.txtSalario.Text = professor.Salario.ToString();
            this.txtTelefone.Text = professor.Telefone;

        }





        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Response r = professorBLL.Update(new Professor()
            {
                ID = int.Parse(txtID.Text),
                Bairro = txtBairro.Text,
                Cidade = txtCidade.Text,
                Rua = txtRua.Text,
                Salario = double.Parse(txtSalario.Text),
                Telefone = txtTelefone.Text,

            });
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Response r = professorBLL.Delete(int.Parse(txtID.Text));
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

        private void txtSalario_KeyPress(object sender, KeyPressEventArgs e)
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

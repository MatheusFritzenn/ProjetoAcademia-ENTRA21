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
    public partial class FormEditDeleteAluno : Form
    {
        private AlunoBLL alunoBLL = new AlunoBLL();

        public FormEditDeleteAluno(Aluno aluno)
        {
            InitializeComponent();

            this.txtID.Text = aluno.ID.ToString();
            this.txtEmail.Text = aluno.Email;
            this.txtTelefone.Text = aluno.Telefone;
            this.txtTelefone2.Text = aluno.Telefone2;
        }



        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            Response r = alunoBLL.Update(new Aluno()
            {
                ID = int.Parse(txtID.Text),
                Email = txtEmail.Text,
                Telefone = txtTelefone.Text,
                Telefone2 = txtTelefone2.Text,
            });
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            Response r = alunoBLL.Delete(int.Parse(txtID.Text));
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }

        }

        private void txtTelefone2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }

        }
    }  
}

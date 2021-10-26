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
    public partial class FormEditDeleteUsuario : Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        public FormEditDeleteUsuario(Usuario usuario)
        {
            InitializeComponent();

            this.txtID.Text = usuario.ID.ToString();
            this.txtEmail.Text = usuario.Email;
            this.txtUsuario.Text = usuario.Nome;
            this.txtSenha.Text = usuario.Senha;
            this.cmbPapel.Text = usuario.Papel.ToString();
            Form frm = new Form();
            frm.WindowState = FormWindowState.Maximized;
        }

      

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Response r = usuarioBLL.Update(new Usuario()
            {
                ID = int.Parse(txtID.Text),
                Email = txtEmail.Text,
                Senha = txtSenha.Text,
                Nome = txtUsuario.Text,
            }); ;
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Response r = usuarioBLL.Delete(int.Parse(txtID.Text));
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

      
    }
}

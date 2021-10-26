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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();


            this.txtEmail.Text = "admin@gmail.com";
            this.txtSenha.Text = "abc123";
        }
        UsuarioBLL usuarioBLL = new UsuarioBLL();

        private void button1_Click_1(object sender, EventArgs e)
        {

            SingleResponse<Usuario> response = usuarioBLL.Authenticate(txtEmail.Text, txtSenha.Text);

            if (response.Success)
            {
                FormMenu frm = new FormMenu();
                this.Visible = false;
                frm.ShowDialog();
                this.Visible = true;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }
    }
}

using BusinessLogicalLayer;
using Entities;
using Entities.Enum;
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
    public partial class FormCadastroUsuario : Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        public FormCadastroUsuario()
        {
            InitializeComponent();
            this.Load += FormCadastroUsuario_Load;
            this.dgvUsuarios.CellDoubleClick += DgvUsuarios_CellDoubleClick;
        }

        private void DgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            Usuario u = (Usuario)this.dgvUsuarios.Rows[e.RowIndex].DataBoundItem;
            FormEditDeleteUsuario frm = new FormEditDeleteUsuario(u);
            frm.ShowDialog();

            this.AtualizarGrid();

        }

        private void FormCadastroUsuario_Load(object sender, EventArgs e)
        {
            
            foreach (var item in Enum.GetValues(typeof(Papel)))
            {
                cmbPapel.Items.Add(item);
            }

            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            DataResponse<Usuario> response = usuarioBLL.Select();
            if (response.Success)
            {
                this.dgvUsuarios.DataSource = response.Data;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }



        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            Usuario u = new Usuario();
            u.Nome = txtNome.Text;
            u.Email = txtEmail.Text;
            u.Senha = txtSenha.Text;
            u.Atividade = true;
            u.Papel = (Papel)cmbPapel.SelectedIndex;


            Response response = usuarioBLL.Insert(u);
            MessageBox.Show(response.Message);
            AtualizarGrid();
        }

        private void btnAtualizar_Click_1(object sender, EventArgs e)
        {
            AtualizarGrid();

        }
    }
}

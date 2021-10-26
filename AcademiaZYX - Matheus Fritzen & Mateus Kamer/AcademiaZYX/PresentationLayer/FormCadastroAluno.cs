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
    public partial class FormCadastroAluno : Form
    {
        private AlunoBLL alunoBLL = new AlunoBLL();

        public FormCadastroAluno()
        {
            InitializeComponent();
            this.Load += FormCadastroModalidade_Load;
            this.dgvAlunos.CellDoubleClick += DgvAlunos_CellDoubleClick;
        }

        private void DgvAlunos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            Aluno a = (Aluno)this.dgvAlunos.Rows[e.RowIndex].DataBoundItem;
            FormEditDeleteAluno frm = new FormEditDeleteAluno(a);
            frm.ShowDialog();

            this.AtualizarGrid();

        }

        private void FormCadastroModalidade_Load(object sender, EventArgs e)
        {
          
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            DataResponse<Aluno> response = alunoBLL.Select();
            if (response.Success)
            {
                this.dgvAlunos.DataSource = response.Data;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }


        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Aluno a = new Aluno();
            a.Nome = txtNome.Text;
            a.Email = txtEmail.Text;
            a.CPF = txtCPF.Text;
            a.RG = txtRG.Text;
            a.DataMatricula = DateTime.Now;
            a.DataNascimento = dtNascimento.Value;
            a.Telefone = txtTelefone.Text;
            a.Telefone2 = txtTelefone.Text;
            a.Atividade = true;
            Response response = alunoBLL.Insert(a);
            MessageBox.Show(response.Message);
            AtualizarGrid();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarGrid();

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
    }
}

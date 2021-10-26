using BusinessLogicalLayer;
using Entities;
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
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
            Usuario user = SystemParameters.GetCurrentUsuario();
            this.toolStripStatusLabel1.Text = user.Nome;

            switch (user.Papel)
            {
                case Entities.Enum.Papel.GerenciadorEstoque:
                    this.vinculaçãoDoPlanoToolStripMenuItem.Visible = false;
                    this.planoToolStripMenuItem1.Visible = false;
                    this.categoriaToolStripMenuItem1.Visible = false;
                    this.modalidadeToolStripMenuItem1.Visible = false;
                    this.alunoToolStripMenuItem1.Visible = false;
                    this.cadastrosToolStripMenuItem.Visible = false;
                    this.produtoToolStripMenuItem1.Visible = false;
                    this.formaDePagamentoToolStripMenuItem.Visible = false;
                    break;
                case Entities.Enum.Papel.Professor:
                    this.modalidadeToolStripMenuItem1.Visible = false;
                    this.cadastrosToolStripMenuItem.Visible = false;
                    this.produtoToolStripMenuItem1.Visible = false;
                    this.categoriaToolStripMenuItem1.Visible = false;
                    this.planoToolStripMenuItem1.Visible = false;
                    this.estoqueToolStripMenuItem.Visible = false;
                    this.formaDePagamentoToolStripMenuItem.Visible = false;
                    break;
            }

        }

        Usuario usuario = new Usuario();


        private void entradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroEntradaProdutos frm = new FormCadastroEntradaProdutos();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void saídaDeProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroSaidaProdutos frm = new FormCadastroSaidaProdutos();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void usuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroUsuario frm = new FormCadastroUsuario();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void professorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormCadastroProfessor frm = new FormCadastroProfessor();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void formaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroFormaPagamento frm = new FormCadastroFormaPagamento();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void modalidadeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCadastroModalidade frm = new FormCadastroModalidade();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void categoriaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCadastroCategoria frm = new FormCadastroCategoria();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void produtoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCadastroProduto frm = new FormCadastroProduto();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void alunoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCadastroAluno frm = new FormCadastroAluno();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void planoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCadastroPlano frm = new FormCadastroPlano();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void vinculaçãoDoPlanoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroPlanoProfessorAluno frm = new FormCadastroPlanoProfessorAluno();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }
    }
}

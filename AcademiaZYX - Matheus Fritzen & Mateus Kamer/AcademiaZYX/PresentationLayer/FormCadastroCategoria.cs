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
    public partial class FormCadastroCategoria : Form
    {
        private CategoriaBLL categoriaBLL = new CategoriaBLL();

        public FormCadastroCategoria()
        {
            InitializeComponent();
            this.Load += FormCadastroCategoria_Load;
            this.dgvCategorias.CellDoubleClick += DgvModalidades_CellDoubleClick;
        }

        private void DgvModalidades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            Categoria c = (Categoria)this.dgvCategorias.Rows[e.RowIndex].DataBoundItem;
            FormEditDeleteCategoria frm = new FormEditDeleteCategoria(c);
            frm.ShowDialog();

            this.AtualizarGrid();

        }

        private void FormCadastroCategoria_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            DataResponse<Categoria> response = categoriaBLL.Select();
            if (response.Success)
            {
                this.dgvCategorias.DataSource = response.Data;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }



        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            Categoria c = new Categoria();
            c.Nome = txtCategoria.Text;
            Response response = categoriaBLL.Insert(c);
            MessageBox.Show(response.Message);
            AtualizarGrid();
        }

        private void btnAtualizar_Click_1(object sender, EventArgs e)
        {
            AtualizarGrid();

        }
    }
}

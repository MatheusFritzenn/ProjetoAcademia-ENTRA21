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
    public partial class FormEditDeleteCategoria : Form
    {
        private CategoriaBLL categoriaBLL = new CategoriaBLL();

        public FormEditDeleteCategoria(Categoria categoria)
        {
            InitializeComponent();

            this.txtID.Text = categoria.ID.ToString();
            this.txtCategoria.Text = categoria.Nome;
        }


        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            Response r = categoriaBLL.Update(new Categoria()
            {
                ID = int.Parse(txtID.Text),
                Nome = txtCategoria.Text
            });
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            Response r = categoriaBLL.Delete(int.Parse(txtID.Text));
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

      
    }
}

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
    public partial class FormEditDeleteModalidade : Form
    {
        private ModalidadeBLL modalidadeBLL = new ModalidadeBLL();

        public FormEditDeleteModalidade(Modalidade modalidade)
        {
            InitializeComponent();

            this.txtID.Text = modalidade.ID.ToString();
            this.txtModalidade.Text = modalidade.Descricao;
        }



        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            Response r = modalidadeBLL.Update(new Modalidade()
            {
                ID = int.Parse(txtID.Text),
                Descricao = txtModalidade.Text,
            });
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            Response r = modalidadeBLL.Delete(int.Parse(txtID.Text));
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }
    }
}

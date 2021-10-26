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
    public partial class FormEditDeletePlano : Form
    {
        private PlanoBLL planoBLL = new PlanoBLL();

        public FormEditDeletePlano(PlanoQueryViewModel plano)
        {
            InitializeComponent();

            this.txtID.Text = plano.ID.ToString();
            this.txtNome.Text = plano.Nome;
            this.txtValor.Text = plano.Valor.ToString();
            this.txtFrequencia.Text = plano.Frequencia.ToString();
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Response r = planoBLL.Update(new Plano()
            {
                ID = int.Parse(txtID.Text),
                Nome = txtNome.Text,
                Frequencia = int.Parse(txtFrequencia.Text),
                Valor = double.Parse(txtValor.Text)
            });
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Response r = planoBLL.Delete(int.Parse(txtID.Text));
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void txtFrequencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }
    }
}

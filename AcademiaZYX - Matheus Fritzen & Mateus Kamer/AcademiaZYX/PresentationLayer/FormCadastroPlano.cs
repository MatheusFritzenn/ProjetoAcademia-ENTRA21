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
    public partial class FormCadastroPlano : Form
    {
        private PlanoBLL planoBLL = new PlanoBLL();
        private ModalidadeBLL modalidadeBLL = new ModalidadeBLL();

        public FormCadastroPlano()
        {
            Usuario u = SystemParameters.GetCurrentUsuario();
            SingleResponse<Professor> professorUsandoOSistema =
                new ProfessorBLL().GetProfessorByUserId(u.ID);



            InitializeComponent();
            this.Load += FormCadastroPlano_Load;
            this.dgvPlanos.CellDoubleClick += DgvPlanos_CellDoubleClick;
        }

        private void DgvPlanos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            PlanoQueryViewModel p = (PlanoQueryViewModel)this.dgvPlanos.Rows[e.RowIndex].DataBoundItem;
            FormEditDeletePlano frm = new FormEditDeletePlano(p);
            frm.ShowDialog();

            this.AtualizarGrid();

        }

        private void FormCadastroPlano_Load(object sender, EventArgs e)
        {
            DataResponse<Modalidade> response = modalidadeBLL.Select();
            if (response.Success)
            {
                cmbModalidade.DataSource = response.Data;
                cmbModalidade.ValueMember = "ID";
                cmbModalidade.DisplayMember = "Descricao";
            }
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            DataResponse<PlanoQueryViewModel> response = planoBLL.GetPlanos();
            if (response.Success)
            {
                this.dgvPlanos.DataSource = response.Data;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }


        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Plano p = new Plano();
            p.Nome = txtPlano.Text;
            p.Frequencia = int.Parse(txtFrequencia.Text);
            p.Valor = double.Parse(txtValor.Text);
            int idModalidadeSelecionada = (int)cmbModalidade.SelectedValue;
            p.IDModalidade = idModalidadeSelecionada;


            Response response = planoBLL.Insert(p);
            MessageBox.Show(response.Message);
            AtualizarGrid();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarGrid();

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

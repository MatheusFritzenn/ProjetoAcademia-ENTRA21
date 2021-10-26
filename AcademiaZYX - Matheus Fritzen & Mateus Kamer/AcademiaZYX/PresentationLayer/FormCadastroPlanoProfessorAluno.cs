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
    public partial class FormCadastroPlanoProfessorAluno : Form
    {
        private PlanoProfessorAlunoBLL planoProfessorAlunoBLL = new PlanoProfessorAlunoBLL();
        private PlanoBLL planoBLL = new PlanoBLL();
        private AlunoBLL alunoBLL = new AlunoBLL();
        private ProfessorBLL professorBLL = new ProfessorBLL();
        public FormCadastroPlanoProfessorAluno()
        {
            InitializeComponent();
            this.Load += FormCadastroPlanoProfessorAluno_Load;
            this.dgvPlanos.CellDoubleClick += DgvPlanoProfessorAluno_CellDoubleClick;
        }

        private void DgvPlanoProfessorAluno_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            PlanoProfessorAlunoQueryViewModel p = (PlanoProfessorAlunoQueryViewModel)this.dgvPlanos.Rows[e.RowIndex].DataBoundItem;
            FormEditDeletePlanoProfessorAluno frm = new FormEditDeletePlanoProfessorAluno(p);
            frm.ShowDialog();

            this.AtualizarGrid();

        }

        private void FormCadastroPlanoProfessorAluno_Load(object sender, EventArgs e)
        {
            DataResponse<Plano> responsePlano = planoBLL.Select();
            if (responsePlano.Success)
            {
                cmbPlano.DataSource = responsePlano.Data;
                cmbPlano.ValueMember = "ID";
                cmbPlano.DisplayMember = "Nome";
            }
            DataResponse<Aluno> responseAluno = alunoBLL.Select();
            if (responseAluno.Success)
            {
                cmbAluno.DataSource = responseAluno.Data;
                cmbAluno.ValueMember = "ID";
                cmbAluno.DisplayMember = "Nome";
            }
           
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            DataResponse<PlanoProfessorAlunoQueryViewModel> response = planoProfessorAlunoBLL.GetPlanos();
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
            PlanoProfessorAluno p = new PlanoProfessorAluno();
            int idPlanoSelecionado = (int)cmbPlano.SelectedValue;
            p.IDPlano = idPlanoSelecionado;
            int idAlunoSelecionado = (int)cmbAluno.SelectedValue;
            p.IDAluno = idAlunoSelecionado;

            Response response = planoProfessorAlunoBLL.Insert(p);
            MessageBox.Show(response.Message);
            AtualizarGrid();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarGrid();
        }
    }
}

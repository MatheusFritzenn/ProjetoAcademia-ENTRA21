using BusinessLogicalLayer;
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
    public partial class FormEditDeletePlanoProfessorAluno : Form
    {
        private PlanoProfessorAlunoBLL planoProfessorAlunoBLL = new PlanoProfessorAlunoBLL();

        public FormEditDeletePlanoProfessorAluno(PlanoProfessorAlunoQueryViewModel plano)
        {
            InitializeComponent();

            this.txtID.Text = plano.ID.ToString();
        }


        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            Response r = planoProfessorAlunoBLL.Delete(int.Parse(txtID.Text));
            MessageBox.Show(r.Message);
            if (r.Success)
            {
                this.Close();
            }
        }
    }
}

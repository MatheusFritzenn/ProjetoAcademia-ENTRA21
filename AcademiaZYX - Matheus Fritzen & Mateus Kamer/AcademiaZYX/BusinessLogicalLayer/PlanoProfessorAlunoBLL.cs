using DataAccessLayer;
using Entities;
using Entities.Interfaces;
using Entities.ViewModel;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class PlanoProfessorAlunoBLL : BaseValidator<PlanoProfessorAluno>, IPlanoProfessorAluno
    {
        private PlanoProfessorAlunoDAL planoProfAlunoDAL = new PlanoProfessorAlunoDAL();
        private PlanoBLL planoBLL = new PlanoBLL();
        private ProfessorBLL professorBLL = new ProfessorBLL();

        public Response Delete(int id)
        {
            return planoProfAlunoDAL.Delete(id);
        }

        public Response Insert(PlanoProfessorAluno p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            SingleResponse<Professor> professorResponse = professorBLL.GetProfessorByUserId(SystemParameters.GetCurrentUsuario().ID);

            if (!professorResponse.Success)
            {
                professorResponse.Message = "Apenas professores podem vincular planos a alunos.";

                return professorResponse;
            }

            p.IDProfessor = professorResponse.Item.ID;


            response = planoProfAlunoDAL.Insert(p);
            if (!response.Success)
            {
                return response;
            }

            SingleResponse<Plano> singleResponse = planoBLL.GetPlanoById(p.IDPlano);
            if (!singleResponse.Success)
            {
                return singleResponse;
            }
            double comissao = singleResponse.Item.Valor * 0.1;
            var resposta = professorBLL.UpdateComissao(p.IDProfessor, comissao);
            if (!resposta.Success)
            {
                return resposta;
            }
            Response r = new Response();
            r.Success = true;
            r.Message = "Plano vinculado com sucesso!";
            return r;
        }

        public DataResponse<PlanoProfessorAluno> Select()
        {
            return planoProfAlunoDAL.Select();
        }

       
        public DataResponse<PlanoProfessorAlunoQueryViewModel> GetPlanos()
        {
            return planoProfAlunoDAL.GetPlanos();
        }
       

        public SingleResponse<PlanoProfessorAluno> GetPlanoProfessorAlunoById(int id)
        {
            return planoProfAlunoDAL.GetPlanoProfessorAlunoById(id);

        }
    }
}

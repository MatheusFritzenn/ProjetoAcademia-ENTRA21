using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IPlanoProfessorAluno
    {
        Response Insert(PlanoProfessorAluno p);
        Response Delete(int id);
        DataResponse<PlanoProfessorAluno> Select();
        SingleResponse<PlanoProfessorAluno> GetPlanoProfessorAlunoById(int id);

    }
}
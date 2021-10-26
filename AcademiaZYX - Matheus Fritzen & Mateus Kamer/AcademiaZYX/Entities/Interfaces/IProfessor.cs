using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IProfessor
    {
        Response Insert(Professor p);
        Response Update(Professor p);
        Response Delete(int id);
        DataResponse<Professor> Select();
        Response UpdateComissao(int id, double valor);
        SingleResponse<Professor> GetProfessorByUserId(int id);

    }
}

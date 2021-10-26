using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IAluno
    {
        Response Insert(Aluno a);
        Response Update(Aluno a);
        Response Delete(int id);
        DataResponse<Aluno> Select();

    }
}

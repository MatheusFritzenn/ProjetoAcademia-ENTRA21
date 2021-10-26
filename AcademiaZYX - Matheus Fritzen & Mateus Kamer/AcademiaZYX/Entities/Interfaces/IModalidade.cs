using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IModalidade
    {
        Response Insert(Modalidade m);
        Response Update(Modalidade m);
        Response Delete(int id);
        DataResponse<Modalidade> Select();

    }
}

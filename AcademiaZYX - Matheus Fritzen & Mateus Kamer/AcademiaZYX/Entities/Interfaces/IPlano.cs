using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IPlano
    {
        Response Insert(Plano p);
        Response Update(Plano p);
        Response Delete(int id);
        DataResponse<Plano> Select();
        SingleResponse<Plano> GetPlanoById(int id);

    }
}

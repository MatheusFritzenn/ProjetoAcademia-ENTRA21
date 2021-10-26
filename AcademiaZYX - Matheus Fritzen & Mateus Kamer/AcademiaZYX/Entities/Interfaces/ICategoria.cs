using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface ICategoria
    {
        Response Insert(Categoria c);
        Response Update(Categoria c);
        Response Delete(int id);

        DataResponse<Categoria> Select();
    }
}

using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IUsuario
    {
        Response Insert(Usuario u);
        Response Update(Usuario u);
        Response Delete(int id);
        DataResponse<Usuario> Select();
        SingleResponse<Usuario> GetUsuarioById(int id);

        SingleResponse<Usuario> Authenticate(string email, string senha);
    }
}

using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public static class SystemParameters
    {
        private static Usuario _usuario;

        public static void Authenticate(Usuario usuario)
        {
            _usuario = usuario;
        }

        public static void Logout()
        {
            _usuario = null;
        }

        public static Usuario GetCurrentUsuario()
        {
            return _usuario;
        }
    }
}

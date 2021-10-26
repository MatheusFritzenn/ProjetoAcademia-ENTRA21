using DataAccessLayer;
using Entities;
using Entities.Enum;
using Entities.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class UsuarioBLL : BaseValidator<Usuario>, IUsuario
    {
        private UsuarioDAL usuarioDAL = new UsuarioDAL();

        public override Response Validate(Usuario item)
        {
            //NOME
            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                this.AddError("Nome deve ser informado.");
            }
            else
            {
                item.Nome = Normatization.NormatizeString(item.Nome);
                if (item.Nome.Length < 3 || item.Nome.Length > 30)
                {
                    this.AddError("Nome deve conter entre 3 e 30 caracteres.");
                }
            }

            //EMAIL
            if (string.IsNullOrWhiteSpace(item.Email))
            {
                this.AddError("Email deve ser informado!");
            }
            if (item.Email.Length < 8 || item.Email.Length > 100)
            {
                this.AddError("Email inválido.");
            }

            string regexEmail = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool isValidEmail = Regex.IsMatch(item.Email, regexEmail);
            if (!isValidEmail)
            {
                this.AddError("Email inválido.");
            }
            return base.Validate(item);
        }

        public SingleResponse<Usuario> Authenticate(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                this.AddError("Email deve ser informado.");
            }
            if (string.IsNullOrWhiteSpace(senha))
            {
                this.AddError("Senha deve ser informada.");
            }
           

            Response response = base.Validate(null);
            if (response.Success)
            {
                SingleResponse<Usuario> responseUsuario = usuarioDAL.Authenticate(email, senha);
                if (responseUsuario.Success)
                {
                    SystemParameters.Authenticate(responseUsuario.Item);
                }
                return responseUsuario;
            }
            return new SingleResponse<Usuario>()
            {
                Message = response.Message,
                Success = false
            };
        }

        public Response Insert(Usuario u)
        {
            Response response = this.Validate(u);
            if (!response.Success)
            {
                return response;
            }

            return usuarioDAL.Insert(u);
        }

        public Response Update(Usuario u)
        {
            Response response = this.Validate(u);
            if (!response.Success)
            {
                return response;
            }

            return usuarioDAL.Update(u);
        }

        public Response Delete(int id)
        {
            return usuarioDAL.Delete(id);

        }

        public DataResponse<Usuario> Select()
        {
            return usuarioDAL.Select();
        }

       

        public SingleResponse<Usuario> GetUsuarioById(int id)
        {
            return usuarioDAL.GetUsuarioById(id);
        }
    }
}

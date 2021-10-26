using DataAccessLayer;
using Entities;
using Entities.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class CategoriaBLL : BaseValidator<Categoria>, ICategoria
    {
        private CategoriaDAL categoriaDAL = new CategoriaDAL();

        public override Response Validate(Categoria item)
        {
            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                this.AddError("Categoria deve ser informada.");
            }
            else
            {
                item.Nome = Normatization.NormatizeString(item.Nome);
                if (item.Nome.Length < 3 || item.Nome.Length > 30)
                {
                    this.AddError("Categoria deve conter entre 3 e 30 caracteres.");
                }
            }

            return base.Validate(item);
        }

        public Response Delete(int id)
        {
            return categoriaDAL.Delete(id);
        }

        public Response Insert(Categoria c)
        {
            Response response = this.Validate(c);
            if (!response.Success)
            {
                return response;
            }

            return categoriaDAL.Insert(c);
        }

        public DataResponse<Categoria> Select()
        {
            return categoriaDAL.Select();
        }

        public Response Update(Categoria c)
        {
            Response response = this.Validate(c);
            if (!response.Success)
            {
                return response;
            }

            return categoriaDAL.Update(c);
        }
      
    }
}

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
    public class ModalidadeBLL : BaseValidator<Modalidade>, IModalidade
    {
        private ModalidadeDAL modalidadeDAL = new ModalidadeDAL();

        public override Response Validate(Modalidade item)
        {
            if (string.IsNullOrWhiteSpace(item.Descricao))
            {
                this.AddError("Modalidade deve ser informada.");
            }
            else
            {
                item.Descricao = Normatization.NormatizeString(item.Descricao);
                if (item.Descricao.Length < 3 || item.Descricao.Length > 50)
                {
                    this.AddError("Modalidade deve conter entre 3 e 50 caracteres.");
                }
            }


            return base.Validate(item);
        }

        public Response Insert(Modalidade m)
        {
            Response response = this.Validate(m);
            if (!response.Success)
            {
                return response;
            }

            return modalidadeDAL.Insert(m);
        }

        public Response Update(Modalidade m)
        {
            Response response = this.Validate(m);
            if (!response.Success)
            {
                return response;
            }

            return modalidadeDAL.Update(m);
        }

        public Response Delete(int id)
        {
            return modalidadeDAL.Delete(id);
        }

        public DataResponse<Modalidade> Select()
        {
            return modalidadeDAL.Select();
        }
       
    }
}

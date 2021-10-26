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
    public class FormaPagamentoBLL : BaseValidator<FormaPagamento>, IFormaPagamento
    {
        private FormaPagamentoDAL formaPagamentoDAL = new FormaPagamentoDAL();

        public override Response Validate(FormaPagamento item)
        {
            if (string.IsNullOrWhiteSpace(item.Descricao))
            {
                this.AddError("Gênero deve ser informado");
            }
            else
            {
                item.Descricao = Normatization.NormatizeString(item.Descricao);
                if (item.Descricao.Length < 3 || item.Descricao.Length > 30)
                {
                    this.AddError("Gênero deve conter entre 3 e 30 caracteres");
                }
            }


            return base.Validate(item);
        }

        public Response Delete(int id)
        {
            return formaPagamentoDAL.Delete(id);
        }

        public Response Insert(FormaPagamento f)
        {
            Response response = this.Validate(f);
            if (!response.Success)
            {
                return response;
            }

            return formaPagamentoDAL.Insert(f);
        }

        public DataResponse<FormaPagamento> Select()
        {
            return formaPagamentoDAL.Select();
        }

        public Response Update(FormaPagamento f)
        {
            Response response = this.Validate(f);
            if (!response.Success)
            {
                return response;
            }

            return formaPagamentoDAL.Update(f);
        }
    }
}

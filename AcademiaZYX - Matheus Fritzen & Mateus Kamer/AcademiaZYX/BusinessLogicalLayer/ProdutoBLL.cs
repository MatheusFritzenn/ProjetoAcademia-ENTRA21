using DataAccessLayer;
using Entities;
using Entities.Interfaces;
using Entities.ViewModel;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class ProdutoBLL : BaseValidator<Produto>, IProduto
    {
        private ProdutoDAL produtoDAL = new ProdutoDAL();

        public override Response Validate(Produto item)
        {
            if (string.IsNullOrWhiteSpace(item.Descricao))
            {
                this.AddError("Produto deve ser informado.");
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

        public Response Delete(int id)
        {
            return produtoDAL.Delete(id);
        }

        public Response Insert(Produto p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            return produtoDAL.Insert(p);
        }

        public DataResponse<Produto> Select()
        {
            return produtoDAL.Select();
        }

        public Response Update(Produto p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            return produtoDAL.Update(p);
        }
        public DataResponse<ProdutoQueryViewModel> GetProdutos()
        {
            return produtoDAL.GetProdutos();
        }

        public SingleResponse<Produto> GetProdutoById(int id)
        {
            return produtoDAL.GetProdutoById(id);
        }

        public Response UpdateEstoque(int id, double valor)
        {
            return produtoDAL.UpdateEstoque(id, valor);
        }

        public Response UpdatePreco(int id, double valor)
        {
            return produtoDAL.UpdatePreco(id, valor);
        }
    }
}

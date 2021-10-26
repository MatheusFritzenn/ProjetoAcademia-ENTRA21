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
using System.Transactions;

namespace BusinessLogicalLayer
{
    public class EntradaProdutosBLL : BaseValidator<EntradaProduto>, IEntradaProdutos
    {
        private EntradaProdutosDAL entradaProdutosDAL = new EntradaProdutosDAL();
        private ProdutoBLL produtoBLL = new ProdutoBLL();

        public override Response Validate(EntradaProduto item)
        {
            foreach (ItemEntrada ie in item.Items)
            {
                if (ie.Valor <= 0 || ie.Quantidade <= 0)
                {
                    this.AddError("Valor e/ou quantidade não pode ser menor ou igual a ZERO.");
                }
            }

            return base.Validate(item);
        }

        public Response Registrar(EntradaProduto e)
        {
            e.Data = DateTime.Now;
            e.IDUsuario = SystemParameters.GetCurrentUsuario().ID;
            e.CalcularPreco();

            Response response = this.Validate(e);
            if (!response.Success)
            {
                return response;
            }

            using (TransactionScope scope = new TransactionScope())
            {
                Response responseRegistroEntrada = entradaProdutosDAL.Registrar(e);

                if (!responseRegistroEntrada.Success)
                {
                    return responseRegistroEntrada;
                }
                foreach (ItemEntrada item in e.Items)
                {
                    item.ID = e.ID;
                    Response responseCadastroItem = entradaProdutosDAL.RegistrarItem(item);
                    if (!response.Success)
                    {
                        return response;
                    }
                    SingleResponse<Produto> responseProduto = produtoBLL.GetProdutoById(item.IDProduto);
                    if (!responseProduto.Success)
                    {
                        return responseProduto;
                    }
                    Produto produto = responseProduto.Item;
                    double novoEstoque = item.Quantidade + produto.Estoque;
                    double novoPreco = ((item.Quantidade * item.Valor) + (produto.Valor * produto.Estoque)) / (item.Quantidade + produto.Estoque);

                    produtoBLL.UpdateEstoque(produto.ID, novoEstoque);
                    produtoBLL.UpdatePreco(produto.ID, novoPreco);
                }
                scope.Complete();
            }

            Response r = new Response();
            r.Success = true;
            r.Message = "Itens registrados com sucesso.";
            return r;
        }
    }
}

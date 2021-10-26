using DataAccessLayer;
using Entities;
using Entities.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogicalLayer
{
    public class SaidaProdutosBLL : BaseValidator<SaidaProduto>, ISaidaProdutos
    {
        private SaidaProdutosDAL saidaProdutosDAL = new SaidaProdutosDAL();
        private ProdutoBLL produtoBLL = new ProdutoBLL();

        public override Response Validate(SaidaProduto item)
        {
            foreach (ItemSaida itemSaida in item.Items)
            {
                if (itemSaida.Quantidade <= 0)
                {
                    this.AddError("Quantidade não pode ser menor ou igual a ZERO.");
                }
              
            }

            return base.Validate(item);
        }

        public Response Registrar(SaidaProduto s)
        {
            s.Data = DateTime.Now;
            s.IDUsuario = SystemParameters.GetCurrentUsuario().ID;
            

            Response response = this.Validate(s);
            if (!response.Success)
            {
                return response;
            }

            

            using (TransactionScope scope = new TransactionScope())
            {
                Response responseRegistroSaida = saidaProdutosDAL.Registrar(s);

                if (!responseRegistroSaida.Success)
                {
                    return responseRegistroSaida;
                }
                foreach (ItemSaida item in s.Items)
                {
                    item.ID = s.ID;
                    Response responseCadastroItem = saidaProdutosDAL.RegistrarItem(item);
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
                    if (produto.Estoque < item.Quantidade)
                    {
                        responseProduto.Success = false;
                        responseProduto.Message = "Número de produtos indisponível";
                        return responseProduto;
                    }

                         double novoEstoque = produto.Estoque - item.Quantidade;
                    

                    produtoBLL.UpdateEstoque(produto.ID, novoEstoque);
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

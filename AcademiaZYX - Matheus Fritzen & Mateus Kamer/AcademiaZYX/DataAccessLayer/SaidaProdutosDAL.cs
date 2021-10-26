using Entities;
using Entities.Interfaces;
using Entities.ViewModel;
using Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SaidaProdutosDAL : ISaidaProdutos
    {
        public DataResponse<SaidaProdutoQueryViewModel> GetEntradas()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT S.ID,
                                           S.DATA,
                                           U.ID,
                                           F.DESCRICAO,
                                    FROM SAIDAS_PRODUTO S INNER JOIN
                                         USUARIOS U ON
                                    S.USUARIOID = U.NOME INNER JOIN
                                         FORMAS_PAGAMENTO F ON
                                      S.FORMA_PAGAMENTO = F.DESCRICAO";
            DataResponse<SaidaProdutoQueryViewModel> resposta = new DataResponse<SaidaProdutoQueryViewModel>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<SaidaProdutoQueryViewModel> saidas = new List<SaidaProdutoQueryViewModel>();
                while (reader.Read())
                {
                    SaidaProdutoQueryViewModel s = new SaidaProdutoQueryViewModel();
                    s.ID = Convert.ToInt32(reader["ID"]);
                    s.Data = Convert.ToDateTime(reader["DATA"]);
                    s.Usuario = Convert.ToString(reader["NOME"]);
                    s.FormaPagamento = Convert.ToString(reader["DESCRICAO"]);

                    saidas.Add(s);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = saidas;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<SaidaProdutoQueryViewModel>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Registrar(SaidaProduto s)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO SAIDAS_PRODUTO (DATA,USUARIOID,FORMA_PAGAMENTO) VALUES (@DATA,@USUARIOID,@FORMA_PAGAMENTO); select scope_identity()";
            command.Parameters.AddWithValue("@DATA", s.Data);
            command.Parameters.AddWithValue("@USUARIOID", s.IDUsuario);
            command.Parameters.AddWithValue("@FORMA_PAGAMENTO", s.IDFormaPagamento);


            Response response = new Response();
            try
            {
                connection.Open();
                s.ID = Convert.ToInt32(command.ExecuteScalar());
                response.Success = true; response.Message = "Saída de produtos cadastrada com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                if (ex.Message.Contains("UQ__SAIDA__PROD"))
                {
                    response.Message = "Saída de produtos já cadastrada!";
                    return response;
                }
                response.Message = "Erro no banco de dados, contate o administrador";
                return response;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response RegistrarItem(ItemSaida s)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;


            command.CommandText = "INSERT INTO ITENS_SAIDA (IDSAIDA,IDPRODUTO,QTD) VALUES  (@IDSAIDA,@IDPRODUTO,@QTD)";
            command.Parameters.AddWithValue("@IDSAIDA", s.ID);
            command.Parameters.AddWithValue("@IDPRODUTO", s.IDProduto);
            command.Parameters.AddWithValue("@QTD", s.Quantidade);


            Response response = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                response.Success = true; response.Message = "Entrada de produtos cadastrada com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                if (ex.Message.Contains("UQ__SAIDA"))
                {
                    response.Message = "Entrada de produtos já cadastrada!";
                    return response;
                }
                response.Message = "Erro no banco de dados, contate o administrador";
                return response;
            }
            finally
            {
                connection.Dispose();
            }
        }
    }
}

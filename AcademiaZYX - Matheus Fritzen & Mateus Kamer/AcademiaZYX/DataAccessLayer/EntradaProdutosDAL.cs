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
    public class EntradaProdutosDAL : IEntradaProdutos
    {
        public DataResponse<EntradaProdutoQueryViewModel> GetEntradas()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT E.ID,
                                           E.DATAENTRADA,
                                           E.VALOR,
                                           U.ID,
                                    FROM ENTRADAS_PRODUTO E INNER JOIN
                                         USUARIOS U ON
                                    E.USUARIOID = U.NOME";

            DataResponse<EntradaProdutoQueryViewModel> resposta = new DataResponse<EntradaProdutoQueryViewModel>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<EntradaProdutoQueryViewModel> entradas = new List<EntradaProdutoQueryViewModel>();
                while (reader.Read())
                {
                    EntradaProdutoQueryViewModel e = new EntradaProdutoQueryViewModel();
                    e.ID = Convert.ToInt32(reader["ID"]);
                    e.Data = Convert.ToDateTime(reader["DATAENTRADA"]);
                    e.Valor = Convert.ToDouble(reader["VALOR"]);
                    e.Usuario = Convert.ToString(reader["NOME"]);

                    entradas.Add(e);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = entradas;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<EntradaProdutoQueryViewModel>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Registrar(EntradaProduto e)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO ENTRADAS_PRODUTO (DATAENTRADA,VALOR,USUARIOID) VALUES (@DATAENTRADA,@VALOR,@USUARIOID); select scope_identity()";
            command.Parameters.AddWithValue("@DATAENTRADA", e.Data);
            command.Parameters.AddWithValue("@VALOR", e.Valor);
            command.Parameters.AddWithValue("@USUARIOID", e.IDUsuario);


            Response response = new Response();
            try
            {
                connection.Open();
                e.ID = Convert.ToInt32(command.ExecuteScalar());
                response.Success = true; response.Message = "Entrada de produtos cadastrada com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                if (ex.Message.Contains("UQ__ENTRADA"))
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

        public Response RegistrarItem(ItemEntrada e)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;


            command.CommandText = "INSERT INTO ITENS_ENTRADA (IDENTRADA,IDPRODUTO,VALOR,QTD) VALUES  (@IDENTRADA,@IDPRODUTO,@VALOR,@QTD)";
            command.Parameters.AddWithValue("@IDENTRADA", e.ID);
            command.Parameters.AddWithValue("@IDPRODUTO", e.IDProduto);
            command.Parameters.AddWithValue("@QTD", e.Quantidade);
            command.Parameters.AddWithValue("@VALOR", e.Valor);


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
                if (ex.Message.Contains("UQ__ENTRADA"))
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

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
    public class ProdutoDAL : IProduto
    {
        public Response UpdateEstoque(int id, double valor)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE PRODUTOS SET ESTOQUE = @ESTOQUE WHERE ID = @ID";
            command.Parameters.AddWithValue("@ESTOQUE", valor);
            command.Parameters.AddWithValue("@ID", id);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Produto editado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__PRODUTOS"))
                {
                    resposta.Message = "Produto já cadastrado!";
                    return resposta;
                }

                resposta.Message = "Erro no banco de dados, contate o administrador.";
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response UpdatePreco(int id, double valor)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE PRODUTOS SET VALOR = @VALOR WHERE ID = @ID";
            command.Parameters.AddWithValue("@VALOR", valor);
            command.Parameters.AddWithValue("@ID", id);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Produto editado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__PRODUTOS"))
                {
                    resposta.Message = "Produto já cadastrado!";
                    return resposta;
                }

                resposta.Message = "Erro no banco de dados, contate o administrador.";
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }
        public Response Delete(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM PRODUTOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Produto excluído com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                if (ex.Message.Contains("FK__PRODUTO"))
                {
                    resposta.Message = "Produto não pode ser excluído, pois existem vendas vinculados a ela!";
                    return resposta;
                }

                resposta.Message = "Erro no banco de dados, contate o administrador.";
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Insert(Produto p)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO PRODUTOS (NOME,DESCRICAO,VALOR,ESTOQUE,CATEGORIA) VALUES (@NOME,@DESCRICAO,@VALOR,@ESTOQUE,@CATEGORIA)";
            command.Parameters.AddWithValue("@NOME", p.Nome);
            command.Parameters.AddWithValue("@DESCRICAO", p.Descricao);
            command.Parameters.AddWithValue("@ESTOQUE", p.Estoque);
            command.Parameters.AddWithValue("@VALOR", p.Valor);
            command.Parameters.AddWithValue("@CATEGORIA", p.IDCategoria);



            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Produto cadastrado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__PRODUTOS"))
                {
                    resposta.Message = "Produto já cadastrado!";
                    return resposta;
                }

                resposta.Message = "Erro no banco de dados, contate o administrador.";
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public DataResponse<Produto> Select()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM PRODUTOS ORDER BY ID";

            DataResponse<Produto> resposta = new DataResponse<Produto>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Produto> produtos = new List<Produto>();
                while (reader.Read())
                {
                    Produto produto = new Produto();

                    produto.ID = Convert.ToInt32(reader["ID"]);
                    produto.Nome = Convert.ToString(reader["NOME"]);
                    produto.Descricao = Convert.ToString(reader["DESCRICAO"]);
                    produto.Valor = Convert.ToDouble(reader["VALOR"]);
                    produto.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    produto.Descricao = Convert.ToString(reader["DESCRICAO"]);
                    produtos.Add(produto);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = produtos;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Produto>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public SingleResponse<Produto> GetProdutoById(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM PRODUTOS where id = @id";
            command.Parameters.AddWithValue("@ID", id);

            SingleResponse<Produto> resposta = new SingleResponse<Produto>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Produto produto = new Produto();

                    produto.ID = Convert.ToInt32(reader["ID"]);
                    produto.Nome = Convert.ToString(reader["NOME"]);
                    produto.Descricao = Convert.ToString(reader["DESCRICAO"]);
                    produto.Valor = Convert.ToDouble(reader["VALOR"]);
                    produto.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    produto.Descricao = Convert.ToString(reader["DESCRICAO"]);
                    produto.Estoque = Convert.ToDouble(reader["ESTOQUE"]);
                    resposta.Success = true;
                    resposta.Message = "Dados selecionados com sucesso!";
                    resposta.Item = produto;
                }
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }

        }

        public Response Update(Produto p)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE PRODUTOS SET NOME = @NOME, DESCRICAO = @DESCRICAO, ESTOQUE = @ESTOQUE, VALOR = @VALOR WHERE ID = @ID";
            command.Parameters.AddWithValue("@NOME", p.Nome);
            command.Parameters.AddWithValue("@ID", p.ID);
            command.Parameters.AddWithValue("@DESCRICAO", p.Descricao);
            command.Parameters.AddWithValue("@ESTOQUE", p.Estoque);
            command.Parameters.AddWithValue("@VALOR", p.Valor);


            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Produto editado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__PRODUTOS"))
                {
                    resposta.Message = "Produto já cadastrado!";
                    return resposta;
                }

                resposta.Message = "Erro no banco de dados, contate o administrador.";
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }
        public DataResponse<ProdutoQueryViewModel> GetProdutos()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT P.ID,
                                           P.NOME,
                                           C.NOME 'Categoria',
                                           P.VALOR,
                                           P.ESTOQUE,
                                           P.DESCRICAO
                                    FROM PRODUTOS P INNER JOIN
                                         CATEGORIAS C ON
                                    P.CATEGORIA = C.ID";

            DataResponse<ProdutoQueryViewModel> resposta = new DataResponse<ProdutoQueryViewModel>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<ProdutoQueryViewModel> produtos = new List<ProdutoQueryViewModel>();
                while (reader.Read())
                {
                    //Cada Loop deste while, faz com que o objeto "reader" aponte para um registro retornado pelo select
                    ProdutoQueryViewModel p = new ProdutoQueryViewModel();
                    p.ID = Convert.ToInt32(reader["ID"]);
                    p.Nome = Convert.ToString(reader["NOME"]);
                    p.Categoria = Convert.ToString(reader["Categoria"]);
                    p.Estoque = Convert.ToDouble(reader["ESTOQUE"]);
                    p.Valor = Convert.ToDouble(reader["VALOR"]);
                    p.Descricao = Convert.ToString(reader["DESCRICAO"]);

                    produtos.Add(p);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = produtos;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<ProdutoQueryViewModel>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }
    }



}

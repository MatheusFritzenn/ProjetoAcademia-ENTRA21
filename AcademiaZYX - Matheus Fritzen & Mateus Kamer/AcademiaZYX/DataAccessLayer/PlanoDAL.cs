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
    public class PlanoDAL : IPlano
    {
        public Response Delete(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM PLANOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Plano excluído com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                if (ex.Message.Contains("FK__PLANO"))
                {
                    resposta.Message = "Plano não pode ser excluído, pois existem ítens vinculados a ela!";
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

        public Response Insert(Plano p)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO PLANOS (NOME,MODALIDADE,VALOR,FREQUENCIA) VALUES (@NOME,@MODALIDADE,@VALOR,@FREQUENCIA)";
            command.Parameters.AddWithValue("@NOME", p.Nome);
            command.Parameters.AddWithValue("@MODALIDADE", p.IDModalidade);
            command.Parameters.AddWithValue("@VALOR", p.Valor);
            command.Parameters.AddWithValue("@FREQUENCIA", p.Frequencia);



            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Plano cadastrado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__PLANOS"))
                {
                    resposta.Message = "Plano já cadastrado!";
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

        public DataResponse<Plano> Select()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM PLANOS ORDER BY ID";

            DataResponse<Plano> resposta = new DataResponse<Plano>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Plano> planos = new List<Plano>();
                while (reader.Read())
                {
                    Plano plano = new Plano();

                    plano.ID = Convert.ToInt32(reader["ID"]);
                    plano.Nome = Convert.ToString(reader["NOME"]);
                    plano.Valor = Convert.ToDouble(reader["VALOR"]);
                    plano.Frequencia = Convert.ToInt32(reader["FREQUENCIA"]);
                    plano.IDModalidade = Convert.ToInt32(reader["MODALIDADE"]);
                    planos.Add(plano);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = planos;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Plano>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Update(Plano p)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE PRODUTOS SET NOME = @NOME, FREQUENCIA = @FREQUENCIA, VALOR = @VALOR WHERE ID = @ID";
            command.Parameters.AddWithValue("@NOME", p.Nome);
            command.Parameters.AddWithValue("@ID", p.ID);
            command.Parameters.AddWithValue("@FREQUENCIA", p.Frequencia);
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

                if (ex.Message.Contains("UQ__PLANOS"))
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
        public DataResponse<PlanoQueryViewModel> GetPlanos()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT P.ID,
                                           P.NOME,
                                           M.DESCRICAO,
                                           P.VALOR,
                                           P.FREQUENCIA
                                    FROM PLANOS P INNER JOIN
                                         MODALIDADES M ON
                                    P.MODALIDADE = M.ID";

            DataResponse<PlanoQueryViewModel> resposta = new DataResponse<PlanoQueryViewModel>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<PlanoQueryViewModel> planos = new List<PlanoQueryViewModel>();
                while (reader.Read())
                {
                    PlanoQueryViewModel p = new PlanoQueryViewModel();
                    p.ID = Convert.ToInt32(reader["ID"]);
                    p.Nome = Convert.ToString(reader["NOME"]);
                    p.Modalidade = Convert.ToString(reader["DESCRICAO"]);
                    p.Valor = Convert.ToDouble(reader["VALOR"]);
                    p.Frequencia = Convert.ToInt32(reader["FREQUENCIA"]);
                    

                    planos.Add(p);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = planos;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<PlanoQueryViewModel>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }
      

        public SingleResponse<Plano> GetPlanoById(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM PLANOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            SingleResponse<Plano> resposta = new SingleResponse<Plano>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Plano plano = new Plano();
                    plano.Frequencia = Convert.ToInt32(reader["FREQUENCIA"]);
                    plano.Valor = Convert.ToDouble(reader["VALOR"]);
                    plano.IDModalidade = Convert.ToInt32(reader["MODALIDADE"]);
                    plano.ID = id;
                    resposta.Item = plano;
                    resposta.Success = true;
                    resposta.Message = "Dados selecionados com sucesso!";
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
    }
}

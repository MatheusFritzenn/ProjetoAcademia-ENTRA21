using Entities;
using Entities.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ModalidadeDAL : IModalidade
    {
        public Response Delete(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM MODALIDADES WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Modalidade excluída com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                if (ex.Message.Contains("FK__MODAL"))
                {
                    resposta.Message = "Modalidade não pode ser excluída, pois existem alunos vinculados a ela!";
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

        public Response Insert(Modalidade m)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO MODALIDADES (DESCRICAO) VALUES (@DESCRICAO)";
            command.Parameters.AddWithValue("@DESCRICAO", m.Descricao);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Modalidade cadastrada com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__MODALIDADES"))
                {
                    resposta.Message = "Modalidade já cadastrado!";
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

        public DataResponse<Modalidade> Select()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM MODALIDADES ORDER BY ID";

            DataResponse<Modalidade> resposta = new DataResponse<Modalidade>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Modalidade> modalidades = new List<Modalidade>();
                while (reader.Read())
                {
                    Modalidade modalidade = new Modalidade();
                    modalidade.ID = Convert.ToInt32(reader["ID"]);
                    modalidade.Descricao = Convert.ToString(reader["DESCRICAO"]);
                    modalidades.Add(modalidade);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = modalidades;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Modalidade>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Update(Modalidade m)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE MODALIDADES SET DESCRICAO = @DESCRICAO WHERE ID = @ID";
            command.Parameters.AddWithValue("@DESCRICAO", m.Descricao);
            command.Parameters.AddWithValue("@ID", m.ID);



            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Modalidade editada com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__MODALIDADES"))
                {
                    resposta.Message = "Modalidade já cadastrada!";
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
    }
}

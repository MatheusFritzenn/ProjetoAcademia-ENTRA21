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
    public class AlunoDAL : IAluno
    {
        
        public Response Delete(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
        
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM ALUNOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
        
            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Aluno excluído com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                if (ex.Message.Contains("FK__ALUNO"))
                {
                    resposta.Message = "Aluno não pode ser excluído, pois existem vendas vinculados a ela!";
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

        public Response Insert(Aluno a)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO ALUNOS (NOME,CPF,RG,TELEFONE,TELEFONE2,EMAIL,DATA_NASCIMENTO,MATRICULA,ATIVIDADE) VALUES (@NOME,@CPF,@RG,@TELEFONE,@TELEFONE2,@EMAIL,@DATA_NASCIMENTO,@MATRICULA,@ATIVIDADE)";
            command.Parameters.AddWithValue("@NOME", a.Nome);
            command.Parameters.AddWithValue("@CPF", a.CPF);
            command.Parameters.AddWithValue("@RG", a.RG);
            command.Parameters.AddWithValue("@TELEFONE", a.Telefone);
            command.Parameters.AddWithValue("@TELEFONE2", a.Telefone2);
            command.Parameters.AddWithValue("@EMAIL", a.Email);
            command.Parameters.AddWithValue("@DATA_NASCIMENTO", a.DataNascimento);
            command.Parameters.AddWithValue("@MATRICULA", a.DataMatricula);
            command.Parameters.AddWithValue("@ATIVIDADE", a.Atividade);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Aluno cadastrado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

               
                if (ex.Message.Contains("UQ__ALUNOS"))
                {
                    resposta.Message = "Aluno já cadastrado!";
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

        public DataResponse<Aluno> Select()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM ALUNOS WHERE ATIVIDADE = 1 ORDER BY ID";

            DataResponse<Aluno> resposta = new DataResponse<Aluno>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Aluno> alunos = new List<Aluno>();
                while (reader.Read())
                {
                    Aluno aluno = new Aluno();
                    aluno.ID = Convert.ToInt32(reader["ID"]);
                    aluno.Nome = Convert.ToString(reader["NOME"]);
                    aluno.CPF = Convert.ToString(reader["CPF"]);
                    aluno.RG = Convert.ToString(reader["RG"]);
                    aluno.Telefone = Convert.ToString(reader["TELEFONE"]);
                    aluno.Telefone2 = Convert.ToString(reader["TELEFONE2"]);
                    aluno.Email = Convert.ToString(reader["EMAIL"]);
                    aluno.DataNascimento = Convert.ToDateTime(reader["DATA_NASCIMENTO"]);
                    aluno.DataMatricula = Convert.ToDateTime(reader["MATRICULA"]);
                    aluno.Atividade = Convert.ToBoolean(reader["ATIVIDADE"]);

                    alunos.Add(aluno);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = alunos;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Aluno>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Update(Aluno a)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE ALUNOS SET NOME = @NOME, TELEFONE = @TELEFONE, EMAIL = @EMAIL, DATA_NASCIMENTO = @DATA_NASCIMENTO, ATIVIDADE = @ATIVIDADE WHERE ID = @ID";
            command.Parameters.AddWithValue("@NOME", a.Nome);
            command.Parameters.AddWithValue("@ID", a.ID);
            command.Parameters.AddWithValue("@TELEFONE", a.Telefone);
            command.Parameters.AddWithValue("@TELEFONE2", a.Telefone2);
            command.Parameters.AddWithValue("@EMAIL", a.Email);
            command.Parameters.AddWithValue("@DATA_NASCIMENTO", a.DataNascimento);
            command.Parameters.AddWithValue("@ATIVIDADE", a.Atividade);




            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Aluno editado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__ALUNOS"))
                {
                    resposta.Message = "Aluno já cadastrado!";
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
        public SingleResponse<Aluno> GetAlunoByUserID(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM ALUNOS WHERE USUARIO = @ID";
            command.Parameters.AddWithValue("@ID", id);
            SingleResponse<Aluno> resposta = new SingleResponse<Aluno>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Aluno aluno = new Aluno();
                    aluno.ID = Convert.ToInt32(reader["ID"]);
                    resposta.Item = aluno;
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

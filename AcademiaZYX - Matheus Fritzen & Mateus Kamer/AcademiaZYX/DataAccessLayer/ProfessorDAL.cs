using Entities;
using Entities.Enum;
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
    public class ProfessorDAL : IProfessor
    {
       

        public Response Delete(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM PROFESSORES WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Professor excluído com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                if (ex.Message.Contains("FK__PLANO_PRO__PROFE"))
                {
                    resposta.Message = "Professor não pode ser excluído, pois existem planos vinculados a ela!";
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

        public Response Insert(Professor p)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO PROFESSORES (CPF,RG,CIDADE,BAIRRO,RUA,TELEFONE,SALARIO,USUARIO) VALUES (@CPF,@RG,@CIDADE,@BAIRRO,@RUA,@TELEFONE,@SALARIO,@USUARIO)";
            command.Parameters.AddWithValue("@CPF", p.CPF);
            command.Parameters.AddWithValue("@RG", p.RG);
            command.Parameters.AddWithValue("@CIDADE", p.Cidade);
            command.Parameters.AddWithValue("@BAIRRO", p.Bairro);
            command.Parameters.AddWithValue("@RUA", p.Rua);
            command.Parameters.AddWithValue("@TELEFONE", p.Telefone);
            command.Parameters.AddWithValue("@SALARIO", p.Salario);
            command.Parameters.AddWithValue("@USUARIO", p.IDUsuario);


            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Professor cadastrado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;


                if (ex.Message.Contains("UQ__PROFESSORES"))
                {
                    resposta.Message = "Professor já cadastrado!";
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

        public DataResponse<Professor> Select()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM PROFESSORES ORDER BY ID";

            DataResponse<Professor> resposta = new DataResponse<Professor>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Professor> professores = new List<Professor>();
                while (reader.Read())
                {
                    Professor professor = new Professor();
                    professor.ID = Convert.ToInt32(reader["ID"]);
                    professor.RG = Convert.ToString(reader["RG"]);
                    professor.CPF = Convert.ToString(reader["CPF"]);
                    professor.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    professor.Telefone = Convert.ToString(reader["TELEFONE"]);
                    professor.Salario = Convert.ToDouble(reader["SALARIO"]);
                    professor.Cidade = Convert.ToString(reader["CIDADE"]);
                    professor.Bairro = Convert.ToString(reader["BAIRRO"]);
                    professor.Rua = Convert.ToString(reader["RUA"]);
                    professor.Comissao= Convert.ToDouble(reader["COMISSAO"]);


                    professores.Add(professor);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = professores;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Professor>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }


        public Response Update(Professor p)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE PROFESSORES SET CIDADE = @CIDADE, BAIRRO = @BAIRRO, RUA = @RUA, TELEFONE = @TELEFONE, SALARIO = @SALARIO, WHERE ID = @ID ";
            command.Parameters.AddWithValue("@ID", p.ID);
            command.Parameters.AddWithValue("@CIDADE", p.Cidade);
            command.Parameters.AddWithValue("@BAIRRO", p.Bairro);
            command.Parameters.AddWithValue("@RUA", p.Rua);
            command.Parameters.AddWithValue("@TELEFONE", p.Telefone);
            command.Parameters.AddWithValue("@SALARIO", p.Salario);



            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Professor editado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__PROFESSORES"))
                {
                    resposta.Message = "Professor já cadastrado!";
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
        public DataResponse<ProfessorQueryViewModel> GetProfessores()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
        
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT P.ID,
                                           U.NOME,
                                           P.CPF,
                                           P.RG,
                                           P.TELEFONE,
                                           P.SALARIO,
                                           P.CIDADE,
                                           P.BAIRRO,
                                           P.RUA
                                    FROM PROFESSORES P INNER JOIN
                                         USUARIOS U ON
                                    P.USUARIO = U.ID";
        
            DataResponse<ProfessorQueryViewModel> resposta = new DataResponse<ProfessorQueryViewModel>();
        
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<ProfessorQueryViewModel> professores = new List<ProfessorQueryViewModel>();
                while (reader.Read())
                {
                    ProfessorQueryViewModel p = new ProfessorQueryViewModel();
                    p.ID = Convert.ToInt32(reader["ID"]);
                    p.RG = Convert.ToString(reader["RG"]);
                    p.CPF = Convert.ToString(reader["CPF"]);
                    p.Usuario = Convert.ToString(reader["NOME"]);
                    p.Telefone = Convert.ToString(reader["TELEFONE"]);
                    p.Salario = Convert.ToDouble(reader["SALARIO"]);
                    p.Cidade = Convert.ToString(reader["CIDADE"]);
                    p.Bairro = Convert.ToString(reader["BAIRRO"]);
                    p.Rua = Convert.ToString(reader["RUA"]);

                    professores.Add(p);
                }
        
                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = professores;
                return resposta;
        
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<ProfessorQueryViewModel>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response UpdateComissao(int id, double valor)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE PROFESSORES SET COMISSAO = COMISSAO + @COMISSAO WHERE ID = @ID ";
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@COMISSAO", valor);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Professor editado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__PROFESSORES"))
                {
                    resposta.Message = "Professor já cadastrado!";
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

        public SingleResponse<Professor> GetProfessorByUserId(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM PROFESSORES WHERE USUARIO = @ID";
            command.Parameters.AddWithValue("@ID", id);
            SingleResponse<Professor> resposta = new SingleResponse<Professor>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Professor professor = new Professor();
                    professor.ID = Convert.ToInt32(reader["ID"]);
                    professor.CPF = Convert.ToString(reader["CPF"]);
                    professor.RG = Convert.ToString(reader["RG"]);
                    professor.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    professor.Salario = Convert.ToDouble(reader["SALARIO"]);
                    professor.Comissao = Convert.ToDouble(reader["COMISSAO"]);
                    professor.Bairro = Convert.ToString(reader["BAIRRO"]);
                    professor.Cidade = Convert.ToString(reader["CIDADE"]);
                    professor.Rua = Convert.ToString(reader["RUA"]);
                    professor.Telefone = Convert.ToString(reader["TELEFONE"]);

                    resposta.Item = professor;
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

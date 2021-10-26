using Entities;
using Entities.Enum;
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
    public class UsuarioDAL : IUsuario
    {
        public SingleResponse<Usuario>Authenticate(string email, string senha)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM USUARIOS WHERE EMAIL = @EMAIL AND SENHA = @SENHA";
            command.Parameters.AddWithValue("@EMAIL", email);
            command.Parameters.AddWithValue("@SENHA", senha);

            command.Connection = connection;

            SingleResponse<Usuario> response = new SingleResponse<Usuario>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Usuario u = new Usuario();
                    u.ID = Convert.ToInt32(reader["ID"]);
                    u.Nome = Convert.ToString(reader["NOME"]);
                    u.Email = Convert.ToString(reader["EMAIL"]);
                    u.Papel = (Papel)Convert.ToInt32(reader["PAPEL"]);
                    u.Senha = Convert.ToString(reader["SENHA"]);
                    u.Atividade = Convert.ToBoolean(reader["ATIVIDADE"]);
                    response.Success = true;
                    response.Message = "Autenticação realizada com sucesso.";
                    response.Item = u;
                    return response;
                }
                response.Success = false;
                response.Message = "Usuário e/ou senha inválidos.";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                return response;
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
            command.CommandText = "DELETE FROM USUARIOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);

            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Usuário excluído com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                if (ex.Message.Contains("FK__USUARIO"))
                {
                    resposta.Message = "Usuário não pode ser excluído, pois existem ítens vinculados a ele!";
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

        public Response Insert(Usuario u)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO USUARIOS (NOME,EMAIL,SENHA,PAPEL,ATIVIDADE) VALUES (@NOME,@EMAIL,@SENHA,@PAPEL,@ATIVIDADE)";
            command.Parameters.AddWithValue("@NOME", u.Nome);
            command.Parameters.AddWithValue("@EMAIL", u.Email);
            command.Parameters.AddWithValue("@SENHA", u.Senha);
            command.Parameters.AddWithValue("@PAPEL", u.Papel);
            command.Parameters.AddWithValue("@ATIVIDADE", u.Atividade);

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


                if (ex.Message.Contains("UQ__USUARIOS"))
                {
                    resposta.Message = "Usuário já cadastrado!";
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

        public DataResponse<Usuario> Select()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM USUARIOS WHERE ATIVIDADE = 1 ORDER BY ID";

            DataResponse<Usuario> resposta = new DataResponse<Usuario>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Usuario> usuarios = new List<Usuario>();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.ID = Convert.ToInt32(reader["ID"]);
                    usuario.Nome = Convert.ToString(reader["NOME"]);
                    usuario.Email = Convert.ToString(reader["EMAIL"]);
                    usuario.Senha = Convert.ToString(reader["SENHA"]);
                    usuario.Atividade = Convert.ToBoolean(reader["ATIVIDADE"]);
                    usuario.Papel = (Papel)Convert.ToInt32(reader["PAPEL"]);
                    usuarios.Add(usuario);
                }

                resposta.Success = true;
                resposta.Message = "Dados selecionados com sucesso!";
                resposta.Data = usuarios;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.Message = "Erro no banco de dados, contate o administrador.";
                resposta.Data = new List<Usuario>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Update(Usuario u)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE USUARIOS SET NOME = @NOME, EMAIL = @EMAIL, SENHA = @SENHA, PAPEL = @PAPEL, ATIVIDADE = @ATIVIDADE WHERE ID = @ID";
            command.Parameters.AddWithValue("@NOME", u.Nome);
            command.Parameters.AddWithValue("@ID", u.ID);
            command.Parameters.AddWithValue("@EMAIL", u.Email);
            command.Parameters.AddWithValue("@SENHA", u.Senha);
            command.Parameters.AddWithValue("@PAPEL", u.Papel);
            command.Parameters.AddWithValue("@ATIVIDADE", u.Atividade);


            Response resposta = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                resposta.Success = true;
                resposta.Message = "Usuário editado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;

                if (ex.Message.Contains("UQ__USUARIOS"))
                {
                    resposta.Message = "Usuário já cadastrado!";
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

        public SingleResponse<Usuario> GetUsuarioById(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM USUARIOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            SingleResponse<Usuario> resposta = new SingleResponse<Usuario>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.ID = Convert.ToInt32(reader["ID"]);
                    usuario.Nome = Convert.ToString(reader["NOME"]);
                    usuario.Papel = (Papel)reader["PAPEL"];
                    usuario.Email = Convert.ToString(reader["EMAIL"]);
                    usuario.Senha = Convert.ToString(reader["SENHA"]);
                    usuario.Atividade = Convert.ToBoolean(reader["ATIVIDADE"]);
                    resposta.Item = usuario;
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

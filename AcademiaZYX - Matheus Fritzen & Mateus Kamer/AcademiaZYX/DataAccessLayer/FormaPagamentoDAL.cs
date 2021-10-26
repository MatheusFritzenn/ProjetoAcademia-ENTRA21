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
    public class FormaPagamentoDAL : IFormaPagamento
    {

        public Response Delete(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM FORMAS_PAGAMENTO WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);



            Response response = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                response.Success = true; response.Message = "Forma de pagamento deletada com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;

                if (ex.Message.Contains("FK__FORMA"))
                {
                    response.Message = "Forma de pagamento não pode ser excluida, pois existem locações vinculadas a ela!";
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

        public Response Insert(FormaPagamento f)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO FORMAS_PAGAMENTO (DESCRICAO) VALUES (@DESCRICAO)";
            command.Parameters.AddWithValue("@DESCRICAO", f.Descricao);


            Response response = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                response.Success = true; response.Message = "Forma de pagamento cadastrada com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                if (ex.Message.Contains("UQ__FORMA"))
                {
                    response.Message = "Forma de pagamento já cadastrada!";
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

        public DataResponse<FormaPagamento> Select()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM FORMAS_PAGAMENTO ORDER BY ID";

            DataResponse<FormaPagamento> response = new DataResponse<FormaPagamento>();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<FormaPagamento> formaPagamentos = new List<FormaPagamento>();
                while (reader.Read())
                {
                    FormaPagamento formaPagamento = new FormaPagamento();
                    formaPagamento.ID = Convert.ToInt32(reader["ID"]);
                    formaPagamento.Descricao = Convert.ToString(reader["DESCRICAO"]);
                    formaPagamentos.Add(formaPagamento);
                }

                response.Success = true;
                response.Message = "Dados selecionados com sucesso";
                response.Data = formaPagamentos;
                return response;
            }
            catch (Exception)
            {
                response.Success = false; response.Message = "Erro no banco de dados contate o admistrador";
                response.Data = new List<FormaPagamento>();
                return response;
            }
        }

        public Response Update(FormaPagamento f)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE FORMAS_PAGAMENTO SET DESCRICAO = @DESCRICAO WHERE ID = @ID";
            command.Parameters.AddWithValue("@DESCRICAO", f.Descricao);
            command.Parameters.AddWithValue("@ID", f.ID);



            Response response = new Response();
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                response.Success = true; response.Message = "Forma de pagamento editada com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                if (ex.Message.Contains("UQ__FORMAS_PAGAMENTO"))
                {
                    response.Message = "Forma de pagamento já cadastrada!";
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

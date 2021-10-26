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
    public class PlanoProfessorAlunoDAL : IPlanoProfessorAluno
    {
        public Response Delete(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM PLANO_PROFESSOR_ALUNO WHERE ID = @ID";
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

        public Response Insert(PlanoProfessorAluno p)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO PLANO_PROFESSOR_ALUNO (PLANO,PROFESSOR,ALUNO) VALUES (@PLANO,@PROFESSOR,@ALUNO)";
            command.Parameters.AddWithValue("@PLANO", p.IDPlano);
            command.Parameters.AddWithValue("@PROFESSOR", p.IDProfessor);
            command.Parameters.AddWithValue("@ALUNO", p.IDAluno);



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

        public DataResponse<PlanoProfessorAluno> Select()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM PLANO_PROFESSOR_ALUNO ORDER BY ID";

            DataResponse<PlanoProfessorAluno> resposta = new DataResponse<PlanoProfessorAluno>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<PlanoProfessorAluno> planos = new List<PlanoProfessorAluno>();
                while (reader.Read())
                {
                    PlanoProfessorAluno plano = new PlanoProfessorAluno();

                    plano.ID = Convert.ToInt32(reader["ID"]);
                    plano.IDAluno = Convert.ToInt32(reader["ALUNO"]);
                    plano.IDProfessor = Convert.ToInt32(reader["PROFESSOR"]);
                    plano.IDPlano = Convert.ToInt32(reader["PLANO"]);
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
                resposta.Data = new List<PlanoProfessorAluno>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }
        public DataResponse<PlanoProfessorAlunoQueryViewModel> GetPlanos()
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT P.ID,
                                           PL.NOME 'Plano',
                                           A.NOME 'Aluno',
                                           PR.ID 'Professor'
                                    FROM PLANO_PROFESSOR_ALUNO P INNER JOIN
                                         PLANOS PL ON
                                    P.PLANO = PL.ID INNER JOIN 
                                         ALUNOS A ON
                                    P.ALUNO = A.ID INNER JOIN
                                         PROFESSORES PR ON
                                    P.PROFESSOR = PR.ID";

            DataResponse<PlanoProfessorAlunoQueryViewModel> resposta = new DataResponse<PlanoProfessorAlunoQueryViewModel>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<PlanoProfessorAlunoQueryViewModel> planos = new List<PlanoProfessorAlunoQueryViewModel>();
                while (reader.Read())
                {
                    PlanoProfessorAlunoQueryViewModel p = new PlanoProfessorAlunoQueryViewModel();
                    p.ID = Convert.ToInt32(reader["ID"]);
                    p.Plano = Convert.ToString(reader["Plano"]);
                    p.Aluno = Convert.ToString(reader["Aluno"]);
                    p.Professor = Convert.ToString(reader["Professor"]);
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
                resposta.Data = new List<PlanoProfessorAlunoQueryViewModel>();
                return resposta;
            }
            finally
            {
                connection.Dispose();
            }
        }
        public SingleResponse<PlanoProfessorAluno> GetPlanoProfessorAlunoById(int id)
        {
            string connectionString = SqlConnector.CONNECTION_STRING;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM PLANO_PROFESSOR_ALUNO WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            SingleResponse<PlanoProfessorAluno> resposta = new SingleResponse<PlanoProfessorAluno>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PlanoProfessorAluno plano = new PlanoProfessorAluno();
                    plano.ID = Convert.ToInt32(reader["ID"]);
                    plano.IDProfessor = Convert.ToInt32(reader["PROFESSOR"]);
                    plano.IDAluno = Convert.ToInt32(reader["ALUNO"]);
                    plano.IDPlano = Convert.ToInt32(reader["PLANO"]);
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

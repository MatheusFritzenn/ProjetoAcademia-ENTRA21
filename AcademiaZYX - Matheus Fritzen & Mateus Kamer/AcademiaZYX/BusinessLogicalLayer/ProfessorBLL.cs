using DataAccessLayer;
using Entities;
using Entities.Interfaces;
using Entities.ViewModel;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class ProfessorBLL : BaseValidator<Professor>, IProfessor
    {
        private ProfessorDAL professorDAL = new ProfessorDAL();
        public override Response Validate(Professor item)
        {
            //CIDADE
            if (string.IsNullOrWhiteSpace(item.Cidade))
            {
                this.AddError("Cidade deve ser informada.");
            }
            else
            {
                item.Cidade = Normatization.NormatizeString(item.Cidade);
                if (item.Cidade.Length < 3 || item.Cidade.Length > 30)
                {
                    this.AddError("Cidade deve conter entre 3 e 30 caracteres.");
                }
            }
            //BAIRRO
            if (string.IsNullOrWhiteSpace(item.Bairro))
            {
                this.AddError("Bairro deve ser informado.");
            }
            else
            {
                item.Bairro = Normatization.NormatizeString(item.Bairro);
                if (item.Bairro.Length < 3 || item.Bairro.Length > 30)
                {
                    this.AddError("Bairro deve conter entre 3 e 30 caracteres.");
                }
            }
            //RUA
            if (string.IsNullOrWhiteSpace(item.Rua))
            {
                this.AddError("Rua deve ser informado.");
            }
            else
            {
                item.Rua = Normatization.NormatizeString(item.Rua);
                if (item.Rua.Length < 3 || item.Rua.Length > 30)
                {
                    this.AddError("Rua deve conter entre 3 e 30 caracteres.");
                }
            }


            //CPF
            if (string.IsNullOrWhiteSpace(item.CPF))
            {
                this.AddError("CPF deve ser informada.");
            }

            item.CPF = item.CPF?.Trim().Replace("-", "").Replace(".", "");
            string erroCPF = CommonValidations.IsCpf(item.CPF);

           


            //TELEFONE
            if (string.IsNullOrWhiteSpace(item.Telefone))
            {
                this.AddError("Telefone deve ser informado!");
            }

            string regexTelefone = @"^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$";
            bool isValidTelefone = Regex.IsMatch(item.Telefone, regexTelefone);
            if (!isValidTelefone)
            {
                this.AddError("Telefone inválido.");
            }

            return base.Validate(item);
        }

        public Response Insert(Professor p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }


            return professorDAL.Insert(p);
        }

       

        public Response Update(Professor p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            return professorDAL.Update(p);
        }

        public Response Delete(int id)
        {
            return professorDAL.Delete(id);
        }

        public DataResponse<Professor> Select()
        {
            return professorDAL.Select();
        }
        public DataResponse<ProfessorQueryViewModel> GetProfessores()
        {
            return professorDAL.GetProfessores();
        }

        public Response UpdateComissao(int id, double valor)
        {
            return professorDAL.UpdateComissao(id, valor);
        }

        public SingleResponse<Professor> GetProfessorByUserId(int id)
        {
            return professorDAL.GetProfessorByUserId(id);

        }
    }
}

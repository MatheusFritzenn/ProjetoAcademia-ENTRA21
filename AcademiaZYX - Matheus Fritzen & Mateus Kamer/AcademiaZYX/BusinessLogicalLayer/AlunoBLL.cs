using DataAccessLayer;
using Entities;
using Entities.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class AlunoBLL : BaseValidator<Aluno>, IAluno
    {
        private AlunoDAL alunoDAL = new AlunoDAL();

        public override Response Validate(Aluno item)
        {
            //NOME
            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                this.AddError("Nome deve ser informado.");
            }
            else
            {
                item.Nome = Normatization.NormatizeString(item.Nome);
                if (item.Nome.Length < 3 || item.Nome.Length > 30)
                {
                    this.AddError("Nome deve conter entre 3 e 30 caracteres.");
                }
            }


            //CPF
            if (string.IsNullOrWhiteSpace(item.CPF))
            {
                this.AddError("CPF deve ser informada.");
            }

            item.CPF = item.CPF?.Trim().Replace("-", "").Replace(".", "");
            string erroCPF = CommonValidations.IsCpf(item.CPF);

            //EMAIL
            if (string.IsNullOrWhiteSpace(item.Email))
            {
                this.AddError("Email deve ser informado!");
            }
            if (item.Email.Length < 8 || item.Email.Length > 100)
            {
                this.AddError("Email inválido.");
            }

            string regexEmail = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool isValidEmail = Regex.IsMatch(item.Email, regexEmail);
            if (!isValidEmail)
            {
                this.AddError("Email inválido.");
            }

            //RG
            if (string.IsNullOrWhiteSpace(item.RG))
            {
                this.AddError("RG deve ser informado!");
            }

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

            //TELEFONE2
            if (string.IsNullOrWhiteSpace(item.Telefone2))
            {
                this.AddError("Telefone deve ser informado!");
            }

            string regexTelefone2 = @"^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$";
            bool isValidTelefone2 = Regex.IsMatch(item.Telefone2, regexTelefone2);
            if (!isValidTelefone2)
            {
                this.AddError("Telefone inválido.");
            }

            return base.Validate(item);
        }


        public Response Delete(int id)
        {
            return alunoDAL.Delete(id);
        }

        public Response Insert(Aluno a)
        {
            Response response = this.Validate(a);
            if (!response.Success)
            {
                return response;
            }
            a.Atividade = true;
            return alunoDAL.Insert(a);
        }

        public DataResponse<Aluno> Select()
        {
            return alunoDAL.Select();
        }

        public Response Update(Aluno a)
        {
            Response response = this.Validate(a);
            if (!response.Success)
            {
                return response;
            }

            return alunoDAL.Update(a);
        }
        public SingleResponse<Aluno> GetAlunoByUserID(int id)
        {
            return alunoDAL.GetAlunoByUserID(id);
        }
    }
}

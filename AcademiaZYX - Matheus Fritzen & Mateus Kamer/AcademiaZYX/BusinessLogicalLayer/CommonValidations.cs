using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    internal class CommonValidations
    {
        public static string IsCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return "CPF deve ser informado.\r\n";
            }

            if (cpf.Length != 11)
            {
                return "CPF deve conter 11 caracteres.\r\n";
            }


            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            bool valido = cpf.EndsWith(digito);
            if (!valido)
            {
                return "CPF inválido.\r\n";
            }
            return "";
        }
        public static string IsEmail(string email, bool isRequired)
        {
            if (isRequired && string.IsNullOrWhiteSpace(email))
            {
                return "Email deve ser informado.\r\n";
            }

            string erroEmail = IsValidLength(email, 8, 100, "Email");
            if (erroEmail != "")
            {
                return erroEmail;
            }
            
            string regex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool isValid = Regex.IsMatch(email, regex);
            if (!isValid)
            {
                return "Email inválido.\r\n";
            }
            return "";
        }
        public static string IsValidLength(string value, int min, int max, string fieldName)
        {
            if (value.Length < min || value.Length > max)
            {
                return string.Format("O campo {0} deve conter entre {1} e {2} caracteres.\r\n", fieldName, min, max);
            }
            return "";
        }
        public static string IsValidBairro(string bairro)
        {
            if (string.IsNullOrWhiteSpace(bairro))
            {
                return "Bairro deve ser informado.\r\n";
            }
            return IsValidLength(bairro, 3, 100, "Bairro");
        }

        public static string IsValidRua(string rua)
        {
            if (string.IsNullOrWhiteSpace(rua))
            {
                return "Rua deve ser informada.\r\n";
            }
            return IsValidLength(rua, 3, 100, "Rua");
        }
        public static string IsValidCidade(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
            {
                return "Rua deve ser informada.\r\n";
            }
            return IsValidLength(cidade, 3, 100, "Rua");
        }
        public static string AssertOnlyLetters(string value, string fieldName)
        {
            for (int i = 0; i < value.Length; i++)
            {
                char letra = value[i];
                if (!char.IsLetter(letra) && letra != ' ' && letra != '\'')
                {
                    return string.Format("O campo {0} não pode conter caracteres inválidos", fieldName);
                }
            }
            return "";
        }

        public static string IsValidNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return "O nome deve ser informado.\r\n";
            }
            string tamanhoEhValido = CommonValidations.IsValidLength(nome, 3, 70, "Nome");
            if (tamanhoEhValido != "")
            {
                return tamanhoEhValido;
            }
            return CommonValidations.AssertOnlyLetters(nome, "Nome");
        }

        public static string IsValidRG(string rg)
        {
            if (string.IsNullOrWhiteSpace(rg))
            {
                return "O RG deve ser informado.";
            }

            string regex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool isValid = Regex.IsMatch(rg, regex);
            if (!isValid)
            {
                return "RG inválido.\r\n";
            }
            return "";
        }

        public static string IsValidNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
            {
                return "Informe o número!";
            }
            if (numero.Length > 5)
            {
                return "Número inválido.";
            }
            return "";
        }

        public static string IsValidDataNascimento(DateTime data)
        {
            return "";
        }

        public static string IsValidDataMatricula(DateTime data)
        {
            return "";
        }
        public static string IsValidModalidade(string modalidade)
        {
            if (string.IsNullOrWhiteSpace(modalidade))
            {
                return "Modalidade deve ser informada.";
            }

            return "";
        }
    }
}

using Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Professor
    {
        public int ID { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Rua { get; set; }
        public double Comissao { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public double Salario { get; set; }
        public Usuario Usuario { get; set; }
        public int IDUsuario { get; set; }

        public Professor()
        {
            this.Usuario = new Usuario();
        }

    }
}

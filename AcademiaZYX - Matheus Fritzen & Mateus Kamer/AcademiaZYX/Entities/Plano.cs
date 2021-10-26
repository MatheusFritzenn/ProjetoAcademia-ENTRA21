using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Plano
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public int Frequencia { get; set; }
        public Modalidade Modalidade { get; set; }
        public int IDModalidade { get; set; }
        
    }
}

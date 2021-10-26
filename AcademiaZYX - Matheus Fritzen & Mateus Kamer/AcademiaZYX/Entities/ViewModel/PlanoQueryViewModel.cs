using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class PlanoQueryViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int Frequencia { get; set; }
        public double Valor { get; set; }
        public string Modalidade { get; set; }
    }
}

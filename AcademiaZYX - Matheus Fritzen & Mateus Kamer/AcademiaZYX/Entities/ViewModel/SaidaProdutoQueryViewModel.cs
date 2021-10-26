using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class SaidaProdutoQueryViewModel
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public string Usuario { get; set; }
        public string FormaPagamento { get; set; }
    }
}

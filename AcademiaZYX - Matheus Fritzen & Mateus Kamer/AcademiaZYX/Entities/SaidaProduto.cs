using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SaidaProduto
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public Usuario Usuario { get; set; }
        public int IDUsuario { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public int IDFormaPagamento { get; set; }

        public List<ItemSaida> Items { get; set; }

       

        public SaidaProduto()
        {
            this.Items = new List<ItemSaida>();
        }
    }
}

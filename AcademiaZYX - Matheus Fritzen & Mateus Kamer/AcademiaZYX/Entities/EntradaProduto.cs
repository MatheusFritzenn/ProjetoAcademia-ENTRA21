using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EntradaProduto
    {
        public int ID { get; set; }
        public double Valor
        {
            get;set;
        }
        public DateTime Data { get; set; }
        public Usuario Usuario { get; set; }
        public int IDUsuario { get; set; }

        public List<ItemEntrada> Items { get; set; }

        public void CalcularPreco()
        {
            this.Valor = this.Items.Sum(c => c.Valor * c.Quantidade);
        }

        public EntradaProduto()
        {
            this.Items = new List<ItemEntrada>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Produto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Categoria Categoria { get; set; }
        public int IDCategoria { get; set; }
        public double Estoque { get; set; }
        public double Valor { get; set; }

        public Produto()
        {
            this.Categoria = new Categoria();
        }
    }
}

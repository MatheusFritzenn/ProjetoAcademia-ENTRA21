using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ItemSaida
    {
        public int ID { get; set; }
        public Produto Produto { get; set; }
        public int IDProduto { get; set; }
        public double Quantidade { get; set; }

        public override string ToString()
        {
            return "ID do Produto : " + IDProduto.ToString() + " - " + "Quantidade: " + Quantidade.ToString();
        }
    }
}

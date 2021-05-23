using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrator
{
    public class SimpleMediaProdct
    {
        public int Id { get; private set; }

        public int Price { get; private set; }

        public int Count { get; set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public SimpleMediaProdct(int id, int price, int count, string name, string description)
        {
            Id = id;
            Price = price;
            Name = name;
            Count = count;
            Description = description;
        }
    }
}

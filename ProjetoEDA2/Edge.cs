using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEDA2
{
    public class Edge
    {
        public Node From { get; set; }
        public Node To { get; set; }
        public double Custo { get; set; }

        public Edge(Node from, Node to, double cost)
        {
            From = from;
            To = to;
            Custo = cost;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

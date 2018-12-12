using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEDA2
{
    public class Node
    {
        public int x { get; set; }
        public int y { get; set; }
        public string Name { get; set; }
        public object Info { get; set; }
        public bool Visited { get; set; }
        public List<Edge> Edges { get; set; }
        public Node Parent { get; set; }
        public int Distancia { get; set; }

        public Node()
        {
            this.Edges = new List<Edge>();
        }

        public Node(string name, object info) : this()
        {
            this.Name = name;
            this.Info = info;
            this.Visited = false;
        }

        public void AddEdge(Node to)
        {
            AddEdge(to, 0);
        }

        public void AddEdge(Node to, double cost)
        {
            Edges.Add(new Edge(this, to, cost));
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

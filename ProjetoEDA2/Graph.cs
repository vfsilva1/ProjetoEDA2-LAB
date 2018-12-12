using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEDA2
{
    public class Graph
    {

        #region Atributos

        /// <summary>
        /// Lista de nós que compõe o grafo.
        /// </summary>
        public List<Node> nodes;

        #endregion

        #region Propridades

        /// <summary>
        /// Mostra todos os nós do grafo.
        /// </summary>
        public Node[] Nodes
        {
            get { return this.nodes.ToArray(); }
        }

        #endregion

        #region Construtores

        /// <summary>
        /// Cria nova instância do grafo.
        /// </summary>
        public Graph()
        {
            this.nodes = new List<Node>();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Encontra o nó através do seu nome.
        /// </summary>
        /// <param name="name">O nome do nó.</param>
        /// <returns>O nó encontrado ou nulo caso não encontre nada.</returns>
        private Node Find(string name)
        {
            Node n = null;
            foreach (Node node in nodes)
            {
                if (node.Name == name)
                    n = node;
            }
            return n;
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name)
        {
            AddNode(name, null);
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name, object info)
        {
            nodes.Add(new Node(name, info));
        }

        /// <summary>
        /// Adiciona o arco entre dois nós associando determinado custo.
        /// </summary>
        /// <param name="from">O nó de origem.</param>
        /// <param name="to">O nó de destino.</param>
        /// <param name="cost">O cust associado.</param>
        public void AddEdge(string from, string to, double cost)
        {
            Node nFrom = Find(from);
            Node nTo = Find(to);
            if (nFrom != null && nTo != null)
            {
                nFrom.AddEdge(nTo, cost);
            }
        }

        /// <summary>
        /// Obtem todos os nós vizinhos de determinado nó.
        /// </summary>
        /// <param name="node">O nó origem.</param>
        /// <returns></returns>
        public Node[] GetNeighbours(string from)
        {
            Node n = Find(from);
            Node[] neighbours = null;
            if (n != null)
            {
                neighbours = new Node[n.Edges.Count];
                int i = 0;
                foreach (Edge e in n.Edges)
                {
                    neighbours[i++] = e.To;
                }
            }
            return neighbours;
        }

        public List<Node> ShortestPath (string inicio, string c1, string c2, string c3)
        {
            List<Node> caminho = new List<Node>();
            Queue<Node> q = new Queue<Node>();
            Node begin = Find(inicio);
            Node cofre1 = Find(c1);
            Node cofre2 = Find(c2);
            Node cofre3 = Find(c3);
            Node cofreMaisPerto = CofreMaisPerto(begin, cofre1, cofre2, cofre3);

            begin.Visited = true;
            caminho.Add(begin);
            q.Enqueue(begin);

            while (q.Count > 0)
            {
                Node n = q.Dequeue();
                SetCofresVisitados(n, cofre1, cofre2, cofre3);
                foreach (Edge e in n.Edges)
                {
                    if (e.To.Visited == false && e.To.Name != "W")
                    {
                        q.Enqueue(e.To);
                        e.To.Parent = n;
                        e.To.Visited = true;
                        caminho.Add(e.To);
                    }
                    //if (isCofre(cofreMaisPerto, cofre1, cofre2, cofre3) && GetQtdeCofresVisitados(cofre1, cofre2, cofre3) == 3)
                    if (e.To == cofreMaisPerto)
                    {
                        Node x = e.To;
                        while(x.Parent != null)
                        {
                            Console.WriteLine(x.Name);
                            x = x.Parent;
                        }
                        Console.WriteLine(x.Name);
                        return caminho;
                    }
                }
            }
            return caminho;
        }

        public Node CofreMaisPerto (Node ladrao, Node cofre1, Node cofre2, Node cofre3)
        {
            //distancia euclideana
            int num1 = Math.Abs(ladrao.x - cofre1.x) + Math.Abs(ladrao.y - cofre1.y);
            int num2 = Math.Abs(ladrao.x - cofre2.x) + Math.Abs(ladrao.y - cofre2.y);
            int num3 = Math.Abs(ladrao.x - cofre3.x) + Math.Abs(ladrao.y - cofre3.y);

            int menor = Math.Min(num1, num2);

            if (menor > num3 && cofre3.Visited == false)
                return cofre3;
            else if (menor == num2 && cofre2.Visited == false)
                return cofre2;
            else if (cofre1.Visited == false)
                return cofre1;

            return null;
        }

        public void SetCofresVisitados(Node n, Node v1, Node v2, Node v3)
        {
            if (n == v1)
                v1.Visited = true;
            else if (n == v2)
                v2.Visited = true;
            else if (n == v3)
                v3.Visited = true;
        }

        public int GetQtdeCofresVisitados(Node v1, Node v2, Node v3)
        {
            int cont = 0;

            if (v1.Visited)
                cont++;
            if (v2.Visited)
                cont++;
            if (v3.Visited)
                cont++;

            return cont;
        }

        public bool isCofre(Node n, Node v1, Node v2, Node v3)
        {
            if (n == v1 || n == v2 || n == v3)
                return true;

            return false;
        }

        #endregion

    }
}


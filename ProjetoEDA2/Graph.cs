﻿using System;
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
        public Node Find(string name)
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

        public List<Node> ShortestPath (string inicio, string destino)
        {
            List<Node> caminho = new List<Node>();
            Queue<Node> q = new Queue<Node>();
            Node begin = Find(inicio);
            Node end = Find(destino);
            //Node cofre1 = Find(c1);
            //Node cofre2 = Find(c2);
            //Node cofre3 = Find(c3);
            //Node cofreMaisPerto = CofreMaisPerto(begin, cofre1, cofre2, cofre3);
            //if (begin.Parent != null)
            //    begin.Parent.Visited = false;

            begin.Visited = true;
            q.Enqueue(begin);
            caminho.Add(begin);

            while (q.Count > 0)
            {
                Node n = q.Dequeue();
                foreach (Edge e in n.Edges)
                {
                    if (e.To.Visited == false && e.To.Name != "W")
                    {
                        q.Enqueue(e.To);
                        e.To.Parent = n;
                        e.To.Visited = true;
                        caminho.Add(e.To);

                        if (e.To == end)
                        {
                            return caminho;
                        }

                    }
                    //else if (isCofre(e.To, cofre1, cofre2, cofre3) && GetQtdeCofresVisitados(cofre1, cofre2, cofre3) < 3)
                    //{
                    //    cofreMaisPerto = CofreMaisPerto(e.To, cofre1, cofre2, cofre3);
                    //}
                }
            }
            return caminho;
        }

        public Node CofreMaisPerto (Node inicio, Node cofre1, Node cofre2, Node cofre3)
        {
            double num1 = -1;
            double num2 = -1;
            double num3 = -1;

            //distancia euclidiana
            if (cofre1 != null)
                num1 = Math.Sqrt(Math.Pow(inicio.x - cofre1.x, 2) + Math.Pow(inicio.y - cofre1.y, 2));
            if (cofre2 != null)
                num2 = Math.Sqrt(Math.Pow(inicio.x - cofre2.x, 2) + Math.Pow(inicio.y - cofre2.y, 2));
            if (cofre3 != null)
                num3 = Math.Sqrt(Math.Pow(inicio.x - cofre3.x, 2) + Math.Pow(inicio.y - cofre3.y, 2));

            //int menor = Math.Min(num1, num2);
            List<double> lstNum = new List<double>();
            lstNum.Add(num1);
            lstNum.Add(num2);
            lstNum.Add(num3);
            lstNum.Sort();

            for (int i = 0; i < lstNum.Count; i++)
                if (lstNum[i] == -1)
                    lstNum.Remove(lstNum[i]);

            foreach (int num in lstNum)
            {
                if (num == num1 && cofre1.Visited == false)
                    return cofre1;
                else if (num == num2 && cofre2.Visited == false)
                    return cofre2;
                else if (num == num3 && cofre3.Visited == false)
                    return cofre3;
            }

            return null;
        }

        public void PrintAnswer (Node destino)
        {
            List<Node> answer = new List<Node>();
            while (destino.Parent != null)
            {
                answer.Add(destino);
                destino = destino.Parent;
            }
            answer.Add(destino);

            for (int i = answer.Count - 1; i >= 0; i--)
            {
                //if (i > 0)
                    Console.Write(answer[i].Name + "->");
                //else
                //    Console.Write(answer[i].Name);
            }
        }

        public void SetCofresVisitados(Node n, Node c1, Node c2, Node c3)
        {
            if (n == c1)
                c1.Visited = true;
            else if (n == c2)
                c2.Visited = true;
            else if (n == c3)
                c3.Visited = true;
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


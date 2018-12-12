using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProjetoEDA2
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph grafo = new Graph();
            int tam = 0, cont;
            int ladraoX, ladraoY;
            int cofre1X, cofre1Y;
            int cofre2X, cofre2Y;
            int cofre3X, cofre3Y;
            
            Console.WriteLine("Digite o tamanho da sala: (Recomendado maior que 5)");
            tam = int.Parse(Console.ReadLine());
            Console.Clear();

            createGraph(grafo, tam);
            setCoordinates(grafo.nodes, tam);
            setSala(grafo.nodes, tam);

            Console.WriteLine("Digite as coordenadas do ladrão, ele tem que estar nas bordas da sala.");
            Console.WriteLine("Digite a coordenada X do ladrão: ");
            ladraoX = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite a coordenada Y do ladrão: ");
            ladraoY = int.Parse(Console.ReadLine());

            Console.Clear();
            setLadrao(grafo.nodes, tam, ladraoX, ladraoY);

            printSala(grafo.nodes, tam);

            Console.WriteLine("Digite as coordenadas do cofre 1, ele tem que estar na parede(P)");
            Console.WriteLine("Digite a coordenada X do cofre 1: ");
            cofre1X = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite a coordenada Y do cofre 1: ");
            cofre1Y = int.Parse(Console.ReadLine());

            setCofres(grafo.nodes, tam, cofre1X, cofre1Y, cont = 1);

            Console.WriteLine();
            Console.WriteLine("Digite as coordenadas do cofre 2, ele tem que estar na parede(P)");
            Console.WriteLine("Digite a coordenada X do cofre 2: ");
            cofre2X = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite a coordenada Y do cofre 2: ");
            cofre2Y = int.Parse(Console.ReadLine());

            setCofres(grafo.nodes, tam, cofre2X, cofre2Y, cont = 2);

            Console.WriteLine();
            Console.WriteLine("Digite as coordenadas do cofre 2, ele tem que estar na parede(P)");
            Console.WriteLine("Digite a coordenada X do cofre 3: ");
            cofre3X = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite a coordenada Y do cofre 3: ");
            cofre3Y = int.Parse(Console.ReadLine());

            setCofres(grafo.nodes, tam, cofre3X, cofre3Y, cont = 3);

            setEdges(grafo, tam);

            Console.Clear();
            printSala(grafo.nodes, tam);

            //Console.WriteLine("GetNeighbours: ");
            //string letra = Console.ReadLine();
            //foreach (Node n in grafo.GetNeighbours(letra))
            //{
            //    Console.WriteLine(n.Name);
            //}

            List<Node> caminho = grafo.ShortestPath("L", "C1", "C2", "C3");
            //Console.WriteLine(caminho.Count);
            //foreach (Node n in caminho)
            //{
            //    Console.WriteLine(n.Name);
            //}

            Console.ReadKey();
        }

        public static void setEdges(Graph g, int tam)
        {
            int cont = 0;
            for (int i = 0; i < tam; i++)
            {
                for (int j = 0; j < tam; j++)
                {
                    if (j < tam - 1)
                    {
                        g.AddEdge(g.nodes[cont].Name, g.nodes[cont + 1].Name, 1);
                        g.AddEdge(g.nodes[cont + 1].Name, g.nodes[cont].Name, 1);
                    }

                    if (i < tam - 1)
                    {
                        g.AddEdge(g.nodes[cont].Name, g.nodes[cont + tam].Name, 1);
                        g.AddEdge(g.nodes[cont + tam].Name, g.nodes[cont].Name, 1);
                    }

                    cont++;
                }
                
            }
        }

        public static void setSala (List<Node> nodes, int tam)
        {
            int cont = 0;
            for (int i = 0; i < tam; i++)
            {
                for (int j = 0; j < tam; j++)
                {
                    if (isWall(tam, j, i))
                        nodes[cont].Name = "W";
                    //else
                    //    nodes[cont].Name = "0";

                    cont++;
                }
            }
        }

        public static void setLadrao (List<Node> nodes, int tam, int x, int y)
        {
            int cont = 0, flag = 0;
            for (int i = 0; i < tam; i++)
            {
                for (int j = 0; j < tam; j++)
                {
                    if (nodes[cont].x == x && nodes[cont].y == y && isWall(tam, x, y))
                    {
                        nodes[cont].Name = "L";
                        flag = 1;
                    }
                    
                    cont++;
                }
            }

            if (flag == 0)
            {

                Console.Clear();
                Console.WriteLine("Posição inválida!");
            }
        }

        public static void setCofres (List<Node> nodes, int tam, int cofreX, int cofreY, int numCofre)
        {
            int cont = 0;
            for (int i = 0; i < tam; i++)
            {
                for (int j = 0; j < tam; j++)
                {
                    if (nodes[cont].x == cofreX && nodes[cont].y == cofreY && isWall(tam, cofreX, cofreY))
                    {
                        switch (numCofre)
                        {
                            case 1:
                                nodes[cont].Name = "C1";
                                break;
                            case 2:
                                nodes[cont].Name = "C2";
                                break;
                            case 3:
                                nodes[cont].Name = "C3";
                                break;
                            default:
                                break;
                        }
                    }
                    
                    cont++;
                }
            }
        }

        public static void setCoordinates (List<Node> nodes, int tam)
        {
            int cont = 0;
            for (int i = 0; i < tam; i++)
            {
                for (int j = 0; j < tam; j++)
                {
                    nodes[cont].x = j;
                    nodes[cont].y = i;

                    cont++;
                }
            }
        }

        public static void createGraph (Graph g,int tam)
        {
            int cont = 0;
            for (int i = 0; i < tam; i++)
            {
                for (int j = 0; j < tam; j++)
                {
                    g.AddNode(cont++.ToString());
                }
            }
        }

        public static bool isWall (int tam, int x, int y)
        {
            if (x == 0 || x == tam - 1 || y == 0 || y == tam - 1)
                return true;

            return false;
        }

        public static bool isCorner (int tam, int x , int y)
        {
            if ((x == 0 && y == 0) || (x == tam - 1 && y == 0) || (x == 0 && y == tam - 1) || (x == tam - 1 && y == tam - 1))
                return true;

            return false;
        }

        public static void printSala(List<Node> nodes, int tam)
        {
            int cont = 0;
            for (int i = 0; i < tam; i++)
            {
                for (int j = 0; j < tam; j++)
                {
                    Console.Write(nodes[cont++].Name + "\t");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}

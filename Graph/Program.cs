using Microsoft.VisualBasic;

namespace Count
{
    class Program
    {
        static void Main()
        {
            Graph graph = new Graph();
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");
            graph.AddVertex("F");

            
            graph.AddEdge("A", "B");
            graph.AddEdge("A", "C");
            graph.AddEdge("B", "D");
            graph.AddEdge("C", "E");
            graph.AddEdge("D", "F");
            graph.AddEdge("E", "F");
            
            /*graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");
            graph.AddVertex("F");
            graph.AddVertex("G");
            graph.AddVertex("H");
            graph.AddVertex("V");
            
            graph.AddEdge("A", "B");
            graph.AddEdge("A", "E");
            graph.AddEdge("A", "C");
            graph.AddEdge("E", "B");
            graph.AddEdge("E", "D");
            graph.AddEdge("D", "B");
            graph.AddEdge("C", "D");
            graph.AddEdge("F", "D");
            graph.AddEdge("G", "D");
            graph.AddEdge("H", "B");
            graph.AddEdge("H", "V");
            graph.AddEdge("V", "F");*/
            
            graph.ShowInfo();
            SearchDiameter(graph);
        }

        static void SearchDiameter(Graph graph)
        {
            List<List<Vertex>> chain = new List<List<Vertex>>();
            
            int Size = graph.GetCount();
            
            int diametr = -1;
            
            Vertex[] vertices = new Vertex[Size];
            
            for (var i = 0; i < Size; i++)
            {
                vertices[i] = graph.GetVertexIndex(i);
            }

            List<Vertex>[] parrent = new List<Vertex>[Size]; // Array of List
            
            int[] distance = new int[Size];
            
            for (var i = 0; i < Size; i++) // BFS
            {
                SetVisitedDefault(vertices);
                
                SetValueDefault(ref parrent, ref distance, Size);
                
                parrent[i].Add(vertices[i]); // set parrent vertix for source vertex
                
                List<Vertex> queueName = new List<Vertex>();
                List<int> queueDist = new List<int>();

                queueName.Add(graph.GetVertexIndex(i));
                
                queueDist.Add(0);
                
                while (queueName.Count != 0)
                {
                    Vertex from = queueName[0];
                    
                    from.visited = true;
                    
                    int x = -1;
                    
                    int next = graph.Next(from, x);
                    
                    while (next!=-1)
                    {
                        Vertex neighbor = graph.GetVertexIndex(next);
                        
                        if (!neighbor.visited)
                        {
                            int j = Array.IndexOf(vertices, neighbor);
                            if (distance[j] == 0 || distance[j] != distance[Array.IndexOf(vertices, queueName[0])])
                            {
                                queueName.Add(neighbor);
                            
                                queueDist.Add(queueDist[0]+1);
                            
                                if (parrent[j].IndexOf(queueName[0])==-1) parrent[j].Add(queueName[0]);
                            
                                distance[j] = queueDist[0] + 1;
                            }
                        }
                        
                        x++;
                        
                        next = graph.Next(from, x);
                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
                    queueName.RemoveAt(0);
                    queueDist.RemoveAt(0);
                }
                
                int diam = distance.Max();
                
                if (diametr < diam)
                {
                    diametr = diam;
                    
                    chain.Clear();
                }
                
                for (int j = 0; j < Size; j++) // fill diameter chain
                {
                    if (distance[j] == diametr)
                    {
                        List<Vertex> bufChain = new List<Vertex>();
                        bufChain.Add(vertices[j]);
                        FillChain(parrent,parrent[j], graph.GetVertexIndex(i), chain, bufChain, vertices);
                    }
                }
            }
        
            Console.WriteLine("\nДиаметр графа равен: {0}", diametr);
            
            Console.WriteLine("Диаметральные цепи:");

            foreach (var i in chain) // print Diameter chain
            {
                Console.Write("Цепь {0} {1} : ", i[0].name, i[i.Count-1].name);
                
                foreach (var j in i)
                {
                    Console.Write("{0} ", j.name);
                }

                Console.WriteLine();
            }
        }

        static void SetVisitedDefault(Vertex[] vertices)
        {
            foreach (var iVertex in vertices)
            {
                iVertex.visited = false;
            }
        }

        static void SetValueDefault(ref List<Vertex>[] parrent, ref int[] distance, int size)
        {
            for (int j = 0; j < size; j++) // Set default value
            {
                List<Vertex> empty = new List<Vertex>();
                    
                parrent[j] = empty;
                    
                distance[j] = 0;
            }
        }

        static void FillChain(List<Vertex>[] fullParrent, List<Vertex> parrent, Vertex from, List<List<Vertex>> chainDiam, List<Vertex> subChain, Vertex[] vertices)
        {
            
            if (parrent[0] == from)
            {
                subChain.Add(parrent[0]);
                
                List<Vertex> end = new List<Vertex>();
                
                end.AddRange(subChain.ToArray());
                
                chainDiam.Add(end);
                
                return;
            }
            if (parrent.Count == 1)
            {
                subChain.Add(parrent[0]);
                
                int j = Array.IndexOf(vertices, parrent[0]);
                
                FillChain(fullParrent, fullParrent[j],from, chainDiam, subChain, vertices);
            }

            
            if (parrent.Count > 1)
            {
                for (int i = 0; i < parrent.Count; i++)
                {
                    List<Vertex> bufParrent = new List<Vertex>();
                    
                    bufParrent.Add(parrent[i]);
                    
                    int f = subChain.Count;
                    
                    FillChain(fullParrent,bufParrent, from, chainDiam, subChain, vertices);
                    
                    while (subChain.Count > f)
                        subChain.RemoveAt(f);
                }
            }
        }
    }
}



                                                                                                                                                                                                                    
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Count
{
    internal class Graph
    {
        private List<List<int>> graph = new List<List<int>>();

        private Dictionary<Vertex, int> vertexes = new Dictionary<Vertex, int>();
        
        public Vertex? GetVertexName(string name)
        {
            foreach(var vertex in vertexes.Keys)
                if (vertex.name == name) return vertex;
            
            return null;
        }

        public Vertex? GetVertexIndex(int i)
        {          
            foreach (var vertex in vertexes.Keys)           
                if (vertexes[vertex] == i) return vertex;                
            
            return null;
        }

        public void AddVertex(string name)
        {
            if (GetVertexName(name) != null) return;
            List<int> vs = new List<int>();

            for(int i = 0; i < graph.Count+1; i++)
            {
                if (i< graph.Count) graph[i].Add(0);

                vs.Add(0);
            }

            graph.Add(vs);

            vertexes.Add(new Vertex(name), graph.Count - 1);
        }

        public void DelVertex(string name)
        {
            Vertex? v = GetVertexName(name);
            if (v == null) return;
            int i = vertexes[v];          
            graph.RemoveAt(i);

            for (int j = 0; j < graph.Count; j++)          
                graph[j].RemoveAt(i);

            bool fl = false;

            foreach (var vertex in vertexes.Keys)
            {
                if (fl)
                    vertexes[vertex]--;

                if (vertex.name == name)
                {
                    vertexes.Remove(vertex);
                    fl = true;
                }
            }                                    
        }

        public void AddEdge(string firstname, string secondname) 
        {
            Vertex? v = GetVertexName(firstname);
            if (v == null) return;
            int f = vertexes[v];
            v = GetVertexName(secondname);
            if (v == null) return;
            int s = vertexes[v];        
            graph[f][s] = 1;
            graph[s][f] = 1;
        }

        public void DelEdge(string firstname, string secondname)
        {
            Vertex? v = GetVertexName(firstname);
            if (v == null) return;
            int f = vertexes[v];
            v = GetVertexName(secondname);
            if (v == null) return;
            int s = vertexes[v];
            graph[f][s] = 0;
            graph[s][f] = 0;
        }

        public int First(Vertex v) 
        {
            for (int i = 0; i < graph.Count; i++)
            {
                if (GetVertexIndex(i)?.name == v.name)
                {
                    int buf = graph[i].IndexOf(1);
                    if ( buf > -1)
                    {
                        return buf;
                    }
                    break;
                }                                   
            }
            return -1;
        }

        public int Next(Vertex v, int g)
        {
            for (int i = 0; i < graph.Count; i++)
            {
                if (GetVertexIndex(i)?.name == v.name)
                {
                    int h = 0;

                    for(int j=0; j < graph[i].Count; j++)
                    {
                        if (graph[i][j] == 1)
                        {
                            if(h==g+1) return vertexes[GetVertexIndex(j)];
                            h++;
                        }
                    }

                    break;
                }
            }
            return -1;
        }

        public Vertex SmejVertex(Vertex v, int g)
        {
            for (int i = 0; i < graph.Count; i++)
            {
                if (GetVertexIndex(i)?.name == v.name)
                {
                    int h = 0;

                    for (int j = 0; j < graph[i].Count; j++)
                    {
                        if (graph[i][j] == 1)
                        {
                            if(h==g) return GetVertexIndex(j);
                            h++;
                        }
                    }

                    break;
                }
            }
            return null;
        }

        public void EditVertex(string oldName, string newName)
        {
            Vertex vertex = GetVertexName(oldName); if (vertex == null) return;

            vertex = GetVertexName(newName); if (vertex != null) return;

            int i = vertexes[vertex];

            vertexes.Remove(vertex);

            vertexes.Add(new Vertex(newName), i);
        }

        public void ShowInfo()
        {
            Console.Write("   ");
            for(int i =0; i< graph.Count; i++) Console.Write("{0} ",GetVertexIndex(i).name);
            Console.WriteLine();
            for(int i =0; i< graph.Count; i++)
            {
                Console.Write("{0} |", GetVertexIndex(i).name/*, vertexes[GetVertexIndex(i)]*/);

                for(int j = 0; j < graph[i].Count(); j++)                
                    Console.Write("{0} ", graph[i][j]);

                Console.WriteLine();
            }
        }

        public int GetCount()
        {
            return vertexes.Count();
        }
    }
}

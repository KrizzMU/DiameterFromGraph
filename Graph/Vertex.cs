using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count
{
    internal class Vertex
    {
        public string name;
        public bool visited;
        public Vertex() { }
        public Vertex(string name)
        {
            this.name = name;
            visited = false;
        }
    }
}

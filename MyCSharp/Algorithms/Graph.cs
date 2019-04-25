using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Algorithms
{
    public class Graph
    {
        private int vNo;          // number of vertices   
        private int eNo;                // number of edges   
        private List<int>[] adjList;   // adjacency lists   

        public Graph(int V)
        {
            this.vNo = V;
            this.eNo = 0;
            adjList = new List<int>[V];      // Create array of lists.      
            for (int v = 0; v < V; v++)             // Initialize all lists          
                adjList[v] = new List<int>();         //   to empty.    
        }

        public Graph(Stream stream)
        {
            new Graph(int.Parse(Console.ReadLine()));          // Read V and construct this graph.      
            int E = int.Parse(Console.ReadLine());        // Read E.      
            for (int i = 0; i < E; i++)
            {  // Add an edge.         
                int v = int.Parse(Console.ReadLine());     // Read a vertex,         
                int w = int.Parse(Console.ReadLine());        // read another vertex,         
                addEdge(v, w);               // and add edge connecting them.      
            }
        }

        public int V() { return vNo; }

        public int E() { return eNo; }

        public void addEdge(int v, int w)
        {
            adjList[v].Add(w);                          // Add w to v’s list.      
            adjList[w].Add(v);                          // Add v to w’s list.      
            eNo++;
        }

        public IEnumerable<int> adj(int v)
        {
            return adjList[v];
        }





    }

}

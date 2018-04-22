using System;
using System.Collections.Generic;

namespace SuffixTree
{
    public class Node
    {
        public Dictionary<char, Edge> Edges { get; set; }

        public Node Link { get; set; }

        public Node()
        {
            Edges = new Dictionary<char, Edge>();
        }
        
    }
}
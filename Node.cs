using System;
using System.Collections.Generic;

namespace SuffixTree
{
    public class Node
    {   
        /// <summary>
        /// use a dictionart to store the all the edges that this node has
        /// the key is the first item in the edge
        /// the value is the edge which correspond to the key
        /// </summary>
        public Dictionary<char, Edge> Edges { get; set; }
        /// <summary>
        /// point to the node which was created with "this node" in one step
        /// </summary>
        public Node Link { get; set; }
        /// <summary>
        /// init the node attributes
        /// </summary>
        public Node()
        {
            Edges = new Dictionary<char, Edge>();
            SupportDic = new Dictionary<char, int>();
        }

        /// <summary>
        /// store the support of each edge
        /// </summary>
        public Dictionary<char, int> SupportDic { get; set; }

        /// <summary>
        /// store the support of this node
        /// </summary>
        public int Support { get; set; }


        public void CalculateSupport()
        {
            GetSupport(this);
        }

        private int GetSupport(Node node)
        {
            var support = 0;
            if (node == null)
            {
                return 1;
            }
            else
            {
                foreach (var keyValue in node.Edges)
                {
                    var temp = GetSupport(keyValue.Value.Next);
                    node.SupportDic.Add(keyValue.Key, temp);
                    support += temp;
                }

                node.Support = support;
                return support;
            }
        }

    }
}
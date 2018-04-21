using System.Collections.Generic;

namespace SuffixTree
{
    public class SuffixTree
    {
        private Node Root { get; set; }

        private char ActiveEdge { get; set; }

        private Node ActiveNode { get; set; }

        private int ActiveLength { get; set; }

        private int Remainder { get; set; }

        public SuffixTree()
        {
            ActiveEdge = '\0';
            ActiveNode = Root;
            ActiveLength = 0;
            Remainder = 0;
        }

        public void Dig(List<char> sequence)
        {
            for (int i = 0; i < sequence.Count; i++)
            {
                
            }
        }

    }
}
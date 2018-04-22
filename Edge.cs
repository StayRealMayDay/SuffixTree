using System;

namespace SuffixTree
{
    public class Edge
    {
        public int From { set; get; }

        public int To { get; set; }

        public int Length { get; set; }

        public Edge(int from, int to)
        {
            this.From = from;
            this.To = to;
            this.Length = to - from;
        }

        public int GetLength()
        {
            return this.To - this.From > 0 ? this.To - this.From : int.MaxValue;
        }

        public Node Next { get; set; }
    }
}
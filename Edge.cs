using System;

namespace SuffixTree
{
    public class Edge
    {
        /// <summary>
        /// indicate the start location of this edge
        /// </summary>
        public int From { set; get; }
        /// <summary>
        /// indicate the end index of this edge
        /// </summary>
        public int To { get; set; }
        /// <summary>
        /// the length of this edge , also indicate number of item in this edge
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// init this edge
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public Edge(int from, int to)
        {
            this.From = from;
            this.To = to;
            this.Length = to - from;
        }
        /// <summary>
        /// use this method to return the newest length of this edge
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return this.To - this.From > 0 ? this.To - this.From : int.MaxValue;
        }
        /// <summary>
        /// point to the node that this edge link to
        /// </summary>
        public Node Next { get; set; }
    }
}
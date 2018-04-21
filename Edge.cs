namespace SuffixTree
{
    public class Edge
    {
        public int From { set; get; }

        public int To { get; set; }

        public Edge(int from, int to)
        {
            this.From = from;
            this.To = to;
        }

        public Node Next { get; set; }
    }
}
using System.Collections.Generic;

namespace SuffixTree
{
    public class SuffixTree
    {

        public SuffixTree()
        {
            Root = new Node();
            Active_Edge = '\0';
            Active_Node = Root;
            Active_Length = 0;
            Remainder = 0;
            NeedSuffixLink = false;
        }

        public void Dig(List<char> sequence)
        {
            for (int i = 0; i < sequence.Count; i++)
            {
                var currentItem = sequence[i];
                Remainder++;
                Node oldNode = null;
                NeedSuffixLink = false;
                while (true)
                {
                    if (Active_Length == 0)
                    {
                        if (Active_Node.Edges.ContainsKey(currentItem))
                        {
                            Active_Edge = currentItem;
                            Active_Length++;
                            break;
                        }
                        else
                        {
                            Active_Node.Edges.Add(currentItem, new Edge(i, -1));
                            Remainder--;
                            if (Remainder == 0)
                            {
                                break;
                            }
                            else
                            {
                                if (Active_Node == Root)
                                {
                                    Active_Length = Remainder - 1;
                                    Active_Edge = sequence[i - Active_Length];
                                }
                                else
                                {
                                    Active_Node = Active_Node.Link ?? Root;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Active_Length >= Active_Node.Edges[Active_Edge].GetLength())
                        {
                            Active_Length -= Active_Node.Edges[Active_Edge].GetLength();
                            Active_Node = Active_Node.Edges[Active_Edge].Next;
                            Active_Edge = Active_Length == 0 ? '\0' : sequence[i - Active_Length];
                        }
                        else
                        {
                            if (sequence[Active_Node.Edges[Active_Edge].From + Active_Length] == currentItem)
                            {
                                Active_Length++;
                                break;
                            }
                            else
                            {
                                Node addNode = new Node();
                                addNode.Edges.Add(sequence[Active_Node.Edges[Active_Edge].From + Active_Length],
                                    new Edge(Active_Node.Edges[Active_Edge].From + Active_Length,
                                        Active_Node.Edges[Active_Edge].To));
                                addNode.Edges.Add(currentItem, new Edge(i, -1));
                                addNode.Edges[sequence[Active_Node.Edges[Active_Edge].From + Active_Length]].Next =
                                    Active_Node.Edges[Active_Edge].Next;
                                Active_Node.Edges[Active_Edge].Next = addNode;
                                Active_Node.Edges[Active_Edge].To = Active_Node.Edges[Active_Edge].From + Active_Length;
                                Remainder--;
                                if (NeedSuffixLink)
                                {
                                    if (oldNode == null) continue;
                                    oldNode.Link = addNode;
                                    oldNode = addNode;
                                }
                                else
                                {
                                    NeedSuffixLink = true;
                                    oldNode = addNode;
                                }
                                if (Active_Node == Root)
                                {
                                    Active_Length = Remainder - 1;
                                    Active_Edge = sequence[i - Active_Length];
                                }
                                else
                                {
                                    Active_Node = Active_Node.Link ?? Root;
                                }
                            }
                        }
                    }
                }
            }
        }

       
        public Node Root { get; }

        private char Active_Edge { get; set; }

        private Node Active_Node { get; set; }

        private int Active_Length { get; set; }

        private int Remainder { get; set; }
        
        private bool NeedSuffixLink { get; set; }
    }
}

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
                // oldNode used to keep the Node created in last loop
                Node oldNode = null;
                // init this variable to false
                NeedSuffixLink = false;
                while (true)
                {    
                    if (Active_Length == 0) // if the active length is 0,it means that we have no maching item from the active node yet
                    {
                        if (Active_Node.Edges.ContainsKey(currentItem)) // so if the avtive node has an edge contain current item
                        {                                               // we do nothing but set the active edge to the current item and active length plus 1
                            Active_Edge = currentItem;                  // after done this, we break this loop and take next item
                            Active_Length++;
                            break;
                        }
                        else                                            //if not , we need to add an edge to the active node and the key of this edge is current item
                        {
                            Active_Node.Edges.Add(currentItem, new Edge(i, -1));
                            Remainder--;                                // every time we insert an edge we need to decrease the Remainder variable
                            if (Remainder == 0)                         // if the Remainder is 0, it indicate that we have no suffix sequence wait to insert , so 
                            {                                           // we just break this loop
                                break;                   
                            }
                            else
                            {
                                if (Active_Node == Root)                // if not, we need to  judge whether the active node is root
                                {                                       // if it is the root node ,we need to recover the active length
                                    Active_Length = Remainder - 1;
                                    Active_Edge = sequence[i - Active_Length];
                                }
                                else
                                {
                                    if (Active_Node.Link != null)
                                    {
                                        Active_Node = Active_Node.Link;
                                    }
                                    else
                                    {
                                        Active_Node = Root;
                                        Active_Length = Remainder - 1;
                                        Active_Edge = sequence[i - Active_Length];
                                    }
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
                                    if (Active_Node.Link != null)
                                    {
                                        Active_Node = Active_Node.Link;
                                    }
                                    else
                                    {
                                        Active_Node = Root;
                                        Active_Length = Remainder - 1;
                                        Active_Edge = sequence[i - Active_Length];
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Root node
        /// </summary>
        public Node Root { get; }
        /// <summary>
        /// avtive edge is a char date which indicate the active edge of the active node
        /// </summary>
        private char Active_Edge { get; set; }
        /// <summary>
        /// it is a node indicate the active node
        /// </summary>
        private Node Active_Node { get; set; } 
        /// <summary>
        /// it is indicate how many item it is mathing from the begin of the active edge
        /// </summary>
        private int Active_Length { get; set; }
        /// <summary>
        /// it keeps the number of how many suffix sequence remain to insert in the tree
        /// </summary>
        private int Remainder { get; set; }
        /// <summary>
        /// this variable is indicate whether current created node needs to be the suffix link
        /// </summary>
        private bool NeedSuffixLink { get; set; }
    }
}

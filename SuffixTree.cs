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
                                    Active_Edge = sequence[i - Active_Length];// and we use active length to find the active edge , actually i think this if statement is useless ,
                                }                                       // if the avtive node is root , the active length and active edge must be renewed
                                else
                                {
                                    if (Active_Node.Link != null)
                                    {
                                        Active_Node = Active_Node.Link; // if the active node is not root and it has a link node ,we trun to the link node to repeat . do not change the active lenth and the avtive node
                                    }
                                    else
                                    {                        // if it has no link node ,we turn to the root node ,meanwhile we renew the active length and edge
                                        Active_Node = Root;
                                        Active_Length = Remainder - 1;
                                        Active_Edge = sequence[i - Active_Length];
                                    }
                                }
                            }
                        }
                    }
                    else
                    {                                           // if the length is not 0; we need to find the maching item from the begining of the active edge
                        if (Active_Length >= Active_Node.Edges[Active_Edge].GetLength()) // if true it means that we need to change the active node and renew the length and edge
                        {
                            Active_Length -= Active_Node.Edges[Active_Edge].GetLength();
                            Active_Node = Active_Node.Edges[Active_Edge].Next;
                            Active_Edge = Active_Length == 0 ? '\0' : sequence[i - Active_Length];
                        }
                        else                                     // after we find the maching items then we need to judge the current item is the same with the item in the tree
                        {
                            if (sequence[Active_Node.Edges[Active_Edge].From + Active_Length] == currentItem) //if they are the same we just increase the length and break the loop 
                            {
                                Active_Length++;
                                break;
                            }
                            else        // if not we need to create a new node to insert to the current edge and turn the edge into two edges
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
                                Remainder--;      // after we finish this  we need to decrease the Remainder
                                if (NeedSuffixLink) // juder whether this node is a suffix link
                                {
                                    if (oldNode == null) continue;
                                    oldNode.Link = addNode;
                                    oldNode = addNode;
                                }
                                else   // if not we need to change the value of the NeedSuffixLink variable and store the addNode to the oldNode
                                {
                                    NeedSuffixLink = true;
                                    oldNode = addNode;
                                }
                                if (Active_Node == Root) // the same with before
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

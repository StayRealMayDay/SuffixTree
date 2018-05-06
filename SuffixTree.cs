using System;
using System.Collections.Generic;

namespace SuffixTree
{
    public class SuffixTree
    {
        public SuffixTree()
        {
            Root = new Node();
            ActiveEdge = '\0';
            ActiveNode = Root;
            ActiveLength = 0;
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
                    if (ActiveLength == 0
                    ) // if the active length is 0,it means that we have no maching item from the active node yet
                    {
                        if (ActiveNode.Edges.ContainsKey(currentItem)
                        ) // so if the avtive node has an edge contain current item
                        {
                            // we do nothing but set the active edge to the current item and active length plus 1
                            ActiveEdge = currentItem; // after done this, we break this loop and take next item
                            ActiveLength++;
                            break;
                        }
                        else //if not , we need to add an edge to the active node and the key of this edge is current item
                        {
                            ActiveNode.Edges.Add(currentItem, new Edge(i, -1));
                            Remainder--; // every time we insert an edge we need to decrease the Remainder variable
                            if (Remainder == 0
                            ) // if the Remainder is 0, it indicate that we have no suffix sequence wait to insert , so 
                            {
                                // we just break this loop
                                break;
                            }
                            else
                            {
                                if (ActiveNode == Root) // if not, we need to  judge whether the active node is root
                                {
                                    // if it is the root node ,we need to recover the active length
                                    ActiveLength = Remainder - 1;
                                    ActiveEdge =
                                        sequence
                                            [i - ActiveLength]; // and we use active length to find the active edge , actually i think this if statement is useless ,
                                } // if the avtive node is root , the active length and active edge must be renewed
                                else
                                {
                                    if (ActiveNode.Link != null)
                                    {
                                        ActiveNode =
                                            ActiveNode
                                                .Link; // if the active node is not root and it has a link node ,we trun to the link node to repeat . do not change the active lenth and the avtive node
                                    }
                                    else
                                    {
                                        // if it has no link node ,we turn to the root node ,meanwhile we renew the active length and edge
                                        ActiveNode = Root;
                                        ActiveLength = Remainder - 1;
                                        ActiveEdge = sequence[i - ActiveLength];
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // if the length is not 0; we need to find the maching item from the begining of the active edge
                        if (ActiveLength >= ActiveNode.Edges[ActiveEdge].GetLength()
                        ) // if true it means that we need to change the active node and renew the length and edge
                        {
                            ActiveLength -= ActiveNode.Edges[ActiveEdge].GetLength();
                            ActiveNode = ActiveNode.Edges[ActiveEdge].Next;
                            ActiveEdge = ActiveLength == 0 ? '\0' : sequence[i - ActiveLength];
                        }
                        else // after we find the maching items then we need to judge the current item is the same with the item in the tree
                        {
                            if (sequence[ActiveNode.Edges[ActiveEdge].From + ActiveLength] == currentItem
                            ) //if they are the same we just increase the length and break the loop 
                            {
                                ActiveLength++;
                                break;
                            }
                            else // if not we need to create a new node to insert to the current edge and turn the edge into two edges
                            {
                                Node addNode = new Node();
                                addNode.Edges.Add(sequence[ActiveNode.Edges[ActiveEdge].From + ActiveLength],
                                    new Edge(ActiveNode.Edges[ActiveEdge].From + ActiveLength,
                                        ActiveNode.Edges[ActiveEdge].To));
                                addNode.Edges.Add(currentItem, new Edge(i, -1));
                                addNode.Edges[sequence[ActiveNode.Edges[ActiveEdge].From + ActiveLength]].Next =
                                    ActiveNode.Edges[ActiveEdge].Next;
                                ActiveNode.Edges[ActiveEdge].Next = addNode;
                                ActiveNode.Edges[ActiveEdge].To = ActiveNode.Edges[ActiveEdge].From + ActiveLength;
                                Remainder--; // after we finish this  we need to decrease the Remainder
                                if (NeedSuffixLink) // juder whether this node is a suffix link
                                {
                                    if (oldNode == null) continue;
                                    oldNode.Link = addNode;
                                    oldNode = addNode;
                                }
                                else // if not we need to change the value of the NeedSuffixLink variable and store the addNode to the oldNode
                                {
                                    NeedSuffixLink = true;
                                    oldNode = addNode;
                                }

                                if (ActiveNode == Root) // the same with before
                                {
                                    ActiveLength = Remainder - 1;
                                    ActiveEdge = sequence[i - ActiveLength];
                                }
                                else
                                {
                                    if (ActiveNode.Link != null)
                                    {
                                        ActiveNode = ActiveNode.Link;
                                    }
                                    else
                                    {
                                        ActiveNode = Root;
                                        ActiveLength = Remainder - 1;
                                        ActiveEdge = sequence[i - ActiveLength];
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// get the gap of each sequence
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string Gap(int length)
        {
            //if length == MaxValue it means that this sequence has no branch,
            // so this edge.Next is null
            if (length == int.MaxValue)
            {
                return "-";
            }
            //calculate the gap depende on the sequence length
            var a = "";
            for (int i = 0; i < length; i++)
            {
                a += "--";
            }

            return a;
        }
        
        /// <summary>
        /// this function turn the sequence to a string
        /// you need provide start position , stop position and
        /// the list of data ,
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string TransformSequenceToString(int from, int to, List<char> data)
        {
            // is the parameter to is -1 , it means this sequence is to the end of the data.
            var a = "";
            if (to == -1)
            {
                to = from + 1;
            }

            for (int i = from; i < to; i++)
            {
                a += " " + data[i];
            }

            return a;
        }

        
        private void Display(Node node, string gap, List<char> data)
        {
            if (node != null)
            {
                foreach (var item in node.Edges)
                {
//                    Console.WriteLine(gap + item.Key + "(" + item.Value.From + "," + item.Value.To + ")");
                    Console.Write(gap + "(" + node.ProbabilityDic[item.Key] + ")" + TransformSequenceToString(item.Value.From, item.Value.To, data));
                    if (item.Value.Next != null)
                    {
                        Console.WriteLine("(" + item.Value.Next.Support + ")");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                    Display(item.Value.Next, Gap(item.Value.GetLength()) + gap, data);
                }
            }
        }

        public void PrintTree(List<char> data)
        {
            Root.CalculateSupport();
            Display(Root, "--", data);
        }

//        public int GetSupport(Node node)
//        {
//            var support = 0;
//            if (node == null)
//            {
//                return 1;
//            }
//            else
//            {
//                foreach (var keyValue in node.Edges)
//                {
//                    var temp = GetSupport(keyValue.Value.Next);
//                    node.SupportDic.Add(keyValue.Key, temp);
//                    support += temp;
//                }
//
//                node.Support = support;
//                return support;
//            }
//        }

        /// <summary>
        /// Root node
        /// </summary>
        public Node Root { get; }

        /// <summary>
        /// avtive edge is a char date which indicate the active edge of the active node
        /// </summary>
        private char ActiveEdge { get; set; }

        /// <summary>
        /// it is a node indicate the active node
        /// </summary>
        private Node ActiveNode { get; set; }

        /// <summary>
        /// it is indicate how many item it is mathing from the begin of the active edge
        /// </summary>
        private int ActiveLength { get; set; }

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
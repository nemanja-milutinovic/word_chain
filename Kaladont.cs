using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaladont
{
    class Kaladont
    {
        static string fileName = System.IO.Path.GetFullPath(@"..\..\") + Path.DirectorySeparatorChar + "reci.txt";
        static HashSet<Node> allNodes = new HashSet<Node>();
        static Dictionary<string, HashSet<Node>> NodesThatStartWith = new Dictionary<string, HashSet<Node>>();
        static Dictionary<string, HashSet<Node>> NodesThatEndWith = new Dictionary<string, HashSet<Node>>();

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            using (FileStream fs = File.OpenRead(fileName))
            using (StreamReader sr = new StreamReader(fs))
            {
                String line;
                while ((line = sr.ReadLine()) != null) 
                {
                    Node node = new Node { Word = line, ConnectedNodesIn = new HashSet<Node>(), ConnectedNodesOut = new HashSet<Node>()};
                    allNodes.Add(node);

                    if (NodesThatStartWith.ContainsKey(line.Substring(0, 2)))
                    {
                        NodesThatStartWith[line.Substring(0, 2)].Add(node);
                    }
                    else 
                    {
                        HashSet<Node> hs = new HashSet<Node>();
                        hs.Add(node);
                        NodesThatStartWith.Add(line.Substring(0, 2), hs);
                    }

                    if (NodesThatEndWith.ContainsKey(line.Substring(line.Length-2)))
                    {
                        NodesThatEndWith[line.Substring(line.Length - 2)].Add(node);
                    }
                    else
                    {
                        HashSet<Node> hs = new HashSet<Node>();
                        hs.Add(node);
                        NodesThatEndWith.Add(line.Substring(line.Length - 2), hs);
                    }
                }
                   
            }

            // Create graph
            foreach (Node n in allNodes.ToList()) 
            {
                if (NodesThatStartWith.ContainsKey(n.Word.Substring(n.Word.Length - 2)))
                {
                    n.ConnectedNodesOut = NodesThatStartWith[n.Word.Substring(n.Word.Length - 2)];
                }
                if (NodesThatEndWith.ContainsKey(n.Word.Substring(0, 2)))
                {
                    n.ConnectedNodesIn = NodesThatEndWith[n.Word.Substring(0, 2)];
                }
            }

            // Remove Nodes With 0 Ins and 0 Outs
            foreach (Node n in allNodes.ToList()) 
            {
                if (n.ConnectedNodesIn.Count == 0 && n.ConnectedNodesOut.Count == 0)
                    allNodes.Remove(n);
            }

            // Remove Recursive Nodes
            foreach (Node n in allNodes.ToList())
            {
                foreach (Node nn in n.ConnectedNodesOut.ToList()) 
                {
                    if (n.Word.Equals(nn.Word)) 
                    {
                        n.ConnectedNodesOut.Remove(nn);
                    }
                }
            }

            IAlgoSearch algo = new DFS();

            foreach (Node n in allNodes) 
            {
                algo.Start(n);
            }

            sw.Stop();

            Logger.WriteLine(sw.Elapsed.TotalSeconds.ToString());
            Logger.SaveLog(true);

            Console.ReadKey();
        }

        private static HashSet<Node> GetConnectedNodes(string currentWord) 
        {
            string wordSufix = currentWord.Substring(currentWord.Length - 2);

            if (NodesThatStartWith.ContainsKey(wordSufix))
                return NodesThatStartWith[wordSufix];

            HashSet<Node> retVal = new HashSet<Node>();
            foreach (Node n in allNodes) 
            {
                if (n.Word.StartsWith(wordSufix) && !n.Word.Equals(currentWord)) 
                {
                    retVal.Add(n);
                }
            }

            NodesThatStartWith.Add(wordSufix, retVal);
            return retVal;
        }

    }
    
}

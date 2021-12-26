using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaladont
{
    class DFS : IAlgoSearch
    {
        static int MAX_STACK_LENGTH = 20000;
        static int maxResultStackCount = 0;

        public void Start(Node startNode)
        {
            HashSet<Node> visited = new HashSet<Node>();
            Stack<Node> stack = new Stack<Node>();

            Search(startNode, visited, stack);
        }

        public void Search(Node n, HashSet<Node> visited, Stack<Node> currStack, bool topCall = false)
        {
            if (currStack.Count >= MAX_STACK_LENGTH)
            {
                return;
            }

            currStack.Push(n);
            visited.Add(n);

            if (n.ConnectedNodesOut.Count == 0 || Util.AreAllVisited(n.ConnectedNodesOut, visited))
            {
                if (currStack.Count > maxResultStackCount)
                {
                    maxResultStackCount = currStack.Count;
                    Stack<Node> printStack = new Stack<Node>(currStack);
                    Util.PrintStack(printStack);
                    Logger.SaveLog(true);
                }

                currStack.Pop();
                visited.Remove(n);

                return;
            }

            foreach (Node nn in n.ConnectedNodesOut)
            {
                if (!visited.Contains(nn)) 
                {
                    Search(nn, visited, currStack);
                }
            }

            currStack.Pop();
            visited.Remove(n);
        }
    }
}

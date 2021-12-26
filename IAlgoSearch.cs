using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaladont
{
    interface IAlgoSearch
    {
        void Start(Node startNode);
        void Search(Node n, HashSet<Node> visited, Stack<Node> currStack, bool topCall = false);
    }
}

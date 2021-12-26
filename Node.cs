using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaladont
{
    public class Node
    {
        public string Word { get; set; }
        public HashSet<Node> ConnectedNodesIn { get; set; }
        public HashSet<Node> ConnectedNodesOut { get; set; }
    }
}

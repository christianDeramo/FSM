using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMc
{
    class PriorityQueue
    {
        private Dictionary<Node, int> items;

        public bool IsEmpty { get { return items.Count == 0; } }

        public PriorityQueue()
        {
            items = new Dictionary<Node, int>();
        }

        public void Enqueue(Node node, int priority)
        {
            items.Add(node, priority);
        }

        public Node Dequeue()
        {
            int lowestPriority = int.MaxValue;
            Node node = null;

            foreach (Node item in items.Keys)
            {
                int currentPriority = items[item];
                if (currentPriority < lowestPriority)
                {
                    lowestPriority = currentPriority;
                    node = item;

                }
            }
            items.Remove(node);
            return node;

        }
    }
}

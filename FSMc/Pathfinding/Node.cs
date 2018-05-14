using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMc
{
    class Node
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Cost { get; private set; }
        public List<Node> Neighbours { get; private set; }


        public Node(int x, int y, int cost)
        {
            X = x;
            Y = y;
            Cost = cost;
            Neighbours = new List<Node>();
        }

        public void SetCost(int newCost)
        {
            Cost = newCost;
        }

        public void AddNeigbhour(Node node)
        {
            Neighbours.Add(node);
        }

    }
}

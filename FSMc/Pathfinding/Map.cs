using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace FSMc
{
    class Map
    {
        private Dictionary<Node, Node> cameFrom;
        private Dictionary<Node, int> costSoFar;
        private PriorityQueue frontier;

        private int width;
        private int height;
        private int[] cells;
        public Node[] Nodes { get; private set; }

        private Sprite sprite;

        public Map(int width, int height, int[] cells)
        {
            this.width = width;
            this.height = height;
            this.cells = cells;

            sprite = new Sprite(1, 1);
            Nodes = new Node[cells.Length];

            //build the nodes from cells
            for (int i = 0; i < cells.Length; i++)
            {
                int x = i % width;
                int y = i / width;

                if (cells[i] == 0)
                    Nodes[i] = new Node(x, y, int.MaxValue);
                else
                    Nodes[i] = new Node(x, y, 1);
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;
                    if (Nodes[index] == null)
                        continue;
                    CheckNeighbour(Nodes[index], x, y - 1);
                    CheckNeighbour(Nodes[index], x - 1, y);
                    CheckNeighbour(Nodes[index], x + 1, y);
                    CheckNeighbour(Nodes[index], x, y + 1);

                }
            }
        }

        public void CheckNeighbour(Node currentNode, int cellX, int cellY)
        {
            if (cellX < 0 || cellX >= width)
                return;

            if (cellY < 0 || cellY >= height)
                return;

            int index = cellY * width + cellX;
            Node neighbour = Nodes[index];

            if (neighbour.Cost == 1)
            {
                currentNode.AddNeigbhour(neighbour);
            }

        }

        public int Heuristic(Node start, Node end)
        {
            return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
        }

        public void AStar(Node start, Node end)
        {
            cameFrom = new Dictionary<Node, Node>();
            costSoFar = new Dictionary<Node, int>();
            frontier = new PriorityQueue();
            cameFrom[start] = start;
            costSoFar[start] = 0;
            frontier.Enqueue(start, Heuristic(start, end));
            Node currentNode = start;

            while (!frontier.IsEmpty)
            {
                Node currNode = frontier.Dequeue();
                if (currNode == end)
                    return;

                foreach (Node nextNode in currNode.Neighbours)
                {
                    int newCost = costSoFar[currNode] + nextNode.Cost;
                    if (!costSoFar.ContainsKey(nextNode) || costSoFar[nextNode] > costSoFar[currNode] + nextNode.Cost)
                    {
                        cameFrom[nextNode] = currNode;
                        costSoFar[nextNode] = newCost;
                        int priority = newCost + Heuristic(nextNode, end);
                        frontier.Enqueue(nextNode, priority);
                    }
                }
            }
        }
        
        public Node GetNode(int x, int y)
        {
            if (x >= width || x < 0)
                return null;
            if (y >= height || y < 0)
                return null;
            int index = y * width + x;

            return Nodes[index];
        }

        public List<Node> GetPath(int startX, int startY, int endX, int endY)
        {
            List<Node> path = new List<Node>();

            Node startNode = GetNode(startX, startY);
            Node endNode = GetNode(endX, endY);

            AStar(startNode, endNode);
            Node currNode = endNode;

            while (cameFrom[currNode] != currNode)
            {
                if (currNode.Cost > 1)
                {
                    path = null;
                    break;
                }

                path.Add(currNode);
                currNode = cameFrom[currNode];
            }

            if (path != null)
                path.Reverse();

            return path;
        }   

        public void ReverseNode(int x, int y)
        {
            int index = y * width + x;

            if (Nodes[index].Cost > 1)
                Nodes[index].SetCost(1);

            else
                Nodes[index].SetCost(int.MaxValue);
        }

        public bool CheckPath(List<Node> path, int x, int y)
        {
            int index = y * width + x;

            foreach (var node in path)
            {
                if (node.Cost > 1)
                    return true;
            }

            return false;
        }
        
        public void Draw()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    sprite.position = new Vector2(x, y);
                    if (GetNode(x, y).Cost > 1)
                        sprite.DrawSolidColor(0, 0, 0, 1);
                    else
                        sprite.DrawSolidColor(Vector4.One);
                }
            }
        }
    }
}

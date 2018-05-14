using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMc
{
    class Agent
    {
        protected Sprite sprite;
        public float Speed;
        public int X { get { return Convert.ToInt32(Owner.Position.X); } }
        public int Y { get { return Convert.ToInt32(Owner.Position.Y); } }
        public Actor Owner { get; private set; }
        public Node Target;

        private List<Node> path;
        private Node currNode;

        public Agent(Actor owner, float speed = 3)
        {
            Owner = owner;
            Speed = speed;
            Target = null;
        }
        
        public void SetPath(List<Node> path)
        {
            if (path != null)
            {
                this.path = path;
                Target = path[0];
                path.RemoveAt(0);
            }

            else
                Target = null;
        }

        public void SetTarget(Node target)
        {
            if (target != null)
                Target = target;

            else if (path != null)
            {
                target = path[0];
                path = null;
            }
        }

        public void ResetPath()
        {
            if (path != null)
                path.Clear();

            Target = null;
        }

        public Node GetLastNode()
        {
            if (path.Count > 0)
                return path.Last();

            return null;
        }

        public void Update(float deltaTime)
        {
            if (Target != null)
            {
                Vector2 destination = new Vector2(Target.X, Target.Y);
                float distance = (destination - Owner.Position).Length;

                if (distance < 0.02f)
                {
                    Owner.Position = destination;

                    if (path.Count > 0)
                    {
                        Target = path[0];
                        path.RemoveAt(0);
                    }

                    else
                        Target = null;
                }

                else
                {
                    Vector2 direction = (destination - sprite.position).Normalized();
                    Owner.Position += direction * deltaTime *  Speed;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
   

    public class Tree : Node
    {
        public Tree(string name) : base(name) { }
            public override State Process()
        {
            while (currentchild < children.Count)
            {
                var state = children[currentchild].Process();
                if(state!= State.success)
                {
                    return state;
                }
                currentchild++;
            }
            return State.success;
        }
        }


    public class RepeaterDecorator : Node
    {
        
        private bool check;

        public RepeaterDecorator(string name, bool check) : base(name)
        {
            this.check = check;
        }

        public override State Process()
        {
            if (check)
            {
                var state = children[currentchild].Process();
                if (state == State.success || state == State.failure)
                {
                    reset();
                }
                return State.running;
            }
            reset();
            return State.success;

        }
    }


    public class selector : Node
    {
        public selector(string name) : base(name) { }

        public override State Process()
        {
            
            if (currentchild < children.Count)
            {
                switch (children[currentchild].Process())
                {
                    case State.running:
                        return State.running;
                    case State.success:
                        reset();
                        return State.success;
                    default:
                        currentchild++;
                        return State.running;
                }
            }
            reset();
            return State.failure;
        }
    }
    public class sequence: Node
    {
        public sequence(string name): base(name) { }

        public override State Process()
        {
           
            if (currentchild < children.Count)
            {
                
                
                switch (children[currentchild].Process())
                {
                    case State.running:
                        return State.running;
                    case State.failure:
                        reset();
                        return State.failure;
                    default:
                        currentchild++;
                        return currentchild == children.Count ? State.success : State.running;
                }

            }
            reset();
            return State.success;
            

        }
    }


    public class leaf : Node
    {
        IStrategy strategy;
        public leaf(string name, IStrategy strategy): base(name)
        {
            this.strategy = strategy;
        }
        public override State Process() => strategy.Process();
        public override void reset() => strategy.reset();
        
           



    }
    public class Node 
    {
        public enum State
        {
            success, failure, running
        }
        public string name;
        public List<Node> children = new List<Node>();

        public int currentchild;

        public Node(string name)
        {
            this.name = name;
        }

        public void AddChild(Node child) => children.Add(child);
        public virtual State Process() => children[currentchild].Process();

        public virtual void reset()
        {
            currentchild = 0;
            foreach(var child in children)
            {
                child.reset();
            }
        }




    }
}

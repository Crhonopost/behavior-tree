using Godot;
using System;

namespace BehaviorTree{
    public class BTComposite : BTNode
    {
        private Godot.Collections.Array<BTNode> childs;
        private BTNode currentChild;

        public void setCurrentChild(BTNode child){
            currentChild = child;
        }

        public BTNode getCurrentChild(){
            return currentChild;
        }

        public override void _Ready()
        {
            childs = new Godot.Collections.Array<BTNode>();
            foreach(BTNode c in GetChildren()){
                childs.Add(c);
            }
        }

        public Godot.Collections.Array<BTNode> getChilds(){
            return childs;
        }
    }
}
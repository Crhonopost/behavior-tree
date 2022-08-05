using Godot;
using System;

namespace BehaviorTree.Composite{
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
        public virtual void reset(){
            setCurrentChild(getChilds()[0]);
        }

        public bool HasNext(){
            return getChilds().IndexOf(getCurrentChild()) < getChilds().Count - 1;
        }

        public BTNode Next(){
            return getChilds()[getChilds().IndexOf(getCurrentChild()) + 1];
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

        public void setChilds(Godot.Collections.Array<BTNode> cs){
            childs = cs;
        }
    }
}
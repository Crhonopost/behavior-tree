using Godot;
using System;

namespace BehaviorTree{
    public class BTLeaf : BTNode
    {
        public override BTState Tick(Node agent)
        {
            GD.Print("une node ne fait rien");
            return BTState.RUNNING;
        }
    }
}
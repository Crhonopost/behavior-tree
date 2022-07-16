using Godot;
using System;
//using MonoCustomResourceRegistry;

namespace BehaviorTree.Leaf{
    //[RegisteredType("BTLeaf2", "addons/BehaviorTree/icons/BTLeaf.png", "Node")]
    public class BTLeaf : BTNode
    {
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            return BTState.FAILURE;
        }
    }
}
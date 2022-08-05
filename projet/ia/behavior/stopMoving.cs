using Godot;
using System;
using BehaviorTree.Leaf;

namespace BehaviorTree.mobTest
{
    public class stopMoving : BTLeaf
    {
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            ((mob)agent).CanMove = false;
            return BTState.SUCCESS;
        }
    }
}
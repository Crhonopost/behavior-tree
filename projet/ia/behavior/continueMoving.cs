using Godot;
using System;
using BehaviorTree.Leaf;

namespace BehaviorTree.mobTest
{
    public class continueMoving : BTLeaf
    {
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            ((mob)agent).CanMove = true;
            return BTState.SUCCESS;
        }
    }
}
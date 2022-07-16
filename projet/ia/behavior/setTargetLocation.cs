using Godot;
using System;
using BehaviorTree.Leaf;

namespace BehaviorTree.mobTest
{
    public class setTargetLocation : BTLeaf
    {
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            ((mob)agent).settargetLocation(((mob)agent).Translation);
            return BTState.SUCCESS;
        }
    }
}
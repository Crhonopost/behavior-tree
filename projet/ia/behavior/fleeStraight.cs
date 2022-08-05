using Godot;
using System;

namespace BehaviorTree.Leaf
{
    public class fleeStraight : BTLeaf
    {
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            mob ia = (mob)agent;
            Vector3 target = ia.Translation + (ia.Translation - (Vector3)blackboard.getValue("posJoueur")).Normalized() * 3;
            ia.settargetLocation(target);
            
            return BTState.SUCCESS;
        }
    }
}
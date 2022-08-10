using Godot;
using System;
using BehaviorTree.Leaf;

namespace BehaviorTree.Behavior.basicAIBehavior{
    public class setRandomLocation : BTLeaf
    {
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            GD.Print("new random location");
            ((BasicAI)agent).setNearestMovementTarget(((BasicAI)agent).getRandomLocation(25));
            setState(BTState.SUCCESS);
            return BTState.SUCCESS;
        }
    }
}
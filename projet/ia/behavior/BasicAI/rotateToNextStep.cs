using Godot;
using System;
using BehaviorTree.Leaf;
using BehaviorTree;

public class rotateToNextStep : BTLeaf
{
    public override BTState Tick(Node agent, BTBlackboard blackboard)
    {
        if(((BasicAI)agent).rotateYToward(((BasicAI)agent).getNextStepLocation(), 0)){
            return BTState.SUCCESS;
        }
        else{
            return BTState.RUNNING;
        }
    }
}

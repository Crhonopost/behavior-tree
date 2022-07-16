using Godot;
using System;

namespace BehaviorTree.mobTest{
public class locationReached : BTLeaf
{
    public override BTState Tick(Node agent, BTBlackboard blackboard){
        if(((mob)agent).reachedLocation()){
             GD.Print("location reached");
            return BTState.SUCCESS;
        }
        else{
            return BTState.FAILURE;
        }
    }
}
}
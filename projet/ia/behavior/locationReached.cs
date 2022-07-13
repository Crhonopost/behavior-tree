using Godot;
using System;

namespace BehaviorTree.mobTest{
public class locationReached : BTLeaf
{
    public override BTState Tick(Node agent){
        if(((mob)agent).reachedLocation()){
            return BTState.SUCCESS;
        }
        else{
            return BTState.RUNNING;
        }
    }
}
}
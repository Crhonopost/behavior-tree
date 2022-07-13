using Godot;
using System;

namespace BehaviorTree.mobTest{
    public class setRandomLocation : BTLeaf
    {
        public override BTState Tick(Node agent)
        {
            if(((mob)agent).settargetLocation(((mob)agent).DefineRandomLocation(5f))){
                setState(BTState.SUCCESS);
                return BTState.SUCCESS;
            }
            else{
                setState(BTState.FAILURE);
                return BTState.FAILURE;
            }

        }
    }
}
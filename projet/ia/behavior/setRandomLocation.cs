using Godot;
using System;

namespace BehaviorTree.mobTest{
    public class setRandomLocation : BTLeaf
    {
        public override BTState Tick(Node agent)
        {
            GD.Print("new random location");
            if(((mob)agent).settargetLocation(((mob)agent).DefineRandomLocation(25))){
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
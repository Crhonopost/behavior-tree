using Godot;
using BehaviorTree.Leaf;

namespace BehaviorTree.Behavior.basicAIBehavior{
    public class locationReached : BTLeaf
    {
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            if(((BasicAI)agent).reachedLocation()){
                GD.Print("location reached");
                return BTState.SUCCESS;
            }
            else{
                return BTState.FAILURE;
            }
        }
    }
}
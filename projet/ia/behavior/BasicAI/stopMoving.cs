using Godot;
using BehaviorTree.Leaf;

namespace BehaviorTree.Behavior.basicAIBehavior
{
    public class stopMoving : BTLeaf
    {
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            ((BasicAI)agent).CanMove = false;
            return BTState.SUCCESS;
        }
    }
}
using Godot;
using BehaviorTree.Leaf;

namespace BehaviorTree.Behavior.basicAIBehavior
{
    public class fleeStraight : BTLeaf
    {

        [Export]
        private float range = 1;
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            BasicAI ia = (BasicAI)agent;
            Vector3 target = ia.Translation + (ia.Translation - (Vector3)blackboard.getValue("posJoueur")).Normalized() * range;
            ia.setNearestMovementTarget(target);
            
            return BTState.SUCCESS;
        }
    }
}
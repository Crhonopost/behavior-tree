using Godot;
using BehaviorTree.Leaf;

namespace BehaviorTree.Behavior.basicAIBehavior
{
    public class enemyNearBy:BTLeaf
    {
        [Export]
        private float radius;

        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            if((((BasicAI)agent).Translation - (Vector3)blackboard.getValue("posJoueur")).Length() < radius){
                return BTState.SUCCESS;
            }
            else{
                return BTState.FAILURE;
            }
        }
    }
}
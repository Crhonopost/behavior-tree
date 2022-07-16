using Godot;

namespace BehaviorTree.mobTest
{
    public class enemyNearBy:BTNode
    {
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            if((((mob)agent).Translation - (Vector3)blackboard.getValue("posJoueur")).Length() < 10){
                GD.Print("ennemy near by");
                return BTState.SUCCESS;
            }
            else{
                return BTState.FAILURE;
            }
        }
    }
}
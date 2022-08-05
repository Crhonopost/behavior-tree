using Godot;

namespace BehaviorTree.Composite
{
    public class BTSelector:BTComposite
    {
        public override void _Ready()
        {
            base._Ready();
            reset();
        }

        public override void PreTick(Node agent)
        {
            getCurrentChild().PreTick(agent);
        }

        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            BTState etat = getCurrentChild().Tick(agent, blackboard);
            if(etat == BTState.SUCCESS){
                reset();
                return BTState.SUCCESS;
            }
            else if(etat == BTState.FAILURE){
                if(!HasNext())
                {
                    setCurrentChild(getChilds()[0]);
                    return BTState.FAILURE;
                }
                else
                {
                    setCurrentChild(Next());
                    return BTState.RUNNING;
                }
            }
            else
            {
                return BTState.RUNNING;
            }
        }
    }
}
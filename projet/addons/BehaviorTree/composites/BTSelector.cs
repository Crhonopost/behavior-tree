using Godot;

namespace BehaviorTree.Composite
{
    public class BTSelector:BTComposite
    {
        public override void _Ready()
        {
            base._Ready();
            setCurrentChild(getChilds()[0]);
        }

        public bool HasNext(){
            return getChilds().IndexOf(getCurrentChild()) < getChilds().Count - 1;
        }

        public BTNode Next(){
            return getChilds()[getChilds().IndexOf(getCurrentChild()) + 1];
        }
        public override void PreTick(Node agent)
        {
            getCurrentChild().PreTick(agent);
        }

        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            BTState etat = getCurrentChild().Tick(agent, blackboard);
            if(etat == BTState.SUCCESS){
                setCurrentChild(getChilds()[0]);
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
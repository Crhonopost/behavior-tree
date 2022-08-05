using Godot;

namespace BehaviorTree.Composite{
    public class BTSequence : BTComposite{

        public override void _Ready()
        {
            base._Ready();
            reset();
        }

        public override void PreTick(Node agent)
        {
            getCurrentChild().PreTick(agent);
        }

        public override void PostTick(Node agent)
        {
            getCurrentChild().PostTick(agent);
        }

        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            BTState etat = getCurrentChild().Tick(agent, blackboard);
            BTState result;
            setState(etat);
            if(etat == BTState.FAILURE || etat == BTState.RUNNING){
                result = etat;
                if(etat == BTState.FAILURE){
                    reset();
                }
            }
            else{
                if(HasNext()){
                    setCurrentChild(Next());
                    result = BTState.RUNNING;
                } else
                {
                    reset();
                    result = BTState.SUCCESS;
                }
            }
            return result;

        }
    }
}
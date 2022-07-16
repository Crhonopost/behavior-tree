using Godot;

namespace BehaviorTree.Composite{
    public class BTSequence : BTComposite{

        public BTNode Next(){ // part du principe que l'on a vérifié qu'il y avait un autre child
            return getChilds()[getChilds().IndexOf(getCurrentChild()) + 1];
        }

        public bool HasNext(){
            return getChilds().IndexOf(getCurrentChild()) < getChilds().Count-1;
        }
        public override void _Ready()
        {
            base._Ready();
            setCurrentChild(getChilds()[0]);
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
                    setCurrentChild(getChilds()[0]);
                }
            }
            else{
                if(HasNext()){
                    setCurrentChild(Next());
                    result = BTState.RUNNING;
                } else
                {
                    setCurrentChild(getChilds()[0]);
                    result = BTState.SUCCESS;
                }
            }
            return result;

        }
    }
}
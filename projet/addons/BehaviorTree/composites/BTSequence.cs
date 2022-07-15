using Godot;

namespace BehaviorTree{
    public class BTSequence : BTComposite{

        public BTNode Next(){ // sort le prochain enfant ou null s'il n'y en a pas
            int currentI = getChilds().IndexOf(getCurrentChild());
            if(!HasNext()){
                return null;
            }
            else{
                return getChilds()[currentI + 1];
            }
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

        public override BTState Tick(Node agent)
        {
            BTState etat = getCurrentChild().Tick(agent);
            BTState result;
            setState(etat);
            if(etat == BTState.FAILURE || etat == BTState.RUNNING){
                result = etat;
                if(etat == BTState.FAILURE){
                    setCurrentChild(getChilds()[0]);
                }
            }
            else{
                BTNode next = Next();
                if(next == null){
                    setCurrentChild(getChilds()[0]);
                    result = BTState.SUCCESS;
                }
                else{
                    setCurrentChild(next);
                    result = BTState.RUNNING;
                }
            }
            return result;

        }
    }
}
using Godot;

namespace BehaviorTree{
    public class BTSequence : BTComposite{

        public BTNode Next(){ // sort le prochain enfant ou null s'il n'y en a pas
            int currentI = getChilds().IndexOf(getCurrentChild());
            if(currentI == getChilds().Count-1){
                return null;
            }
            else{
                return getChilds()[currentI + 1];
            }

        }
        public override void _Ready()
        {
            base._Ready();
            setCurrentChild(getChilds()[0]);
        }

        public override BTState Tick(Node agent)
        {
            BTState etat = getCurrentChild().Tick(agent);
            if(etat == BTState.FAILURE || etat == BTState.RUNNING){
                return etat;
            }
            else{
                BTNode next = Next();
                if(next == null){
                    return BTState.SUCCESS;
                }
                else{
                    setCurrentChild(next);
                    return BTState.RUNNING;
                }
            }

        }
    }
}
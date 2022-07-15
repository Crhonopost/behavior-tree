using Godot;
using System;
//using MonoCustomResourceRegistry;


/*
liste des nodes à implémenter:
 - composite:
   * sequence (enchaine les success, si failure: ?)
   * selector/fallback (s'arrete au premier success, si failure : passe au suivant)
   * leur variante random
   * node qui en execute pusieur en parallele maybe?
 - decorator:
   * inverter (inverse le resultat de son enfant, si running : ?)
   * repeater (infinit, certain nb de fois)
   * repeatUntilFail (c'est dit dans le nom ...)
   * j'avais vu un wait, jsp si ca peut etre utile

se renseigner sur les stacks
*/

namespace BehaviorTree{
    //[RegisteredType("BTNode", "addons/BehaviorTree/icons/BTNode.png", "Node")]
    public class BTNode: Node
    {
        /*
        une node est appelée à chaque tick (run)
        - seul les leafs appellent une fonction pour agir directement sur le monde
        - vue que je mets plus delta dans l'appel de fonction, il faudra gérer ca différemment
        */
        [Signal]
        public delegate BTState changingState(BTState state);

        private BTState state;

        public BTState getState(){
            return state;
        }

        protected void setState(BTState newState){
            state = newState;
            EmitSignal("changingState", newState);
        }

        public virtual BTState Tick(Node agent){ // manque le blackboard
            GD.Print("une node ne fait rien");
            return BTState.FAILURE;
        }

        public virtual void PreTick(Node agent) // pas sur de l'implémentation mais cette methode sera appellée avant Tick() 
        {}

        public virtual void PostTick(Node agent){}// appellée apres Tick()
    }
}
using Godot;
using System;

namespace BehaviorTree.Decorator
{
    public class BTInverter : BTDecorator // le running devient success
    {
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            switch(child.Tick(agent, blackboard))
            {
                case BTState.SUCCESS:   {return BTState.FAILURE;}
                case BTState.RUNNING:   {return BTState.RUNNING;}
                case BTState.FAILURE:   {return BTState.SUCCESS;}
            }
            GD.Print("erreur");
            return BTState.FAILURE;
        }
    }
}
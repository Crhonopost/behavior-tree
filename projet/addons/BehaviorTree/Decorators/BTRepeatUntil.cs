using Godot;
using System;
using BehaviorTree.Leaf;

namespace BehaviorTree.Decorator
{
    public class BTRepeatUntil : BTDecorator
    {
        [Export]
        private NodePath conditionPath;
        private BTNode condition; // il faut remplir la methode check() pour que la condition puisse t etre vérifiée

        public override void _Ready()
        {
            base._Ready();
            condition = (BTNode) GetNode(conditionPath);
        }
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            if(condition.Tick(agent, blackboard) != BTState.SUCCESS){
                child.Tick(agent, blackboard);
                return BTState.RUNNING;
            }
            else{
                return BTState.SUCCESS;
            }
        }
    }
}
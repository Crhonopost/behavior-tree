using Godot;
using System;

namespace BehaviorTree.Decorator{
    public class BTDecorator : BTNode
    {
        protected BTNode child;

        public override void _Ready()
        {
            child = GetChild<BTNode>(0);
        }
    }
}
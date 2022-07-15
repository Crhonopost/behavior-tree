using Godot;
using System;

namespace BehaviorTree{
    public class BTDecorator : BTNode
    {
        protected BTNode child;

        public override void _Ready()
        {
            child = GetChild<BTNode>(0);
        }
    }
}
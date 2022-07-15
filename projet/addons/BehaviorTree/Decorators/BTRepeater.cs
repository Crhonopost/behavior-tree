
using Godot;

namespace BehaviorTree{
    public class BTRepeater:BTDecorator
    {
        public override void PreTick(Node agent)
        {
            child.PreTick(agent);
        }
        public override BTState Tick(Node agent)
        {
            child.Tick(agent);
            return BTState.RUNNING;
        }
        public override void PostTick(Node agent)
        {
            child.PostTick(agent);
        }
    }
}

using Godot;

namespace BehaviorTree{
    public class BTRepeater:BTDecorator
    {
        public override void PreTick(Node agent)
        {
            child.PreTick(agent);
        }
        public override BTState Tick(Node agent, BTBlackboard blackboard)
        {
            child.Tick(agent, blackboard);
            return BTState.RUNNING;
        }
        public override void PostTick(Node agent)
        {
            child.PostTick(agent);
        }
    }
}
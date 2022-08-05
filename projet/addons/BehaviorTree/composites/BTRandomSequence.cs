using Godot;
using System;

namespace BehaviorTree.Composite{
    public class BTRandomSequence : BTSequence
    {
        public override void reset()
        {
            Godot.Collections.Array<BTNode> bufferChilds = getChilds();
            bufferChilds.Shuffle();
            setChilds(bufferChilds);
            setCurrentChild(getChilds()[0]);
        }
    }
}
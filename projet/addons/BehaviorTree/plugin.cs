using Godot;
using System;

[Tool]
public class plugin : EditorPlugin
{
    public override void _EnterTree()
    {
        AddCustomType("BehaviorTree", "Node", GD.Load<Script>("addons/BehaviorTree/BasicNode/BT.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BT.png"));
        AddCustomType("BTNode", "Node", GD.Load<Script>("addons/BehaviorTree/BasicNode/BTNode.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTNode.png"));
        AddCustomType("BTLeaf", "Node", GD.Load<Script>("addons/BehaviorTree/BasicNode/BTLeaf.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTLeaf.png"));
        AddCustomType("BTSequence", "Node", GD.Load<Script>("addons/BehaviorTree/composites/BTSequence.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTSequence.png"));
        AddCustomType("BTRandomSequence", "Node", GD.Load<Script>("addons/BehaviorTree/composites/BTRandomSequence.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTSequence.png"));
        AddCustomType("BTRepeater", "Node", GD.Load<Script>("addons/BehaviorTree/Decorators/BTRepeater.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTRepeater.png"));
        AddCustomType("BTBlackboard", "Node", GD.Load<Script>("addons/BehaviorTree/BTBlackboard.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTBlackBoard.png"));
        AddCustomType("BTSelector", "Node", GD.Load<Script>("addons/BehaviorTree/composites/BTSelector.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTSelector.png"));
        AddCustomType("BTRandomSelector", "Node", GD.Load<Script>("addons/BehaviorTree/composites/BTRandomSelector.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTSelector.png"));
        AddCustomType("BTRepeatUntil", "Node", GD.Load<Script>("addons/BehaviorTree/Decorators/BTRepeatUntil.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTNode.png"));
        AddCustomType("BTInverter", "Node", GD.Load<Script>("addons/BehaviorTree/Decorators/BTInverter.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTNode.png"));
    }

    public override void _ExitTree()
    {
        RemoveCustomType("BTInverter");
        RemoveCustomType("BTRepeatUntil");
        RemoveCustomType("BTRandomSelector");
        RemoveCustomType("BTSelector");
        RemoveCustomType("BTRepeater");
        RemoveCustomType("BTRandomSequence");
        RemoveCustomType("BTSequence");
        RemoveCustomType("BTLeaf");
        RemoveCustomType("BehaviorTree");
        RemoveCustomType("BTBlackboard");
        RemoveCustomType("BTNode");
    }
}

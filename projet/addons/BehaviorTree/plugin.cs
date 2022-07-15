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
        AddCustomType("BTSequence", "Node", GD.Load<Script>("addons/BehaviorTree/composites/BTSequence.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTNode.png"));
        AddCustomType("BTRepeater", "Node", GD.Load<Script>("addons/BehaviorTree/Decorators/BTRepeater.cs"), GD.Load<Texture>("addons/BehaviorTree/icons/BTNode.png"));
    }

    public override void _ExitTree()
    {
        RemoveCustomType("BTRepeater");
        RemoveCustomType("BTSequence");
        RemoveCustomType("BTLeaf");
        RemoveCustomType("BehaviorTree");
        RemoveCustomType("BTNode");
    }
}

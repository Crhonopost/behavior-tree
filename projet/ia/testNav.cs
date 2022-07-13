using Godot;
using System;

public class testNav : Spatial
{
    private Navigation nav;
    private Camera cam;
    private mob ia1;
    private System.Collections.Queue pathIa1;

    public override void _Ready()
    {
        nav = GetNode<Navigation>("Navigation");
        cam = GetNode<Camera>("Camera");
        ia1 = GetNode<mob>("mob");
        pathIa1 = new System.Collections.Queue();
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event is InputEventMouseButton){
            Vector2 mousePose = ((InputEventMouse) @event).Position;
            Vector3 from = cam.ProjectRayOrigin(mousePose);
            Vector3 to = from + cam.ProjectRayNormal(mousePose) * 200;
            ia1.settargetLocation(nav.GetClosestPointToSegment(from, to));
        }
    }
    public override void _PhysicsProcess(float delta)
    {
    }
}

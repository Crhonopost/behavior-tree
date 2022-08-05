using Godot;
using System;

public class BigMobModel : Spatial
{
    public override void _PhysicsProcess(float delta)
    {
        Vector3 direction = Vector3.Zero;
        if(Input.IsActionPressed("ui_up")){
            direction += Vector3.Forward;
        }
        if(Input.IsActionPressed("ui_down")){
            direction += Vector3.Back;
        }
        if(Input.IsActionPressed("ui_left")){
            direction += Vector3.Left;
        }
        if(Input.IsActionPressed("ui_right")){
            direction += Vector3.Right;
        }
        Translate(direction * delta * 5);
        GetNode<frontRightIK>("Armature/Skeleton/frontRightIK").updateRayDirection(direction);
    }

    /*
    private Skeleton skel;
    private SkeletonIK frontRightTip;
    private RayCast frontRightRay;
    private Spatial frontRightTarget;
    public override void _EnterTree()
    {
        skel = GetNode<Skeleton>("Armature/Skeleton");
        frontRightTip = GetNode<SkeletonIK>("Armature/Skeleton/frontRightIK");
        frontRightTip.Start();
        frontRightTarget = new Spatial();
        frontRightRay = GetNode<RayCast>("frontRightRay");
    }

    public override void _Ready()
    {
        GetParent().CallDeferred("add_child",(frontRightTarget));
        frontRightTarget.Connect("ready", this, "initTarget");
    }

    public void initTarget(){
        frontRightTip.TargetNode = frontRightTarget.GetPath();
    }

    public override void _PhysicsProcess(float delta)
    {
        if(Input.IsActionPressed("ui_up")){
            Translate(Vector3.Forward * delta);
        }
        if(Input.IsActionPressed("ui_down")){
            Translate(Vector3.Back * delta);
        }

        float distance = (skel.GlobalTransform.origin + skel.GetBoneGlobalPoseNoOverride(skel.FindBone("ForeArm.R")).origin - frontRightTarget.GlobalTransform.origin).Length();
        if(distance > 14.75f){
            if(frontRightRay.IsColliding()){
                Transform targetPos = frontRightTarget.Transform;
                targetPos.origin = frontRightRay.GetCollisionPoint();
                frontRightTarget.GlobalTransform = targetPos;
            }
        }
        GetNode<CSGSphere>("../show").GlobalTransform = frontRightTarget.Transform;
    }
    public override void _ExitTree()
    {
        frontRightTip.Stop();
    }
    */
}

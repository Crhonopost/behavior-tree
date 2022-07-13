using Godot;
using System;

public class mob : KinematicBody
{
    private Vector3 targetLocation;
    private Navigation nav;
    private float speed;
    private MeshInstance representation;
    private float height;

    private System.Collections.Generic.Queue<Vector3> path;

    public override void _Ready()
    {
        nav = GetNode<Navigation>("../Navigation");
        targetLocation = GlobalTransform.origin;
        path = new System.Collections.Generic.Queue<Vector3>();
        speed = 10;
        representation = GetNode<MeshInstance>("../representation");
        height = ((CapsuleShape) GetNode<CollisionShape>("CollisionShape").Shape).Height;
    }

    public override void _PhysicsProcess(float delta)
    {
        if(path.Count > 0){
            Vector3 movement = path.Peek() - Translation;
            movement.y += height/2;
            GetNode<RayCast>("RayCast").CastTo = movement;
            
            movement = movement.Normalized() * speed;
            
            GD.Print(MoveAndSlide(movement));

            if((path.Peek() - Translation).Length() < 1.2){
                path.Dequeue();
            }
        }
    }

    public override void _UnhandledKeyInput(InputEventKey @event)
    {
        if(@event.IsActionPressed("ui_accept")){
            settargetLocation(DefineRandomLocation(50));
        }
    }

    public bool settargetLocation(Vector3 newTargetLocation){ // on entre en parametre le point auquel on veut se rendre, la fct prend le point ateignable le plus proche
        targetLocation = nav.GetClosestPoint(newTargetLocation);
        representation.Translation = targetLocation;
        path.Clear();
        foreach(Vector3 vec in nav.GetSimplePath(Translation, targetLocation)){
            path.Enqueue(vec);
            GD.Print(vec);
        }
        return true;
    }

    public Vector3 DefineRandomLocation(float radius){ // donne un point random dans le rayon radius, la fct ne se soucie aps de savoir si le point est accessible
        Random rng = new Random();
        Vector3 direction = Vector3.Forward * (float) rng.NextDouble() * radius;
        return direction.Rotated(Vector3.Up, Mathf.Deg2Rad(rng.Next(0,360)));
    }

    public bool reachedLocation(){
        return (Translation - targetLocation).Length() < 2;
    }
}

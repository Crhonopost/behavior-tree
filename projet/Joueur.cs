using Godot;
using System;
using BehaviorTree;

public class Joueur : KinematicBody
{
    [Signal]
    private delegate void updatePosJoueur(Vector3 pos);
    private Vector3 velocity;

    [Export]
    private NodePath blackboardPath;


    public override void _Ready()
    {
        if(blackboardPath != null){
            GD.Print("puteee");
            GD.Print(Connect("updatePosJoueur", GetNode(blackboardPath), "setValue"));
        }
        velocity = Vector3.Zero;
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector3 input = Vector3.Zero;
        input.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        input.z = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        input = input.Normalized() * 10;
        velocity.y --;

        velocity.x = input.x;
        velocity.z = input.z;
        
        MoveAndSlide(velocity, Vector3.Up);

        if(IsOnFloor()){
            velocity.y = 0;
        }

        EmitSignal("updatePosJoueur", Translation, "posJoueur");
    }
}

using Godot;
using System;

public class Joueur : KinematicBody
{
    private Vector3 velocity;

    public override void _Ready()
    {
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
    }
}

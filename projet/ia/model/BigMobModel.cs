using Godot;
using System;

public class BigMobModel : Spatial
{
    [Signal]
    delegate void updateSignal(Vector3 dir);

    private ProceduralLegV2 frontRight;
    private ProceduralLegV2 frontLeft;
    private ProceduralLegV2 backRight;
    private ProceduralLegV2 backLeft;

    private enum State{
        IDLE,
        WALKING
    }
    private State currentState;
    private Vector3 direction;

    private ProceduralLegV2[] frontPart;
    private ProceduralLegV2[] backPart;
    
    public override void _Ready()
    {
        frontRight = GetNode<ProceduralLegV2>("Armature/Skeleton/frontRightIK");
        frontLeft = GetNode<ProceduralLegV2>("Armature/Skeleton/frontLeftIK");
        backRight = GetNode<ProceduralLegV2>("Armature/Skeleton/backRightIK");
        backLeft = GetNode<ProceduralLegV2>("Armature/Skeleton/backLeftIK");

        frontRight.Connect("endCourse", this, "alternateFront");
        frontLeft.Connect("endCourse", this, "alternateFront");
        backRight.Connect("endCourse", this, "alternateBack");
        backLeft.Connect("endCourse", this, "alternateBack");

        frontPart = new ProceduralLegV2[2] {frontRight, frontLeft};
        backPart = new ProceduralLegV2[2] {backRight, backLeft};

        currentState = State.IDLE;
        direction = Vector3.Zero;
    }
    public override void _PhysicsProcess(float delta)
    {
        direction = checkDirection();
        updateState();
        Translate(direction * delta * 5);
    }

    public void updateState(){
        if(direction != Vector3.Zero && currentState == State.IDLE){
            frontLeft.setRest(true);
            backRight.setRest(true);
            frontRight.setNextStep();
            backLeft.setNextStep();
            currentState = State.WALKING;
        }
        else if(direction == Vector3.Zero && currentState != State.IDLE){
            currentState = State.IDLE;
            frontLeft.setRest(false);
            frontRight.setRest(false);
            backLeft.setRest(false);
            backRight.setRest(false);
        }
    }

    public Vector3 checkDirection(){
        Vector3 newDirection = Vector3.Zero;
        if(Input.IsActionPressed("ui_up")){
            newDirection += Vector3.Forward;
        }
        if(Input.IsActionPressed("ui_down")){
            newDirection += Vector3.Back;
        }
        if(Input.IsActionPressed("ui_left")){
            turn("LEFT");
            newDirection += Vector3.Left;
        }
        if(Input.IsActionPressed("ui_right")){
            turn("RIGHT");
            newDirection += Vector3.Right;
        }
        return newDirection;
    }

    public void alternate(ProceduralLegV2[] part){ // part represente soit les deux pattes avant ou arrieres
        if(part[0].isResting()){
            part[0].setNextStep();
            part[0].setRest(false);
            part[1].setRest(true);
        }
        else{
            part[1].setNextStep();
            part[1].setRest(false);
            part[0].setRest(true);
        }
    }

    public void alternateFront(){
        alternate(frontPart);
    }

    public void alternateBack(){
        alternate(backPart);
    }

    public void turn(String direction){
        float angle = 0;
        if(direction == "RIGHT"){
            Rotate(Vector3.Up, Mathf.Deg2Rad(-1));
            angle = 90;
        }
        else if(direction == "LEFT"){
            Rotate(Vector3.Up, Mathf.Deg2Rad(1));
            angle = -90;
        }
        frontRight.updateRayDirection(GlobalTransform.basis.z.Rotated(Vector3.Up, angle));
        frontLeft.updateRayDirection(GlobalTransform.basis.z.Rotated(Vector3.Up, angle));

        backRight.updateRayDirection(GlobalTransform.basis.z.Rotated(Vector3.Up, -angle));
        backLeft.updateRayDirection(GlobalTransform.basis.z.Rotated(Vector3.Up, -angle));
    }
}

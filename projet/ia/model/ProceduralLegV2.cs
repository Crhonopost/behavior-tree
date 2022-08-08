using Godot;
using System;

public class ProceduralLegV2 : SkeletonIK
{
    private int footIndex; // index du bone representant le pied
    private Vector3 rayInitDir; // direction d'origine du raycCast
    private Spatial stepTarget; // target qui aura une transition smooth pour définir la manière dont la patte va du precedant target vers le novueau
    private Vector3 finalStepTarget; // position final que doit atteindra la patte pour etre fixé
    private Vector3 footToTarget; // bizarre, si j'essais de rendre cette variable locale a une methode, je peux pas utiliser stepTarget pour ca position
    private RayCast nextStepRay; // raycast qui permet de definir le prochaine emplacement de la patte
    private bool grounded; // dixera la pate au sol des qu'elle l'atteindra pour pas que ca glisse
    private bool rest;
    private Skeleton skel;
    private Vector3 direction;
    
    [Signal]
    public delegate void endCourse(); // emis quand la jambe est censé avoir finit sa course
    [Signal]
    public delegate void touchGround(); // emis quand la jambe touche le sol

    [Signal]
    public delegate void phaseChanged(String name);
    private enum phase{
        CONTACT,
        LOW,
        PASSING,
        HIGH,
        END
    }
    private phase currentPhase;

    [Export]
    private float footToTargetRange; // distance entre le pied et la target a partir de laquelle on cherche un nouvel appui
                                    // je le change, ca represente mtn la distance entre le pied et la base de la jambe
    [Export]
    private NodePath nextStepRayPath; // chemin pour le raycast nextStepRay
    [Export]
    private bool showTarget; // sert pour le debuggage
    private CSGSphere show; // pour le debuggage
    
    public override void _EnterTree()
    {
        stepTarget = new Spatial();
        //GetTree().CurrentScene.CallDeferred("add_child", stepTarget);
        //stepTarget.Connect("ready", this, "initStepTarget");
        stepTarget =(Spatial) GetNode(TargetNode);
        footIndex = GetParent<Skeleton>().FindBone(TipBone);
        nextStepRay = GetNode<RayCast>(nextStepRayPath);
        rayInitDir = nextStepRay.CastTo;
    }

    public override void _Ready()
    {
        footToTargetRange = 1.5f;

        currentPhase = phase.CONTACT;

        rest = false;
        skel = GetParentSkeleton();
        Start();

        if(showTarget){
            show = new CSGSphere();
            show.Radius = 1f;
        }
    }

    private void initStepTarget(){ // je fais avec un signal et tout mais c'est pas la meilleure methode vu que j'ai un message d'erreur la premiere frame
        TargetNode = stepTarget.GetPath();
        GetTree().CurrentScene.AddChild(show);
    }

    public override void _PhysicsProcess(float delta)
    {
        footToTarget = stepTarget.GlobalTransform.origin - skel.ToGlobal(skel.GetBoneGlobalPose(footIndex).origin);
        if(rest){
            int rootBoneIndex = skel.FindBone(RootBone);
            Vector3 restTarget = skel.ToGlobal(skel.GetBoneRest(rootBoneIndex).origin);
            restTarget.y -= 1;
            stepTarget.Translation = stepTarget.Translation.LinearInterpolate(restTarget, delta * 20);
            }
        else{
            stepTarget.Translation = stepTarget.Translation.LinearInterpolate(finalStepTarget, delta * 20);

            if(footToTarget.Length() > footToTargetRange && grounded){
                grounded = false;
                EmitSignal("endCourse");
            }
            else if(footToTarget.Length() < footToTargetRange && !grounded){
                EmitSignal("touchGround");
                grounded = true;
            }
            updatePhase();
        }
    }

    public void setNextStep(){
        finalStepTarget = nextStepRay.GetCollisionPoint();
        if(showTarget){
            show.Translation = finalStepTarget;
        }
    }

    public void setRest(bool state){
        rest = state;
    }
    public bool isResting(){
        return rest;
    }

    public void updatePhase(){
        Vector3 baseToTarget = stepTarget.GlobalTransform.origin - skel.ToGlobal(skel.GetBoneGlobalPose(skel.FindBone(RootBone)).origin);
        if(isInFront(baseToTarget)){
            if(baseToTarget.Length() < 7){
                currentPhase = phase.LOW;
            }
            else{
                currentPhase = phase.CONTACT;
            }
        }
        else{
            if(baseToTarget.Length() < 4){
               currentPhase = phase.PASSING;
            }
            else if (baseToTarget.Length() < 8){
                currentPhase = phase.HIGH;
            }
            else{
                EmitSignal("endCourse");
                currentPhase = phase.END;
            }
        }
        EmitSignal("phaseChanged", currentPhase.ToString());
    }

    public bool isGrounded(){
        return grounded;
    }

    public void updateRayDirection(Vector3 movingDir){ // ca c'st tres moche
        direction = movingDir;
        if(movingDir != Vector3.Zero){
            Vector3 test2 = rayInitDir.Rotated(Vector3.Up, new Vector2(rayInitDir.x, rayInitDir.z).AngleTo(new Vector2(-movingDir.x, movingDir.z)));
            nextStepRay.CastTo = test2;
        }
        else{
            nextStepRay.CastTo = Vector3.Down * nextStepRay.CastTo.Length();
        }
    }

    public bool isInFront(Vector3 testedVec){
        Vector3 normal = direction;
        Vector3 normalToTest = testedVec - normal;
        float dot = normalToTest.Dot(normal); // c'est quoi deja le produit scalaire ...
        return dot > 0;
    }
}

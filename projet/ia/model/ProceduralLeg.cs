using Godot;
using System;

public class ProceduralLeg : SkeletonIK
{
    private int footIndex; // index du bone representant le pied
    private Vector3 rayInitDir; // direction d'origine du raycCast
    private Spatial stepTarget; // target qui aura une transition smooth pour définir la manière dont la patte va du precedant target vers le novueau
    private Vector3 finalStepTarget; // position final que doit atteindra la patte pour etre fixé
    private Vector3 footToTarget; // bizarre, si j'essais de rendre cette variable locale a une methode, je peux pas utiliser stepTarget pour ca position
    private RayCast nextStepRay; // raycast qui permet de definir le prochaine emplacement de la patte
    private bool grounded; // dixera la pate au sol des qu'elle l'atteindra pour pas que ca glisse
    private Skeleton skel;

    private SecondOrder.SecondOrderSysVec3 stepBehavior; // definit la maniere selon laquelle la patte ira vers la prochaine target
    [Export]
    private float f; // oscilation?
    [Export]
    private float C; // vitesse de stabilisation?
    [Export]
    private float r; // vitesse à laquelle la valeur va vers la nouvelle

    [Export]
    private float footToTargetRange; // distance entre le pied et la target a partir de laquelle on cherche un nouvel appui
                                    // je le change, ca represente mtn la distance entre le pied et la base de la jambe
    [Export]
    private NodePath nextStepRayPath; // chemin pour le raycast nextStepRay

    private Spatial show;

    public override void _EnterTree()
    {
        stepTarget = new Spatial();
        GetTree().CurrentScene.CallDeferred("add_child", stepTarget);
        stepTarget.Connect("ready", this, "initStepTarget");
        footIndex = GetParent<Skeleton>().FindBone(TipBone);
        nextStepRay = GetNode<RayCast>(nextStepRayPath);
        rayInitDir = nextStepRay.CastTo;

        stepBehavior = new SecondOrder.SecondOrderSysVec3(f,C,r,Vector3.Zero);


        show = GetTree().CurrentScene.GetNode<Spatial>("show");
    }

    public override void _Ready()
    {
        skel = GetParentSkeleton();
        Start();
    }

    private void initStepTarget(){ // je fais avec un signal et tout mais c'est pas la meilleure methode vu que j'ai un message d'erreur la premiere frame
        TargetNode = stepTarget.GetPath();
    }

    public override void _PhysicsProcess(float delta)
    {
        footToTarget = stepTarget.GlobalTransform.origin - skel.ToGlobal(skel.GetBoneGlobalPose(footIndex).origin);

        if(footToTarget.Length() > footToTargetRange){
            grounded = false;
            if(nextStepRay.IsColliding()){
                finalStepTarget = nextStepRay.GetCollisionPoint();
                //stepTarget.Translation = nextStepRay.GetCollisionPoint();
            }
        }
        else{
            grounded = true;
        }
        stepTarget.Translation = stepBehavior.update(delta, finalStepTarget);
    }

    public bool isGrounded(){
        return grounded;
    }
    public void updateRayDirection(Vector3 movingDir){ // ca c'st tres moche
        if(movingDir != Vector3.Zero){
            Vector3 test2 = rayInitDir.Rotated(Vector3.Up, new Vector2(rayInitDir.x, rayInitDir.z).AngleTo(new Vector2(-movingDir.x, movingDir.z)));
            nextStepRay.CastTo = test2;
        }
        else{
            nextStepRay.CastTo = Vector3.Down * nextStepRay.CastTo.Length();
        }
    }
}

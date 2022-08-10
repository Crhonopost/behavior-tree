using Godot;
using System;

namespace BehaviorTree{
    public class BasicAI : KinematicBody
    {
        private Vector3 targetLocation; // y a moyen de le delete sans trop de probleme si jamais faut alléger le code
        private bool canMove = true;
        private System.Collections.Generic.Queue<Vector3> path;
        private Navigation nav;
        [Export]
        private NodePath navigationPath;
        [Export]
        public float speed = 10;

        public bool CanMove{
            set{canMove = value;}
            get{return canMove;}
        }

        public Vector3 getTargetLocation(){
            return targetLocation;
        }

        public Vector3 getNextStepLocation(){
            return path.Peek();
        }

        public override void _Ready()
        {
            nav = (Navigation) GetNode(navigationPath);
            targetLocation = nav.GetClosestPoint(Translation);
            path = new System.Collections.Generic.Queue<Vector3>();
            path.Enqueue(targetLocation);
        }

        public override void _PhysicsProcess(float delta)
        {
            if(path.Count > 1 && (Translation - path.Peek()).Length() < 1){
                path.Dequeue();
            }
            if(canMove){
                Vector3 movement = (path.Peek() - Translation).Normalized() * speed;
                MoveAndSlide(movement, Vector3.Up);
            }
        }

        public bool rotateYToward(Vector3 target, float speed){
            Vector3 currentDirection = GlobalTransform.basis.z - Translation;
            Vector3 newDirection = target - Translation;

            currentDirection.y = 0;
            newDirection.y = 0;
            RotateY(currentDirection.Normalized().AngleTo(newDirection.Normalized()));
            return true;
        }

        public void setNearestMovementTarget(Vector3 location){ // définit comme prochain objectif a atteindre, le point le plus proche de location
            targetLocation = nav.GetClosestPoint(location);
            path.Clear();
            foreach(Vector3 step in nav.GetSimplePath(Translation, targetLocation)){
                path.Enqueue(step);
            }
        }

        public virtual Vector3 getRandomLocation(float radius){
            Random rng = new Random();
            Vector3 direction = Vector3.Forward * (float) rng.NextDouble() * radius;
            return direction.Rotated(Vector3.Up, Mathf.Deg2Rad(rng.Next(0,360)));
        }

        public bool reachedLocation(){
            return (Translation - targetLocation).Length() < 2;
        }

        public bool pointTo(Vector3 position, float rotationSpeed){ // rotationSpeed a donner en degre
            Vector3 direction = GlobalTransform.basis.z - GlobalTransform.origin;
            Vector3 axis = direction.Cross(position - Translation);
            float rotationAngle =  direction.AngleTo(position - Translation);
            if(rotationAngle > Mathf.Deg2Rad(rotationSpeed)){
                Rotate(axis, Mathf.Deg2Rad(rotationSpeed));
                return false;
            }
            else{
                Rotate(axis, rotationAngle);
                return true;
            }
        }
    }
}
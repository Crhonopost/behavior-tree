using Godot;
using System;

namespace BehaviorTree.Blackboard
{
    public class BlackboardTest : BTBlackboard
    {
        [Export]
        private NodePath playerPath;
        public override void _Ready()
        {
            GetNode(playerPath).Connect("updatePosJoueur", this, "majPosJoueur");
        }

        public void majPosJoueur(Vector3 newPos){
            
        }
    }
}
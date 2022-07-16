using Godot;
using System;

namespace BehaviorTree{
    public class BT : Node
    // manque un truc pour réinitialiser l'etat de l'arbre sans que ca rame 
    {
        /*
        que dois faire le behavior tree?
        - il doit être capable d'initialiser le tick qui permettra de contacter toutes les branches
        - reinitialiser les noeuds quand tout l'arbre a été parcouru?
        - il optimisera les appels aux noeuds quand y'en aura trop ? (idée d'opti: faire tout en 2 frames, une pour tick et une pour process)
        - envoyer le blackboard?
        - faut un agent, une entité à laquelle il est lié
        */
        private BTNode root;

        [Export]
        private NodePath rootPath;

        private Node agent;
        [Export]
        private NodePath agentPath;

        private BTBlackboard blackboard;
        [Export]
        private NodePath blackboardPath;

        private bool isActive;

        public BT(BTNode root, Node agent, BTBlackboard blackboard){
            this.root = root;
            this.agent = agent;
            this.blackboard = blackboard;
        }

        public BT(){
        }

        public void setRoot(NodePath path){
            rootPath = path;
        }

        public void setAgentPath(NodePath path){
            agentPath = path;
        }

        public override void _Ready()
        {
            isActive = true;
            if(GetNode(rootPath).GetType().IsAssignableFrom(typeof(BTNode))){
                root = GetNode<BTNode>(rootPath);
            }
            if(GetNode(agentPath).GetType().IsAssignableFrom(typeof(Node))){
                agent = GetNode<Node>(agentPath);
            }
            if(GetNode(blackboardPath).GetType() == typeof(BTBlackboard)){
                blackboard = GetNode<BTBlackboard>(blackboardPath);
            }
            agent = GetNode(agentPath);
            root = GetNode<BTNode>(rootPath);
            blackboard = GetNode<BTBlackboard>(blackboardPath);
        }

        public override void _Process(float delta)
        {
            if(isActive){
                root.PreTick(agent);
                if(root.Tick(agent, blackboard) != BTState.RUNNING){
                    isActive = false;
                };
                root.PostTick(agent);
            }
        }
    }
}
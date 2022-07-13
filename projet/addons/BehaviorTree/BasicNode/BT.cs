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

        public BT(BTNode root, Node agent){
            this.root = root;
            this.agent = agent;
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
            if(GetNode(rootPath).GetType().IsAssignableFrom(typeof(BTNode))){
                root = GetNode<BTNode>(rootPath);
            }
            if(GetNode(agentPath).GetType().IsAssignableFrom(typeof(Node))){
                agent = (Node) GetNode<Node>(agentPath);
            }
            agent = GetNode(agentPath);
            root = GetNode<BTNode>(rootPath);
        }

        public override void _Process(float delta)
        {
            root.Tick(agent);
        }
    }
}
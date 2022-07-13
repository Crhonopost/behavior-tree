using System;
using Godot;

namespace BehaviorTree
{
    [Title("si tu vois ce text, c'est censé être un titre")]

    [Pre("BeforeEach")]
    class BasicTests : WAT.Test
    {
        private BT tree;
        private BTNode root;
        public void BeforeEach(){
            tree = new BT();
            root = new BTNode();
        }

        [Test]
        public void testInitBT(){
            tree.setRoot(root._ImportPath); // importPath?

        }
    }
}
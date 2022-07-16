using Godot;

namespace BehaviorTree
{
    public class BTBlackboard:Node
    {
        [Export]
        private Godot.Collections.Dictionary data;

        public override void _Ready()
        {
            GD.Print(data);
        }

        public void setValue(object value, string key){
            data[key] = value;
        }

        public object getValue(string key){
            return data[key];
        }

        public bool contains(string key){
            return data.Contains(key);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAndTriggerManipulation
{
    class Node
    {
        public int MyId; // My event id
        public List<string> MyEvents;//Possible event strings
        public List<string> MyTriggers; //Possible event triggers
        public List<int> MySubEvents; //defines list to allow subnodes to be stored/connected below this node
        public bool Terminator = false;//defines and sets the terminator value to false, this is so the structure can no if its the end of a word
        public Node(int ID, List<string> Events, List<string> Triggers, bool EndofTree, List<int> SubEvents)
        {
            this.MyId = ID;
            this.MyEvents = Events;
            this.MyTriggers = Triggers;
            this.Terminator = EndofTree;
            this.MySubEvents = SubEvents;
        }
        s
    }
}

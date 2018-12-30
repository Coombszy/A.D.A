using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.D.A_Host
{
    class Event
    {
        public Event(string EventName, int Id, bool Tree, bool AlwaysAwake, List<string> Triggers, List<string> Phrases, List<int> SubEvents, string Command)
        {
            this.ename = EventName;
            this.id = Id;
            this.tree = Tree;
            this.alwaysawake = AlwaysAwake;
            this.triggers = Triggers;
            this.phrases = Phrases;
            this.subEvents = SubEvents;
            this.command = Command;
        }
        public string ename;
        public int id;
        public bool tree;
        public bool alwaysawake;
        public List<string> phrases;//What the users does/says
        public List<string> triggers;//How ada will react
        public List<int> subEvents;//Next events in the tree
        public string command;//Issues a command from the tree

    }
}

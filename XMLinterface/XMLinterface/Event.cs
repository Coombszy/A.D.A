using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLinterface
{
    class Event
    {
        public Event(string EventName, int Id, bool Tree, bool AlwaysAwake, List<string> Triggers)
        {
            this.ename = EventName;
            this.id = Id;
            this.tree = Tree;
            this.alwaysawake = AlwaysAwake;
            this.triggers = Triggers;
        }
        public string ename;
        public int id;
        public bool tree;
        public bool alwaysawake;
        public List<string> triggers;
        //Sub event tree?

    }
}

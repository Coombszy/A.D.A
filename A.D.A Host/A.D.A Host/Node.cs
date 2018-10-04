using System;
using System.Collections.Generic;
using System.Text;

namespace A.D.A_Host
{
    class Node
    {
        public string MyName;//Event name (for debug)
        public int MyId; // My event id
        public bool AlwaysAwake; //For sorting, always listening
        public List<string> MyEvents;//Possible event strings
        public List<string> MyTriggers; //Possible event triggers
        public List<int> MySubEventsIds; //Ids of child events
        public List<Node> MySubEvents; //defines list to allow subnodes to be stored/connected below this node
        public string MyCommand;//Command to execute
        public bool Terminator = false;//defines and sets the terminator value to false, this is so the structure can no if its the end of a word
        public Node(string Name, int ID, bool EndofTree, bool Awake, List<string> Events, List<string> Triggers, List<int> SubEventsIds, string Command)
        {
            this.MyName = Name;
            this.MyId = ID;
            this.Terminator = EndofTree;
            this.AlwaysAwake = Awake;
            this.MyEvents = Events;
            this.MyTriggers = Triggers;
            this.MySubEventsIds = SubEventsIds;
            this.MyCommand = Command;
            //Console.WriteLine("NODE ~" + MyName + "~ SubEventIdsCount:" + SubEventsIds.Count);
        }
        public List<string> GetDictionary()
        {
            List<string> TempList = new List<string>();
            foreach(object SubEvent in MySubEvents)
            {
                foreach(string EventDictionaryString in ((Node)SubEvent).MyEvents)
                {
                    TempList.Add(EventDictionaryString);
                }
            }
            return TempList;
        }//Gets the Events for Dictionary of all the sub nodes
        public string GetRandomTrigger()
        {
            if (MyTriggers.Capacity == 0)
            {
                return "";
            }
            else
            {
                Random rnd = new Random();
                return MyTriggers[rnd.Next(0, MyTriggers.Count)];
            }
        }//returns a random trigger 
        public void LoadInSubEvents(List<Node> SubEventsToAdd)
        {
            try
            {
                this.MySubEvents = SubEventsToAdd;
            }
            catch (Exception e)
            {
                Console.WriteLine("LOAD ERROR!");
                Console.WriteLine("EVENT:" + MyName + " - Failed to load its sub events");
                Console.WriteLine(" LOG:");
                Console.WriteLine(e.ToString());
                Console.WriteLine("-------------------END LOG-------------------");
                Console.WriteLine("Enter to Exit");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

    }
}

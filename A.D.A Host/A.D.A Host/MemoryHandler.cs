using System;
using System.Collections.Generic;
using System.Text;

namespace A.D.A_Host
{
    class MemoryHandler
    {
        private List<Node> MemoryStructure = new List<Node>();
        private List<Node> EventList;
        public Node ActiveNode; // -------------------------MAKE ME PRIVATE (PUBLIC FOR DEBUG)----------------
        public void BuildEventList()
        {
            try
            {
                XMLInterface XmlReader = new XMLInterface("events.xml");
                XmlReader.ReadXML();
                EventList = XmlReader.EventData;
                Console.WriteLine("TOTAL EVENTS FOUND:" + EventList.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine("FATAL ERROR");
                Console.WriteLine(e.ToString());
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        public void BuildMemoryStructure()
        {
            LoadingBar MemoryStructureBuilding = new LoadingBar(EventList.Count, EventList.Count, false);
            try
            {
                while (EventList.Count != 0)
                {
                    Node Event = EventList[0];
                    if (Event.AlwaysAwake == true)
                    {
                        MemoryStructure.Add(Event);
                        EventList.RemoveAll(x => x.MyId == Event.MyId);
                        MemoryStructureBuilding.Draw(EventList.Count);
                        //Console.WriteLine("ALWAYSAWAKENODE!");
                    }
                    else
                    {
                        CollectSubNodes(Event);
                        foreach(Node Established in MemoryStructure)
                        {
                            if (Established.MySubEventsIds.Contains(Event.MyId))
                            {
                                Established.LoadInSubEvents(new List<Node> { Event });
                                break;
                            }
              
                        }
                        EventList.RemoveAll(x => x.MyId == Event.MyId);
                        MemoryStructureBuilding.Draw(EventList.Count);
                        //Console.WriteLine("SUBNODE");
                    }
                }
                Console.WriteLine("SUCCESSFULLY BUILT MEMORY UNIT");
                EventList = null;
                GC.Collect();
            }
            catch (Exception e)
            {
                Console.WriteLine("FAILED TO BUILD MEMORY UNIT");
                Console.WriteLine("ERROR LOG: "+e.ToString());
                Console.ReadLine();
            }
        }
        public List<string> GetEventsofActiveNode()
        {
            List<string> Events = new List<string>();
            if(ActiveNode == null)
            {
                foreach(Node Event in MemoryStructure)
                {
                    foreach(string EventString in Event.MyPhrases)
                    {
                        Events.Add(EventString);
                    }
                }
            }
            else
            {
                foreach(Node SubEvent in ActiveNode.MySubEvents)
                {
                    foreach (string EventString in SubEvent.MyPhrases)
                    {
                        Events.Add(EventString);
                    }
                }
            }
            return Events;
        }
        public List<string> GetActiveEventTrigger()
        {
            List<string> Triggers = new List<string>();
            if (ActiveNode == null)
            {
                foreach (Node Event in MemoryStructure)
                {
                    foreach (string EventString in Event.MyTriggers)
                    {
                        Triggers.Add(EventString);
                    }
                }
            }
            else
            {
                foreach (Node SubEvent in ActiveNode.MySubEvents)
                {
                    foreach (string EventString in SubEvent.MyTriggers)
                    {
                        Triggers.Add(EventString);
                    }
                }
            }
            return Triggers;
        }
        public string GetResponse()
        {
            string Response = ActiveNode.GetRandomTrigger();

            if (ActiveNode.Terminator)
            {
                ActiveNode = null;
            }
            return Response;
        }
        public string GetCommand()
        {
            if (ActiveNode == null)
            { return ""; }
            return ActiveNode.MyCommand;
            
        }
        /*public bool NavigateOLD(string EventString)
        {
            List<string> EventStrings = GetEventsofActiveNode();
            if(EventStrings.Contains(EventString))
            {
                if(ActiveNode == null)
                {
                    foreach(Node Event in MemoryStructure)
                    {
                        foreach(string String in Event.MyPhrases)
                        {
                            if(String == EventString)
                            {
                                ActiveNode = Event;
                                return true;
                            }
                        }
                        break;
                    }
                }
                else if (ActiveNode.Terminator == false)
                {
                    foreach (Node Event in ActiveNode.MySubEvents)
                    {
                        foreach (string String in Event.MyPhrases)
                        {
                            Console.WriteLine(":" + String);
                            if (String == EventString)
                            {
                                ActiveNode = Event;
                                return true;
                            }
                        }
                        break;
                    }
                }
            }
            return false;
        }*/
        public bool Navigate(string EventString)
        {
            List<string> EventStrings = GetEventsofActiveNode();
            if (EventStrings.Contains(EventString))
            {
                if (ActiveNode == null)
                {
                    foreach (Node Event in MemoryStructure)
                    {
                        foreach (string String in Event.MyPhrases)
                        {
                            if (String == EventString)
                            {
                                ActiveNode = Event;
                                return true;
                            }
                        }
                        break;
                    }
                }
                else if (ActiveNode.Terminator == false)
                {
                    foreach (Node Event in ActiveNode.MySubEvents)
                    {
                        foreach (string String in Event.MyPhrases)
                        {
                            if (String == EventString)
                            {
                                ActiveNode = Event;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private void CollectSubNodes(Node Event)
        {
            List<Node> SubNodesToAdd;
            if (Event.MySubEventsIds.Count != 0)
            {
                SubNodesToAdd = EventList.FindAll(x => Event.MySubEventsIds.Contains(x.MyId));
                EventList.RemoveAll(x => Event.MySubEventsIds.Contains(x.MyId));
                foreach (Node SubEvent in SubNodesToAdd)
                {
                    CollectSubNodes(SubEvent);
                }
                Event.LoadInSubEvents(SubNodesToAdd);
            }
            EventList.RemoveAll(x => Event.MySubEventsIds.Contains(x.MyId));
        }
    }
}

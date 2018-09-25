using System;
using System.Collections.Generic;
using System.Text;

namespace EventAndTriggerManipulation
{
    class MemoryHandler
    {
        private List<Node> MemoryStructure = new List<Node>();
        private List<Node> EventList;
        private Node ActiveNode = null;
        public void BuildEventList()
        {
            try
            {
                XMLinterface XmlReader = new XMLinterface("events.xml");
                EventList = XmlReader.ReadXML();
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
            while (EventList.Count != 0)
            {
                Node Event = EventList[0];
                if (Event.MySubEventsIds.Count != 0)
                {
                    CollectSubNodes(Event);
                }
                if (Event.AlwaysAwake == true)
                {
                    MemoryStructure.Add(Event);
                    EventList.RemoveAll(x => x.MyId == Event.MyId);
                }
            }
            EventList = null;
            GC.Collect();
            /*
            foreach (Node Event in EventList)
            {
                if (Event.MySubEventsIds.Count != 0)
                {
                    CollectSubNodes(Event);
                }
                if (Event.AlwaysAwake == true)
                {
                    MemoryStructure.Add(Event);
                    EventList.RemoveAll(x => x.MyId == Event.MyId);
                }
            }*/
        }
        public List<string> GetEventsofActiveNode()
        {
            List<string> Events = new List<string>();
            if(ActiveNode == null)
            {
                foreach(Node Event in MemoryStructure)
                {
                    foreach(string EventString in Event.MyEvents)
                    {
                        Events.Add(EventString);
                    }
                }
            }
            else
            {
                foreach(Node SubEvent in ActiveNode.MySubEvents)
                {
                    foreach (string EventString in SubEvent.MyEvents)
                    {
                        Events.Add(EventString);
                    }
                }
            }
            return Events;
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

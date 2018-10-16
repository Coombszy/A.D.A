using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace A.D.A_Host
{
    class XMLinterface
    {
        private string XmlName;
        public XMLinterface(string xmlname)
        {
            this.XmlName = xmlname;
        }
        public List<Node> old_ReadXML()
        {
            List<Node> events = new List<Node>();
            try
            {
                XmlReader reader = XmlReader.Create(XmlName);
                try
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "EVENT")
                        {
                            int id = int.Parse(reader.GetAttribute(0));
                            string name = reader.GetAttribute(1);
                            string command = "";
                            bool tree = false;
                            bool alwaysawake = false;
                            List<string> responses = new List<string>();
                            List<string> Commands = new List<string>();
                            List<string> triggers = new List<string>();
                            List<int> subevents = new List<int>();
                            while (reader.NodeType != XmlNodeType.EndElement)
                            {
                                reader.Read();
                                if (reader.Name == "tree")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.NodeType == XmlNodeType.Text)
                                        {
                                            tree = bool.Parse(reader.Value);
                                        }
                                    }
                                    reader.Read();
                                }//Read tree value
                                if (reader.Name == "alwaysawake")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.NodeType == XmlNodeType.Text)
                                        {
                                            alwaysawake = bool.Parse(reader.Value);
                                        }
                                    }
                                    reader.Read();
                                }//Read alwaysawake value

                                if (reader.Name == "triggerstrings")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.Name == "string")
                                        {
                                            while (reader.NodeType != XmlNodeType.EndElement)
                                            {
                                                reader.Read();
                                                if (reader.NodeType == XmlNodeType.Text)
                                                {
                                                    triggers.Add(reader.Value);
                                                }
                                            }
                                            reader.Read();
                                        } //end if
                                    }
                                    reader.Read();
                                }

                                if (reader.Name == "responsestrings")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.Name == "string")
                                        {
                                            while (reader.NodeType != XmlNodeType.EndElement)
                                            {
                                                reader.Read();
                                                if (reader.NodeType == XmlNodeType.Text)
                                                {
                                                    responses.Add(reader.Value);
                                                }
                                            }
                                            reader.Read();
                                        } //end if
                                    }
                                    reader.Read();
                                }

                                if (reader.Name == "subevents")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.Name == "int")
                                        {
                                            while (reader.NodeType != XmlNodeType.EndElement)
                                            {
                                                reader.Read();
                                                if (reader.NodeType == XmlNodeType.Text)
                                                {
                                                    subevents.Add(int.Parse(reader.Value));
                                                }
                                            }
                                            reader.Read();
                                        } //end if
                                    }
                                    reader.Read();
                                }
                                if (reader.Name == "command")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.NodeType == XmlNodeType.Text)
                                        {
                                            command = reader.Value;
                                        }
                                    }
                                    reader.Read();
                                }//Read command value
                            }
                            events.Add(new Node(name, id, tree, alwaysawake, responses, triggers, subevents, command));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(XmlName + " is invalid, Overwrite enabled");
                    Console.WriteLine(e.ToString());
                    Console.ReadLine();
                }
                reader.Close();
            }
            catch
            {
                Console.WriteLine(XmlName + " doesn't exsist, will make one when saving");
                Console.ReadLine();
            }
            return events;
        }
        public List<Node> ReadXML()
        {
            List<Node> events = new List<Node>();
            try
            {
                XmlReader reader = XmlReader.Create(XmlName);
                try
                {
                    while (reader.Read())
                    {
                        while (reader.NodeType != XmlNodeType.EndElement)
                        {
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "EVENT")
                            {
                                int id = int.Parse(reader.GetAttribute(0));
                                string name = reader.GetAttribute(1);
                                bool tree = false;
                                bool alwaysawake = false;
                                List<string> triggers = new List<string>();
                                List<string> responses = new List<string>();
                                List<int> subevents = new List<int>();
                                string command = "";
                                while (reader.NodeType != XmlNodeType.EndElement)
                                {
                                    reader.Read();
                                    if (reader.Name == "tree")
                                    {
                                        while (reader.NodeType != XmlNodeType.EndElement)
                                        {
                                            reader.Read();
                                            if (reader.NodeType == XmlNodeType.Text)
                                            {
                                                tree = bool.Parse(reader.Value);
                                            }
                                        }
                                        reader.Read();
                                    }//Read tree value
                                    if (reader.Name == "alwaysawake")
                                    {
                                        while (reader.NodeType != XmlNodeType.EndElement)
                                        {
                                            reader.Read();
                                            if (reader.NodeType == XmlNodeType.Text)
                                            {
                                                alwaysawake = bool.Parse(reader.Value);
                                            }
                                        }
                                        reader.Read();
                                    }//Read alwaysawake value

                                    if (reader.Name == "triggerstrings")
                                    {
                                        while (reader.NodeType != XmlNodeType.EndElement)
                                        {
                                            reader.Read();
                                            if (reader.Name == "string")
                                            {
                                                while (reader.NodeType != XmlNodeType.EndElement)
                                                {
                                                    reader.Read();
                                                    if (reader.NodeType == XmlNodeType.Text)
                                                    {
                                                        triggers.Add(reader.Value);
                                                    }
                                                }
                                                reader.Read();
                                            } //end if
                                        }
                                        reader.Read();
                                    }

                                    if (reader.Name == "responsestrings")
                                    {
                                        while (reader.NodeType != XmlNodeType.EndElement)
                                        {
                                            reader.Read();
                                            if (reader.Name == "string")
                                            {
                                                while (reader.NodeType != XmlNodeType.EndElement)
                                                {
                                                    reader.Read();
                                                    if (reader.NodeType == XmlNodeType.Text)
                                                    {
                                                        responses.Add(reader.Value);
                                                    }
                                                }
                                                reader.Read();
                                            } //end if
                                        }
                                        reader.Read();
                                    }

                                    if (reader.Name == "subevents")
                                    {
                                        while (reader.NodeType != XmlNodeType.EndElement)
                                        {
                                            reader.Read();
                                            Console.WriteLine("SUBEVENTS DETECTED!");
                                            Console.WriteLine("READERVALUE:"+reader.Value);
                                            if (reader.Name == "int")
                                            {
                                                Console.WriteLine("INT DETECTED!");
                                                while (reader.NodeType != XmlNodeType.EndElement)
                                                {
                                                    reader.Read();
                                                    if (reader.NodeType == XmlNodeType.Text)
                                                    {
                                                        subevents.Add(int.Parse(reader.Value));
                                                        Console.WriteLine("ADD A SUBNODE:" + reader.Value);
                                                    }
                                                }
                                                reader.Read();
                                            } //end if
                                        }
                                        reader.Read();
                                    }
                                    if (reader.Name == "command")
                                    {
                                        while (reader.NodeType != XmlNodeType.EndElement)
                                        {
                                            reader.Read();
                                            if (reader.NodeType == XmlNodeType.Text)
                                            {
                                                command = reader.Value;
                                            }
                                        }
                                        reader.Read();
                                    }//Read Command
                                }
                                events.Add(new Node(name, id, tree, alwaysawake, triggers, responses, subevents, command));
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(XmlName + " is invalid, Overwrite enabled");
                    Console.WriteLine(e.ToString());
                    Console.ReadLine();
                }
                reader.Close();
            }
            catch
            {
                Console.WriteLine(XmlName + " doesn't exsist, will make one when saving");
                Console.ReadLine();
            }
            return events;
        }
        public Node[] LoadEvents()
        {
            List<Node> TempEvents = ReadXML();
            Node[] Events = new Node[TempEvents.Count];
            foreach (Node TempEvent in TempEvents)
            {
                Events[TempEvent.MyId - 1] = TempEvent;
            }
            return Events;
        }//NOT USED---------------------------------------
        public Node[] ConvertListToArray(List<Node> TempEvents)
        {
            Node[] Events = new Node[TempEvents.Count];
            foreach (Node TempEvent in TempEvents)
            {
                Events[TempEvent.MyId - 1] = TempEvent;
            }
            return Events;
        }
        public void SaveEvents(Node[] Events)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(XmlName, settings);
            writer.WriteStartDocument();
            writer.WriteComment("XML INTERFACE - List of events");
            foreach (Node TempEvent in Events)
            {
                writer.WriteStartElement("EVENT");
                writer.WriteAttributeString("ID", "" + TempEvent.MyId);
                writer.WriteAttributeString("Name", TempEvent.MyName);
                writer.WriteElementString("tree", "" + TempEvent.Terminator);
                writer.WriteElementString("alwaysawake", "" + TempEvent.AlwaysAwake);
                writer.WriteStartElement("triggerstrings");
                foreach (string etrigger in TempEvent.MyEvents)
                {
                    writer.WriteElementString("string", etrigger);
                }
                writer.WriteEndElement();
                writer.WriteStartElement("responsestrings");
                foreach (string eresponse in TempEvent.MyTriggers)
                {
                    writer.WriteElementString("string", eresponse);
                }
                writer.WriteEndElement();
                writer.WriteStartElement("subevents");
                foreach (int Event in TempEvent.MySubEventsIds)
                {
                    writer.WriteElementString("int", "" + Event);
                }
                writer.WriteEndElement();


            }
            //writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }//OLD WRITER, NEW ONE AVALIABLE IN XMLEventsAndTriggers
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLinterface
{
    class XMLinterface
    {
        private string XmlName;
        public XMLinterface(string xmlname)
        {
            this.XmlName = xmlname;
        }
        public List<Event> ReadXML()
        {
            List<Event> Events = new List<Event>();
            XmlReader reader;
            reader = XmlReader.Create(XmlName);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "EVENT")
                {
                    //-----------------EVENT DATA VARIABLES-----------------
                    int id = int.Parse(reader.GetAttribute(0));
                    string name = reader.GetAttribute(1);
                    bool tree = false;
                    bool alwaysawake = false;
                    List<string> phrases = new List<string>();
                    List<string> triggers = new List<string>();
                    List<int> subevents = new List<int>();
                    string command = "";
                    //-----------------EVENT DATA VARIABLES-----------------
                    while (reader.Read())
                    {
                        if (reader.Name == "Tree")
                        {
                            while (reader.NodeType != XmlNodeType.EndElement)
                            {
                                reader.Read();
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    tree = bool.Parse(reader.Value);
                                }
                            }
                        }//Read tree value
                        if (reader.Name == "AlwaysAwake")
                        {
                            while (reader.NodeType != XmlNodeType.EndElement)
                            {
                                reader.Read();
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    alwaysawake = bool.Parse(reader.Value);
                                }
                            }
                        }//Read alwaysawake value

                        if (reader.Name == "Phrases")
                        {
                            while (reader.NodeType != XmlNodeType.EndElement)
                            {
                                //reader.Read();
                                if (reader.Name == "string")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.NodeType == XmlNodeType.Text)
                                        {
                                            phrases.Add(reader.Value);
                                            //Console.WriteLine("Phrase:" + reader.Value);
                                        }
                                    }
                                    reader.Read();
                                } //end if
                                reader.Read();
                            }
                        }//Phrases

                        if (reader.Name == "Triggers")
                        {
                            while (reader.NodeType != XmlNodeType.EndElement)
                            {
                                //reader.Read();
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
                                reader.Read();
                            }
                        }

                        if (reader.Name == "SubEvents")
                        {
                            while (reader.NodeType != XmlNodeType.EndElement)
                            {
                                //reader.Read();
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
                                reader.Read();
                            }
                        }

                        //IN COMMAND ELEMENT BREAK FROM WHILE LOOP TO COMPLETE THE SINGLE ELEMENT!---------------------------------------DELETE ME-----------
                        if (reader.Name == "Command")
                        {

                            while (reader.NodeType != XmlNodeType.EndElement)
                            {
                                reader.Read();
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    command = reader.Value;
                                }
                            }
                            //break;
                        }//Read Command value
                        if (reader.Name == "EVENT" && reader.NodeType == XmlNodeType.EndElement)
                        {
                            break;
                        }
                    }
                    try
                    {
                        Events.Add(new Event(name, id, tree, alwaysawake, triggers, phrases, subevents, command));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        Console.WriteLine("PAUSED");
                        Console.ReadLine();
                        reader.Close();
                        return Events;
                    }
                }
            }
            reader.Close();
            return Events;

        }
        public Event[] LoadEvents()
        {
            List<Event> TempEvents = ReadXML();
            Event[] Events = new Event[TempEvents.Count];
            foreach (Event TempEvent in TempEvents)
            {
                Events[TempEvent.id - 1] = TempEvent;
            }
            return Events;
        }
        public Event[] ConvertListToArray(List<Event> TempEvents)
        {
            Event[] Events = new Event[TempEvents.Count];
            foreach (Event TempEvent in TempEvents)
            {
                Events[TempEvent.id - 1] = TempEvent;
            }
            return Events;
        }
        public void SaveXML(Event[] Events)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter Writer = XmlWriter.Create(XmlName, settings);
            Writer.WriteStartDocument();
            Writer.WriteComment("A.D.A Project - MASTER Event list");
            Writer.WriteStartElement("EVENTS");
            foreach (Event anEvent in Events)
            {
                Writer.WriteStartElement("EVENT");
                Writer.WriteAttributeString("ID", "" + anEvent.id);
                Writer.WriteAttributeString("Name", anEvent.ename);
                Writer.WriteElementString("Tree", "" + anEvent.tree);
                Writer.WriteElementString("AlwaysAwake", "" + anEvent.alwaysawake);
                if (anEvent.phrases.Count != 0)
                {
                    Writer.WriteStartElement("Phrases");
                    foreach (string phrase in anEvent.phrases)
                    {
                        Writer.WriteElementString("string", phrase);
                    }
                    Writer.WriteEndElement();
                }
                if (anEvent.triggers.Count != 0)
                {
                    Writer.WriteStartElement("Triggers");
                    foreach (string trigger in anEvent.triggers)
                    {
                        Writer.WriteElementString("string", trigger);
                    }
                    Writer.WriteEndElement();
                }
                if (anEvent.subEvents.Count != 0)
                {
                    Writer.WriteStartElement("SubEvents");
                    foreach (int subevent in anEvent.subEvents)
                    {
                        Writer.WriteElementString("int", "" + subevent);
                    }
                    Writer.WriteEndElement();
                }
                if (anEvent.command != "" && anEvent.command != null)
                {
                    Writer.WriteElementString("Command", "" + anEvent.command);
                }
                Writer.WriteEndElement();
            }
            Writer.WriteEndElement();
            Writer.WriteEndDocument();
            Writer.Flush();
            Writer.Close();
        }
    }
}

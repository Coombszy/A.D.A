using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace A.D.A_Host
{
    class XMLInterface
    {
        private string XmlName;
        public List<Node> EventData;
        public XMLInterface(string xmlname)
        {
            this.XmlName = xmlname;
            this.EventData = new List<Node>();
        }
        public bool ReadXML()
        {
            List<Node> Events = new List<Node>();
            XmlReader reader;
            try
            {
                reader = XmlReader.Create(XmlName);
            }
            catch
            {
                Console.WriteLine("FATAL:XMLInterface - Could not find XMLInterface");
                return false;
            }
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
                        EventData.Add(new Node(name, id, tree, alwaysawake, phrases, triggers, subevents, command));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        Console.WriteLine("PAUSED");
                        Console.ReadLine();
                        reader.Close();
                        return false;
                    }
                }
            }
            reader.Close();
            return true;
        }
        public void SaveXML(Node[] Events)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter Writer = XmlWriter.Create(XmlName, settings);
            Writer.WriteStartDocument();
            Writer.WriteComment("A.D.A Project - MASTER Event list");
            Writer.WriteStartElement("EVENTS");
            foreach(Node anEvent in Events)
            {
                Writer.WriteStartElement("EVENT");
                Writer.WriteAttributeString("ID", "" + anEvent.MyId);
                Writer.WriteAttributeString("Name", anEvent.MyName);
                Writer.WriteElementString("Tree", "" + anEvent.Terminator);
                Writer.WriteElementString("AlwaysAwake", "" + anEvent.AlwaysAwake);
                if (anEvent.MyPhrases.Count != 0)
                {
                    Writer.WriteStartElement("Phrases");
                    foreach (string phrase in anEvent.MyPhrases)
                    {
                        Writer.WriteElementString("string", phrase);
                    }
                    Writer.WriteEndElement();
                }
                if (anEvent.MyTriggers.Count != 0)
                {
                    Writer.WriteStartElement("Triggers");
                    foreach (string trigger in anEvent.MyTriggers)
                    {
                        Writer.WriteElementString("string", trigger);
                    }
                    Writer.WriteEndElement();
                }
                if (anEvent.MySubEventsIds.Count != 0)
                {
                    Writer.WriteStartElement("SubEvents");
                    foreach (int subevent in anEvent.MySubEventsIds)
                    {
                        Writer.WriteElementString("int", "" + subevent);
                    }
                    Writer.WriteEndElement();
                }
                if (anEvent.MyCommand != "" && anEvent.MyCommand != null)
                {
                    Writer.WriteElementString("Command", "" + anEvent.MyCommand);
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

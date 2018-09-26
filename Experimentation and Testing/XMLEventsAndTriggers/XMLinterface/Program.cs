using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLinterface
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLinterface Interface = new XMLinterface("events.xml");
            List<Event> Events = Interface.ReadXML();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Event builder");
                Console.WriteLine("     1)Make event");
                Console.WriteLine("     2)Show events");
                Console.WriteLine("     3)Save events");
                Console.WriteLine("     4)Reload events");
                try
                {
                    int option = int.Parse(Console.ReadLine());
                    if (option == 1)
                    {
                        Console.WriteLine("Next ID in sequence {" + (Events.Count + 1) + "}");
                        Console.Write("Enter ID:");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("\nEnter Event Name:");
                        string eventname = Console.ReadLine();
                        Console.Write("\nEnter end of tree bool (true/false):");
                        bool tree = bool.Parse(Console.ReadLine());
                        Console.Write("\nEnter always awake bool (true/false):");
                        bool alwaysawake = bool.Parse(Console.ReadLine());
                        Console.WriteLine("Enter triggers now, enter // to finish");
                        List<string> triggers = new List<string>();
                        while (true)
                        {
                            string input = Console.ReadLine();
                            if (input == "//")
                            {
                                break;
                            }
                            else
                            {
                                triggers.Add(input);
                            }
                        }
                        Console.WriteLine("Enter responses now, enter // to finish");
                        List<string> responses = new List<string>();
                        while (true)
                        {
                            string input = Console.ReadLine();
                            if (input == "//")
                            {
                                break;
                            }
                            else
                            {
                                responses.Add(input);
                            }
                        }
                        Console.WriteLine("Enter SubEvents now, enter // to finish");
                        List<int> SubEvents = new List<int>();
                        while (true)
                        {
                            string input = Console.ReadLine();
                            if (input == "//")
                            {
                                break;
                            }
                            else
                            {
                                SubEvents.Add(int.Parse(input));
                            }
                        }
                        try
                        {
                            Events[id - 1] = new Event(eventname, id, tree, alwaysawake, triggers, responses, SubEvents);
                        }
                        catch
                        {
                            Events.Add(new Event(eventname, id, tree, alwaysawake, triggers, responses, SubEvents));
                        }
                    }
                    else if (option == 2)
                    {
                        foreach (Event TempEvent in Events)
                        {
                            Console.WriteLine("          ID:" + TempEvent.id);
                            Console.WriteLine("  Event Name:" + TempEvent.ename);
                            Console.WriteLine(" End Of Tree:" + TempEvent.tree);
                            Console.WriteLine("Always Awake:" + TempEvent.alwaysawake);
                            Console.WriteLine("    Triggers:");
                            int i = 0;
                            foreach (string Trigger in TempEvent.triggers)
                            {
                                i++;
                                Console.WriteLine("             " + i + "." + Trigger);
                            }
                            Console.WriteLine("   Responses:");
                            i = 0;
                            foreach (string Response in TempEvent.responses)
                            {
                                i++;
                                Console.WriteLine("             " + i + "." + Response);
                            }
                            Console.WriteLine("   SubEvents:");
                            i = 0;
                            foreach (int SubEvent in TempEvent.subEvents)
                            {
                                i++;
                                Console.WriteLine("             " + i + "." + SubEvent);
                            }
                            Console.WriteLine("-------------------------------------------------------------------------------------------");
                        }
                        Console.WriteLine("Press enter to Leave");
                        Console.ReadLine();
                    }
                    else if (option == 3)
                    {
                        try
                        {
                            Interface.SaveEvents(Interface.ConvertListToArray(Events));
                            Console.WriteLine("Saved!");
                            Console.ReadLine();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("FAILED TO SAVE!");
                            Console.WriteLine(e.ToString());
                            Console.ReadLine();
                        }

                    }
                    else if (option == 4)
                    {
                        try
                        { 
                            Events = Interface.ReadXML();
                            Console.WriteLine("Reloaded events!!");
                            Console.ReadLine();
                        }
                        catch
                        {
                            Console.WriteLine("failed to reload");
                            Console.ReadLine();
                        }
                    }

                }
                catch(Exception e)
                    {
                    Console.WriteLine(e.ToString());
                    Console.ReadLine();
                    }
            }
        }
    }
}

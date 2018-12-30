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
                        Console.WriteLine("Enter phrases now, enter // to finish");
                        List<string> phrases = new List<string>();
                        while (true)
                        {
                            string input = Console.ReadLine();
                            if (input == "//")
                            {
                                break;
                            }
                            else
                            {
                                phrases.Add(input);
                            }
                        }
                        Console.WriteLine("Enter responses now, enter // to finish");
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
                        Console.Write("\nEnter a command (Nothing for no command):");
                        string command = Console.ReadLine();
                        Console.WriteLine();
                        try
                        {
                            Events[id - 1] = new Event(eventname, id, tree, alwaysawake, triggers, phrases, SubEvents, command);
                        }
                        catch
                        {
                            Events.Add(new Event(eventname, id, tree, alwaysawake, triggers, phrases, SubEvents, command));
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
                            Console.WriteLine("    Responses:");
                            int i = 0;
                            foreach (string res in TempEvent.phrases)
                            {
                                i++;
                                Console.WriteLine("             " + i + "." + res);
                            }
                            Console.WriteLine("   Triggers:");
                            i = 0;
                            foreach (string trig in TempEvent.triggers)
                            {
                                i++;
                                Console.WriteLine("             " + i + "." + trig);
                            }
                            Console.WriteLine("   SubEvents:");
                            i = 0;
                            foreach (int SubEvent in TempEvent.subEvents)
                            {
                                i++;
                                Console.WriteLine("             " + i + "." + SubEvent);
                            }
                            Console.WriteLine("     Command:" + TempEvent.command);
                            Console.WriteLine("-------------------------------------------------------------------------------------------");
                        }
                        Console.WriteLine("Press enter to Leave");
                        Console.ReadLine();
                    }
                    else if (option == 3)
                    {
                        try
                        {
                            Interface.SaveXML(Interface.ConvertListToArray(Events));
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

using System;
class Reader
{
    static public void start(Client client)
    {
        bool cycle = true;
        while (cycle)
        {
            string input = Console.ReadLine();
            string[] words = input.Split();
            string command = words[0];
            try
            {
                switch (command)
                {
                    case "/add":
                        int id;
                        if (words.Length > 2)
                        {
                            id = client.Manager.add(words[1], words[2]);
                        }
                        else
                        {
                            id = client.Manager.add(words[1]);
                        }
                        Console.WriteLine("Task was created. Id: " + id);
                        break;
                    case "/delete":
                        client.Manager.delete(Convert.ToInt32(words[1]));
                        break;
                    case "/all":
                        client.all();
                        break;
                    case "/save":
                        client.save(words[1]);
                        break;
                    case "/load":
                        client.load(words[1]);
                        break;
                    case "/create-group":
                        client.Manager.create_group(words[1]);
                        break;
                    case "/add-to-group":
                        client.Manager.add_to_group(Convert.ToInt32(words[1]), words[2]);
                        break;
                    case "/delete-group":
                        client.Manager.delete_group(words[1]);
                        break;
                    case "/delete-from-group":
                        client.Manager.delete_from_group(Convert.ToInt32(words[1]), words[2]);
                        break;
                    case "/complete":
                        client.Manager.complete(Convert.ToInt32(words[1]));
                        break;
                    case "/completed":
                        if (words.Length > 1)
                        {
                            client.completed(words[1]);
                        }
                        else
                        {
                            client.completed();
                        }
                        break;
                    case "/today":
                        client.today();
                        break;
                    case "/add-subtask":
                        client.Manager.add_subtask(Convert.ToInt32(words[1]), words[2]);
                        break;
                    case "/break":
                        cycle = false;
                        break;
                    default:
                        Console.WriteLine("Wrong command!");
                        break;
                }
            }
            catch(Exception error)
            {
                Console.WriteLine(error);
            }
            
                      
        }
    }
}
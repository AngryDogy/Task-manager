using System;
using System.IO;
using System.Text;
class Client
{
    private Manager manager;
    public Client(Manager manager_)
    {
        manager = manager_;
    }
    public void start()
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
                            id = manager.add(words[1], words[2]);
                        }
                        else
                        {
                            id = manager.add(words[1]);
                        }
                        Console.WriteLine("Task was created. Id: " + id);
                        break;
                    case "/delete":
                        manager.delete(Convert.ToInt32(words[1]));
                        break;
                    case "/all":
                        this.all();
                        break;
                    case "/save":
                        this.save(words[1]);
                        break;
                    case "/load":
                        this.load(words[1]);
                        break;
                    case "/create-group":
                        manager.create_group(words[1]);
                        break;
                    case "/add-to-group":
                        manager.add_to_group(Convert.ToInt32(words[1]), words[2]);
                        break;
                    case "/delete-group":
                        manager.delete_group(words[1]);
                        break;
                    case "/delete-from-group":
                        manager.delete_from_group(Convert.ToInt32(words[1]), words[2]);
                        break;
                    case "/complete":
                        manager.complete(Convert.ToInt32(words[1]));
                        break;
                    case "/completed":
                        if (words.Length > 1)
                        {
                            this.completed(words[1]);
                        }
                        else
                        {
                            this.completed();
                        }
                        break;
                    case "/today":
                        this.today();
                        break;
                    case "/add-subtask":
                        manager.add_subtask(Convert.ToInt32(words[1]), words[2]);
                        break;
                    case "/break":
                        cycle = false;
                        break;
                    default:
                        Console.WriteLine("Wrong command!");
                        break;

                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }
    }
    private void write_task(int id)
    {
        char cross;
        if (!manager.Tasks[id].IsDeleted)
        {
            if (manager.Tasks[id].IsCompleted)
            {
                cross = 'x';
            }
            else
            {
                cross = ' ';
            }
            string date = manager.Tasks[id].Deadline.Date.ToString("dd/MM/yyyy");
            if (date == "01/01/0001")
            {
                date = " ";
            }
            Console.WriteLine("[" + cross + "]" + ' ' + "{" + id + "}" + ' ' + manager.Tasks[id].Name + ' ' + date);
        }
        if (manager.Subtasks.ContainsKey(id))
        {
            foreach (Task sub in manager.Subtasks[id])
            {
                if (sub.IsCompleted || manager.Tasks[id].IsCompleted)
                {
                    cross = 'x';
                }
                else
                {
                    cross = ' ';
                }

                Console.WriteLine("   " + "[" + cross + "]" + ' ' + sub.Name);
            }
        }
    }
    private void all()
    {
        bool[] was = new bool[manager.Size];
        foreach (string key in manager.Groups.Keys)
        {
            Console.WriteLine("Group: " + key);
            foreach (int id in manager.Groups[key])
            {
                this.write_task(id);
                was[id] = true;
            }
            Console.WriteLine(" ");
        }
        for (int id = 0; id < manager.Size; id++)
        {

            if (!was[id])
            {
                this.write_task(id);
            }
        }
    }
    private void completed()
    {
        for (int id = 0; id < manager.Size; id++)
        {
            if (!manager.Tasks[id].IsDeleted && manager.Tasks[id].IsCompleted)
            {
                this.write_task(id);
            }
        }
    }
    private void completed(string name)
    {
        foreach (int id in manager.Groups[name])
        {
            if (!manager.Tasks[id].IsDeleted && manager.Tasks[id].IsCompleted)
            {
                this.write_task(id);
            }
        }
    }
    private void today()
    {
        string today = DateTime.Now.ToString("dd/MM/yyyy");
        for (int id = 0; id < manager.Size; id++)
        {
            if (!manager.Tasks[id].IsDeleted && manager.Tasks[id].Deadline.ToString("dd/MM/yyyy") == today)
            {
                this.write_task(id);
            }
        }
    }
    private void save(string file_name)
    {
        using (StreamWriter writer = new StreamWriter(file_name))
        {
            bool[] was = new bool[manager.Size];
            foreach (string key in manager.Groups.Keys)
            {
                writer.WriteLineAsync("Group" + ' ' + key);
                foreach (int id in manager.Groups[key])
                {
                    string date = manager.Tasks[id].Deadline.Date.ToString("dd/MM/yyyy");
                    if (date == "01/01/0001")
                    {
                        writer.WriteLineAsync(manager.Tasks[id].Name);
                    }
                    else
                    {
                        writer.WriteLineAsync(manager.Tasks[id].Name + ' ' + date);
                    }
                    if (manager.Subtasks.ContainsKey(id))
                    {
                        foreach (Task sub in manager.Subtasks[id])
                        {
                            writer.WriteLineAsync(sub.Name);
                        }
                    }
                    writer.WriteLineAsync("End");
                    was[id] = true;
                }
                writer.WriteLineAsync("End");
            }
            for (int id = 0; id < manager.Size; id++)
            {
                if (!was[id])
                {
                    string date = manager.Tasks[id].Deadline.Date.ToString("dd/MM/yyyy");
                    if (date == "01/01/0001")
                    {
                        writer.WriteLineAsync(manager.Tasks[id].Name);
                    }
                    else
                    {
                        writer.WriteLineAsync(manager.Tasks[id].Name + ' ' + date);
                    }
                    if (manager.Subtasks.ContainsKey(id))
                    {
                        foreach (Task sub in manager.Subtasks[id])
                        {
                            writer.WriteLineAsync(sub.Name);
                        }
                    }
                    writer.WriteLineAsync("End");
                }
            }


        }
    }
    private void load(string file_name)
    {
        using (StreamReader reader = new StreamReader(file_name))
        {
            string text, group_name;
            int id;
            while ((text = reader.ReadLine()) != null)
            {
                string[] words = text.Split();
                if (words[0] == "Group")
                {
                    manager.create_group(words[1]);
                    group_name = words[1];
                    while (true)
                    {
                        text = reader.ReadLine();
                        if (text == "End")
                        {
                            break;
                        }
                        words = text.Split();
                        if (words.Length > 2)
                        {
                            id = manager.add(words[0], words[1]);
                        }
                        else
                        {
                            id = manager.add(words[0]);
                        }
                        manager.add_to_group(id, group_name);
                        while (true)
                        {
                            text = reader.ReadLine();
                            if (text == "End")
                            {
                                break;
                            }
                            words = text.Split();
                            manager.add_subtask(id, words[0]);
                        }


                    }
                }
                else
                {
                    if (words.Length > 2)
                    {
                        id = manager.add(words[0], words[1]);
                    }
                    else
                    {
                        id = manager.add(words[0]);
                    }
                    while (true)
                    {
                        text = reader.ReadLine();
                        if (text == "End")
                        {
                            break;
                        }
                        words = text.Split();
                        manager.add_subtask(id, words[0]);
                    }
                }


            }
        }
    }


}
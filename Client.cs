using System;
using System.IO;
using System.Text;
class MyClient : Client
{
    private Manager _manager;
    public override Manager Manager 
    {
        get { return _manager; }
        set { _manager = value; }
    }
    public MyClient(Manager manager)
    {
        _manager = manager;
    }
    public override void all()
    {
        bool[] was = new bool[Manager.Size];
        foreach (string key in Manager.Groups.Keys)
        {
            Console.WriteLine("Group: " + key);
            foreach (int id in Manager.Groups[key])
            {
                this.write_task(id);
                was[id] = true;
            }
            Console.WriteLine(" ");
        }
        for (int id = 0; id < Manager.Size; id++)
        {

            if (!was[id])
            {
                this.write_task(id);
            }
        }
    }
    public override void completed()
    {
        for (int id = 0; id < Manager.Size; id++)
        {
            if (!Manager.Tasks[id].IsDeleted && Manager.Tasks[id].IsCompleted)
            {
                this.write_task(id);
            }
        }
    }
    public override void completed(string name)
    {
        foreach (int id in Manager.Groups[name])
        {
            if (!Manager.Tasks[id].IsDeleted && Manager.Tasks[id].IsCompleted)
            {
                this.write_task(id);
            }
        }
    }
    public override void today()
    {
        string today = DateTime.Now.ToString("dd/MM/yyyy");
        for (int id = 0; id < Manager.Size; id++)
        {
            if (!Manager.Tasks[id].IsDeleted && Manager.Tasks[id].Deadline.ToString("dd/MM/yyyy") == today)
            {
                this.write_task(id);
            }
        }
    }
    public override void save(string file_name)
    {
        using (StreamWriter writer = new StreamWriter(file_name))
        {
            bool[] was = new bool[Manager.Size];
            foreach (string key in Manager.Groups.Keys)
            {
                writer.WriteLineAsync("Group" + ' ' + key);
                foreach (int id in Manager.Groups[key])
                {
                    string date = Manager.Tasks[id].Deadline.Date.ToString("dd/MM/yyyy");
                    if (date == "01/01/0001")
                    {
                        writer.WriteLineAsync(Manager.Tasks[id].Name);
                    }
                    else
                    {
                        writer.WriteLineAsync(Manager.Tasks[id].Name + ' ' + date);
                    }
                    if (Manager.Subtasks.ContainsKey(id))
                    {
                        foreach (Task sub in Manager.Subtasks[id])
                        {
                            writer.WriteLineAsync(sub.Name);
                        }
                    }
                    writer.WriteLineAsync("End");
                    was[id] = true;
                }
                writer.WriteLineAsync("End");
            }
            for (int id = 0; id < Manager.Size; id++)
            {
                if (!was[id])
                {
                    string date = Manager.Tasks[id].Deadline.Date.ToString("dd/MM/yyyy");
                    if (date == "01/01/0001")
                    {
                        writer.WriteLineAsync(Manager.Tasks[id].Name);
                    }
                    else
                    {
                        writer.WriteLineAsync(Manager.Tasks[id].Name + ' ' + date);
                    }
                    if (Manager.Subtasks.ContainsKey(id))
                    {
                        foreach (Task sub in Manager.Subtasks[id])
                        {
                            writer.WriteLineAsync(sub.Name);
                        }
                    }
                    writer.WriteLineAsync("End");
                }
            }


        }
    }
    public override void load(string file_name)
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
                    Manager.create_group(words[1]);
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
                            id = Manager.add(words[0], words[1]);
                        }
                        else
                        {
                            id = Manager.add(words[0]);
                        }
                        Manager.add_to_group(id, group_name);
                        while (true)
                        {
                            text = reader.ReadLine();
                            if (text == "End")
                            {
                                break;
                            }
                            words = text.Split();
                            Manager.add_subtask(id, words[0]);
                        }


                    }
                }
                else
                {
                    if (words.Length > 2)
                    {
                        id = Manager.add(words[0], words[1]);
                    }
                    else
                    {
                        id = Manager.add(words[0]);
                    }
                    while (true)
                    {
                        text = reader.ReadLine();
                        if (text == "End")
                        {
                            break;
                        }
                        words = text.Split();
                        Manager.add_subtask(id, words[0]);
                    }
                }


            }
        }
    }
    private void write_task(int id)
    {
        char cross;
        if (!Manager.Tasks[id].IsDeleted)
        {
            if (Manager.Tasks[id].IsCompleted)
            {
                cross = 'x';
            }
            else
            {
                cross = ' ';
            }
            string date = Manager.Tasks[id].Deadline.Date.ToString("dd/MM/yyyy");
            if (date == "01/01/0001")
            {
                date = " ";
            }
            Console.WriteLine("[" + cross + "]" + ' ' + "{" + id + "}" + ' ' + Manager.Tasks[id].Name + ' ' + date);
        }
        if (Manager.Subtasks.ContainsKey(id))
        {
            foreach (Task sub in Manager.Subtasks[id])
            {
                if (sub.IsCompleted || Manager.Tasks[id].IsCompleted)
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


}
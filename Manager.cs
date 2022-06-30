using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
class MyManager : Manager
{
    private List<Task> _tasks;
    Dictionary<int, List<Task>> _subtasks;
    private int _size;
    Dictionary<string, List<int>> _groups;
    public override List<Task> Tasks
    {
        get { return _tasks; }
        set { _tasks = value; }
    }
    public override Dictionary<int, List<Task>> Subtasks
    {
        get { return _subtasks; }
        set { _subtasks = value; }
    }
    public override int Size 
    {
        get { return _size; }
        set { _size = value; }
    }
    public override Dictionary<string, List<int>> Groups
    {
        get { return _groups; }
        set { _groups = value; }

    }
    public MyManager()
    {
        Tasks = new List<Task>();
        Subtasks = new Dictionary<int, List<Task>>();
        Groups = new Dictionary<string, List<int>>();
        Size = 0;
    }
    public override int add(string name)
    {
        Tasks.Add(new Task(name));
        ++Size;
        return (Size - 1);
    }
    public override int add(string name, string deadline)
    {
        Tasks.Add(new Task(name, DateTime.ParseExact(deadline, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        ++Size;
        return (Size - 1);
    }
    public override void delete(int id)
    {
        Tasks[id].IsDeleted = true;
    }

    public override void complete(int id)
    {
        if (Tasks[id].IsDeleted)
        {
            return;
        }
        if (Subtasks.ContainsKey(id))
        {
            bool check = false;
            foreach (Task sub in Subtasks[id])
            {
                if (check)
                {
                    check = false;
                    break;
                }
                if (!sub.IsCompleted)
                {
                    sub.IsCompleted = true;
                    check = true;
                }
            }
            if (check)
            {
                Tasks[id].IsCompleted = true;
            }
        }
        else
        {
            Tasks[id].IsCompleted = true;
        }
                
          
    }
    public override void create_group(string name)
    {
        List<int> id = new List<int>();
        Groups.Add(name, id);
    }
    public override void add_to_group(int id, string name)
    {

        Groups[name].Add(id);
    }
    public override void delete_group(string name)
    {
        Groups.Remove(name);
    }
    public override void delete_from_group(int id, string name)
    {
        Groups[name].Remove(id);
    }
    public override void add_subtask(int id, string name)
    {
        if (!Subtasks.ContainsKey(id))
        {
            List<Task> subtask = new List<Task>();
            Subtasks.Add(id, subtask);
        }
        Subtasks[id].Add(new Task(name));

    }



}
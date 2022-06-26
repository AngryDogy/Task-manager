using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
class Manager
{
    private List<Task> tasks;
    Dictionary<int, List<Task>> subtasks;
    private int size;
    Dictionary<string, List<int>> groups;
    public List<Task> Tasks
    {
        get { return tasks; }
        set { tasks = value; }
    }
    public Dictionary<int, List<Task>> Subtasks
    {
        get { return subtasks; }
        set { subtasks = value; }
    }
    public int Size
    {
        get { return size; }
        set { size = value; }
    }
    public Dictionary<string, List<int>> Groups
    {
        get { return groups; }
        set { groups = value; }
    }
    public Manager()
    {
        tasks = new List<Task>();
        subtasks = new Dictionary<int, List<Task>>();
        groups = new Dictionary<string, List<int>>();
        size = 0;
    }
    public int add(string name)
    {
        tasks.Add(new Task(name));
        ++size;
        return (size - 1);
    }
    public int add(string name, string deadline)
    {
        tasks.Add(new Task(name, DateTime.ParseExact(deadline, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        ++size;
        return (size - 1);
    }
    public void delete(int id)
    {
        for (int i = 0; i < tasks.Capacity; i++)
        {
            if (i == id)
            {
                tasks[i].IsDeleted = true;
                break;
            }
        }
    }

    public void complete(int id)
    {
        for (int i = 0; i < Size; i++)
        {
            if (i == id)
            {
                if (subtasks.ContainsKey(id))
                {
                    bool check = false;
                    foreach (Task sub in subtasks[id])
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
                        tasks[i].IsCompleted = true;
                    }
                }
                else
                {
                    tasks[i].IsCompleted = true;
                }
                break;
            }
        }
    }
    public void create_group(string name)
    {
        List<int> id = new List<int>();
        groups.Add(name, id);
    }
    public void add_to_group(int id, string name)
    {

        groups[name].Add(id);
    }
    public void delete_group(string name)
    {
        groups.Remove(name);
    }
    public void delete_from_group(int id, string name)
    {
        groups[name].Remove(id);
    }
    public void add_subtask(int id, string name)
    {
        if (!subtasks.ContainsKey(id))
        {
            List<Task> subtask = new List<Task>();
            subtasks.Add(id, subtask);
        }
        subtasks[id].Add(new Task(name));

    }



}
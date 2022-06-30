using System;
using System.Collections.Generic;
abstract class Manager
{
    public abstract List<Task> Tasks {get; set;}
    public abstract  Dictionary<int, List<Task>> Subtasks {get; set;}
    public abstract  int Size {get; set;}
    public abstract  Dictionary<string, List<int>> Groups {get; set;}
    public abstract int add(string name);
    public abstract int add(string name, string deadline);
    public abstract void delete(int id);
    public abstract void complete(int id);
    public abstract void create_group(string name);
    public abstract void add_to_group(int id, string name);
    public abstract void delete_group(string name);
    public abstract void delete_from_group(int id, string name);
    public abstract void add_subtask(int id, string name);

}
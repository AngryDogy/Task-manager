using System;

class Task
{
    private string name;
    private bool isDeleted;
    private bool isCompleted;
    private DateTime deadline;
    public Task() { }
    public Task(string name_, DateTime deadline_)
    {
        name = name_;
        isCompleted = false;
        isDeleted = false;
        Deadline = deadline_;
    }
    public Task(string name_)
    {
        name = name_;
        isCompleted = false;
        isDeleted = false;
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public bool IsDeleted
    {
        get { return isDeleted; }
        set { isDeleted = value; }
    }
    public bool IsCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; }
    }
    public DateTime Deadline
    {
        get { return deadline; }
        set { deadline = value; }
    }


}
using System;

class Task
{
    private string _name;
    private bool _isDeleted;
    private bool _isCompleted;
    private DateTime _deadline;
    public Task() { }
    public Task(string name, DateTime deadline)
    {
        Name = name;
        IsCompleted = false;
        IsDeleted = false;
        Deadline = deadline;
    }
    public Task(string name)
    {
        Name = name;
        IsCompleted = false;
        IsDeleted = false;
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public bool IsDeleted
    {
        get { return _isDeleted; }
        set { _isDeleted = value; }
    }
    public bool IsCompleted
    {
        get { return _isCompleted; }
        set { _isCompleted = value; }
    }
    public DateTime Deadline
    {
        get { return _deadline; }
        set { _deadline = value; }
    }


}
using System;
abstract class Client
{
    public abstract Manager Manager { get; set; }
    public abstract void all();
    public abstract void completed();
    public abstract void completed(string name);
    public abstract void today();
    public abstract void save(string file_name);
    public abstract void load(string file_name);
}
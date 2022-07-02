using System;
class Program
{
    static void Main(string[] args)
    {
        Client client = new MyClient(new MyManager());
        Reader.start(client);
    }
}
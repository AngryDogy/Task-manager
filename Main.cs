using System;
class Program
{
    static void Main(string[] args)
    {
        Manager manager = new Manager();
        Client client = new Client(manager);
        client.start();
    }
}
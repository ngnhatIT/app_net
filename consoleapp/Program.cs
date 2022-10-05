using MongoDB.Driver;

namespace consoleapp;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var client = new MongoClient("mongodb+srv://enter0208:8NzaSkZdvPE6MU9@cluster0.cixty.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            Console.WriteLine("Connection mongo success.");
            Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine("Connection mongo fail." + e.Message.ToString());
        }
    }
}
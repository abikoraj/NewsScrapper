using ConsoleApp1;

class Program
{

    static void Main(string[] args)
    {
        List<int> list = new List<int>();
        Client c1=new Client();
        Client c2=new Client();
        Client c3=new Client();

        c1.y = 1;
       

        Client.x = 10;
        list.Add(Client.x);



    }

  
}
using System;
using System.Threading;
namespace ConsoleApplication
{
    public class Program
    {
       public static void Main(string[] args)
        {
        if (args.Length == 0)
            {
                Console.WriteLine("Not enough things on the thing!");
                return;
            }
            var url = args[0];
            _get(url);
               Console.ReadLine();
           // Thread.Sleep(20000);

        }

        static async void _get(string url)
        {
             var result = await RoadConsole.MarkdownGetter.GetMarkdown(url);
             
             Console.WriteLine(result);
        }
    }
}

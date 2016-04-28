using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoadConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = args[0];
            _get(url);

            Thread.Sleep(20000);

        }

        static async void _get(string url)
        {
            Console.WriteLine(await MarkdownGetter.GetMarkdown(url));
        }
    }
}

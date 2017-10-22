using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(105, 28);
            Console.BufferHeight = 28;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Title = "Project Explorer.";
            Console.Clear();
            Main run = new Main();
            List<string> projectFiles = run.retrieveProjects();
            List<pagedData> filepages = run.castToPages(projectFiles);
            run.programLoop(filepages, 0, 0);
        }
    }
}

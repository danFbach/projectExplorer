using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer
{
    class Main
    {
        printUtil p = new printUtil();

        public static class SafeFileEnumerator
        {
            public static IEnumerable<string> EnumerateDirectories(string parentDirectory, string searchPattern, SearchOption searchOpt)
            {
                try
                {
                    var directories = Enumerable.Empty<string>();
                    if (searchOpt == SearchOption.AllDirectories)
                    {
                        directories = Directory.EnumerateDirectories(parentDirectory)
                            .SelectMany(x => EnumerateDirectories(x, searchPattern, searchOpt));
                    }
                    return directories.Concat(Directory.EnumerateDirectories(parentDirectory, searchPattern));
                }
                catch(PathTooLongException PTLex)
                {
                    return Enumerable.Empty<string>();
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Enumerable.Empty<string>();
                }
            }

            public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOpt)
            {
                try
                {
                    var dirFiles = Enumerable.Empty<string>();
                    if (searchOpt == SearchOption.AllDirectories)
                    {
                        dirFiles = Directory.EnumerateDirectories(path)
                                            .SelectMany(x => EnumerateFiles(x, searchPattern, searchOpt));
                    }
                    return dirFiles.Concat(Directory.EnumerateFiles(path, searchPattern));
                }
                catch (PathTooLongException PTLex)
                {
                    return Enumerable.Empty<string>();
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Enumerable.Empty<string>();
                }
            }
        }
        public List<string> retrieveProjects()
        {
            List<string> filePack = new List<string>();
            foreach (string dir in SafeFileEnumerator.EnumerateDirectories(@"C:\Users\Dan DCC\Documents\", "Visual Studio*", SearchOption.AllDirectories))
            {
                foreach(string file in SafeFileEnumerator.EnumerateFiles(dir, "*.sln", SearchOption.AllDirectories))
                {
                    filePack.Add(file);
                }
            }
            filePack.OrderBy(x => x.Split('\\').Last());
            return filePack;
        }
        public List<pagedData> castToPages(List<string> projects)
        {
            int pageCount = (projects.Count() / 10);
            List<pagedData> pages = new List<pagedData>();
            for(int i = 0; i <= pageCount; i++)
            {
                pagedData thisPage = new pagedData();
                thisPage.pageNumber = i;
                thisPage.files = projects.Skip(i * 10).Take(10).ToList();
                pages.Add(thisPage);
            }
            return pages;
        }
        public void listProjects(List<pagedData> pages, int currentPage, int currentEntry)
        {
            int count = 0;
            p.topBarWithCurDir();
            foreach(string filename in pages[currentPage].files)
            {
                if(count == currentEntry)
                {
                    p.curPageEntry(count, filename.Split('\\').Last());
                }
                else
                {
                    p.pageEntry(count, filename.Split('\\').Last());
                }
                count += 1;
            }
            p.pagedBottomBar(currentPage, (pages.Count() - 1));
        }
        public void programLoop(List<pagedData> pages, int currentPage, int currentEntry)
        {
            input i = new input();
            List<char> numbs = new List<char> { '0','1','2','3','4','5','6','7','8','9'};
            listProjects(pages, currentPage, currentEntry);
            ConsoleKeyInfo k = i.getKeystroke();
            if(k.Key == ConsoleKey.Enter)
            {
                Process.Start(pages[currentPage].files[currentEntry]);
                p.resetConsole(0);
                programLoop(pages, 0, 0);
            }
            else if (numbs.Contains(k.KeyChar))
            {
                int selection = 0;
                bool result = int.TryParse(k.KeyChar.ToString(), out selection);
                if (result)
                {
                    if (selection < pages[currentPage].files.Count())
                    {
                        Process.Start(pages[currentPage].files[selection]);
                        p.resetConsole(0);
                        programLoop(pages, 0, 0);
                    }
                    else
                    {
                        p.write(p.br + " Invalid Selection.", p.red);
                        p.resetConsole(750);
                        programLoop(pages, currentPage, currentEntry);
                    }
                }
            }
            else
            {
                List<int> pageUpdate = i.analyzeKey(k, currentPage, currentEntry, (pages.Count() - 1));
                p.resetConsole(0);
                programLoop(pages, pageUpdate[0], pageUpdate[1]);
            }
        }
    }
}

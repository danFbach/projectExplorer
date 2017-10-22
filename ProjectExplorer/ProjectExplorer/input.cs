using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer
{
    class input
    {
        printUtil p = new printUtil();

        public ConsoleKeyInfo getKeystroke()
        {
            List<string> options = new List<string>{ " Enter) Open Project", " Left/Right Arrow) Prev/Next Page", " Up/Down Arrow) Select Project",  " Esc) Exit"};
            foreach(string s in options) { p.write(p.br + s, p.wht); }
            return p.rk(p.br, p.gray, p.grn);
        }
        public List<int> analyzeKey(ConsoleKeyInfo k, int currentPage, int currentEntry, int lastPage)
        {
            List<int> pageInfo = new List<int>();
            pageInfo.Add(currentPage);
            pageInfo.Add(currentEntry);
            if (k.Key == ConsoleKey.LeftArrow)
            {
                pageInfo[0] -= 1;
                if (pageInfo[0] == -1) { pageInfo[0] = lastPage; }
                pageInfo[1] = 0;
                return pageInfo;
            }
            else if(k.Key == ConsoleKey.RightArrow)
            {
                pageInfo[0] += 1;
                if (pageInfo[0] == (lastPage + 1)) { pageInfo[0] = 0; }
                pageInfo[1] = 0;
                return pageInfo;
            }
            else if (k.Key == ConsoleKey.UpArrow)
            {
                pageInfo[1] -= 1;
                if (pageInfo[1] == -1) { pageInfo[1] = 9; }
                return pageInfo;
            }
            else if (k.Key == ConsoleKey.DownArrow)
            {
                pageInfo[1] += 1;
                if (pageInfo[1] == 10) { pageInfo[1] = 0; }
                return pageInfo;
            }
            else if (k.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
                return pageInfo;
            }
            else
            {
                return pageInfo;
            }
        }
    }
}

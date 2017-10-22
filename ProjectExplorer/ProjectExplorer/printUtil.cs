﻿using System;
using System.Threading;

namespace ProjectExplorer
{
    public class printUtil
    {
        #region globalVars

        public string blue = "blue";
        public string drkblue = "darkblue";
        public string cyan = "cyan";
        public string drkcyan = "darkcyan";
        public string gray = "gray";
        public string drkGray = "darkgray";
        public string grn = "green";
        public string drkGrn = "darkgreen";
        public string mgnta = "magenta";
        public string drkmgnta = "darkmagenta";
        public string red = "red";
        public string drkRed = "darkred";
        public string ylw = "yellow";
        public string gold = "darkyellow";
        public string wht = "white";
        public string blk = "black";
        public string bar = "_____________________________________________________________________________________________________";
        public string space = @"                                                                                                                                                                                                        ";
        public string br = "\n\r ";

        #endregion globalVars

        #region printTools
        public void rest(int rest)
        {
            Thread.Sleep(rest);
        }
        public void resetConsole(int rest)
        {
            Thread.Sleep(rest);
            Console.Clear();
        }
        public void pause() { write(br + "Press any Key to Continue.", red); Console.ReadKey(); }

        #endregion printTools

        #region Reading

        public ConsoleKeyInfo rk(string _input, string colorOUT, string colorIN)
        {
            ///RESET BACKGROUND TO BLACK IF BLACK FOREGROUND IS CHOSEN AT ANY POINT

            ConsoleKeyInfo returnData;
            if (colorOUT != null) { colorIN = colorIN.ToLower(); }
            pickColor(colorOUT);
            Console.Write(_input);
            if (colorIN != null) { colorIN = colorIN.ToLower(); }
            pickColor(colorIN);
            returnData = Console.ReadKey(true);
            if (returnData.Key == ConsoleKey.LeftArrow || returnData.Key == ConsoleKey.RightArrow) { return returnData; }
            else
            {
                rest(500);
                return returnData;
            }
        }

        #endregion Reading

        #region printing
        public void write(string _input, string color)
        {
            ///RESET BACKGROUND TO BLACK IF BLACK FOREGROUND IS CHOSEN AT ANY POINT
            Console.BackgroundColor = ConsoleColor.Black;
            if (color != null)
            {
                color = color.ToLower();
            }
            pickColor(color);
            Console.Write(_input);
        }
        

        /// Color Picker
        public void pickColor(string color)
        {
            switch (color)
            {
                /// NORMAL COLORS
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "gray":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                /// DARK COLORS
                case "darkblue":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "darkcyan":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case "darkgray":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "darkgreen":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "darkmagenta":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "darkred":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "darkyellow":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "white":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "black":
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
        }

        #endregion printing

        #region printFormatting

        public void topBarWithCurDir()
        {
            write((space).Substring(0,  79) + "| ProjectExplorer v0.1 |" + br, drkGray);
            write(" " + bar + br + "|", blue);
            write(" Index", wht);
            write("--", drkGray);
            write("|", blue);
            write("-- ", drkGray);
            write("Directory / File Name ", wht);
            write("                                                                   |", blue);
        }

        public void pageEntry(int indexNumber, string entry)
        {
            string entryIndex = String.Format("{0})", indexNumber.ToString());
            string entryFile = entry;
            write(br + "|", blue);
            write(" - ", drkGray);
            write(entryIndex, grn);
            write(" --", drkGray);
            write("|", blue);
            write("-- ", drkGray);
            write(entry + space.Substring(0, (89 - entry.Length)), ylw);
            write("|", blue);
        }
        public void curPageEntry(int indexNumber, string entry)
        {
            string entryIndex = String.Format("{0})", indexNumber.ToString());
            string entryFile = entry;
            write(br + "|", blue);
            write(" - ", drkGray);
            write(entryIndex, grn);
            write(" --", drkGray);
            write("|", blue);
            write("-- ", drkGray);
            write(entry + space.Substring(0, (89 - entry.Length)), grn);
            write("|", blue);
        }
        public void pagedBottomBar(int curPage, int lastPage)
        {
            int lastPgStrClip = 5 - (lastPage.ToString().Length - 1);
            int curPgStrClip = 71 - (curPage.ToString().Length + lastPage.ToString().Length);
            write(br + "|_ ", blue);
            write("Current Page ", drkGray);
            write("[", blue);
            write(curPage.ToString(), grn);
            write(("]" + bar).Substring(0, curPgStrClip), blue);
            write(" Last Page ", drkGray);
            write("[", blue);
            write(lastPage.ToString(), wht);
            write("]_" + "|", blue);
        }
        #endregion printFormatting
    }
}
﻿using EquationSolver.Equation;
using ConsoleInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    public class UserInreface
    {

        private static void ShowTitles()
        {
            List<string> titles = new List<string>() {
                "TeamLeader",
                "Насибуллин Даниил",
                "", "Команда тестировки", "",
                "Бимаков Даниил",
                "Федотов Павел",
                "Меркульев Никита",
                "", "Команда разработчиков библиотеки", "",
                "Шкляев Дмитрий",
                "Колбин Илья",
                "","Команда разработчиков UI", "",
                "Костенков Даниил",
                "Соболев Артур",
                "Широбоков Илья"};

            for (int i = 0; ; i++)
            {
                Thread.Sleep(400);
                int position = i;
                foreach (var title in titles)
                {
                    position = position - 2;
                    if (position < 0) break;
                    else
                    {
                        ConsoleScreen.addLine(new String(' ', Math.Abs((Console.WindowWidth / 2) - (title.Length / 2))) + title, position);
                    }

                }

                ConsoleScreen.renderConsoleScreen();
                Console.SetCursorPosition(0, 0);
                ConsoleScreen.clearLines();
            }
        }

        public static void SelectUserAction()
        {
            const string SelectedMarker = "## ";
            const string UnSelectedMarker = "-- ";

            bool isOpen = true;
            int selectIndex = 0;
            var elections = new List<string>()
            {
                    "Ршеить уравнение",
                    "История",
                    "Титры",
            };

            while (isOpen)
            {
                
                for (int i = 0; i < elections.Count; i++)
                {
                    if (i == selectIndex)
                    {
                        ConsoleScreen.addLine(SelectedMarker + elections[i], i);
                    }
                    else
                    {
                        ConsoleScreen.addLine(UnSelectedMarker + elections[i], i);
                    }
                }

                ConsoleScreen.renderConsoleScreen();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                ConsoleKey keyConsole = keyInfo.Key;
                char keyChar = keyInfo.KeyChar;

                if (keyConsole == ConsoleKey.DownArrow) 
                {
                    if ((selectIndex < elections.Count-1))
                    {
                        selectIndex++;
                    }
                }
                else if (keyConsole == ConsoleKey.UpArrow)
                {
                    if (selectIndex > 0)
                    {
                        selectIndex--;
                    }
                }
                else if (keyConsole == ConsoleKey.Enter)
                {
                    switch (selectIndex) 
                    {
                        case 0:
                            string quadration = EquationValidator.InputEquation();
                            break;
                        case 1:
                            
                            break;
                        case 2:
                            ShowTitles();
                            break;
                    }
                }
            }
        }
    }
}

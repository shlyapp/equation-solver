namespace ConsoleInterface
{
    public static class ConsoleScreen
    {
        //Список строк консоли(приватные, т.к. что бы нельзя было изменить извне, что бы не возникали ошибки)
        private static List<String> lines = new List<String>() { "" };

        /// <summary>
        /// Заготавливает строку line в строке консоли номер index
        /// </summary>
        public static void SetLine(String line, int index)
        {
            if (index < 0)
            {
                lines.Add(line);
            }
            //Если индекс слишком большой, и до этого не были добавлены промежуточные строки
            //то добавляет их вплоть до нужного индекса
            else if (index >= lines.Count)
            {
                for (int i = lines.Count - index; i < 0; i++)
                {
                    lines.Add("");
                }
                lines.Add(line);
            }
            else if (index >= 0 && index < lines.Count)
            {
                lines[index] = line;
            }
        }


        /// <summary>
        /// Очищает все заготовленные строки
        /// </summary>
        public static void ClearLines()
        {
            lines.Clear();
            SetLine("", 0);
        }


        /// <summary>
        /// Отображает все заготовленные строки
        /// </summary>
        public static void RenderConsoleScreen()
        {
            Console.Clear();

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
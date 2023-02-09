namespace ConsoleInterface
{
    public static class ConsoleScreen
    {
        //Список строк консоли
        public static List<String> lines = new List<String>() { "" };

        //Добавляет строку в список строк экрана консоли
        //Так же может их и заменять
        public static void addLine(String line, int index)
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

        //Очищает строки консоли
        public static void clearLines()
        {
            lines.Clear();
            lines[0] = "";
        }

        //Отображает строки консоли в порядке
        public static void renderConsoleScreen()
        {
            Console.Clear();

            foreach (var line in lines)
            {
                foreach (var letter in line)
                    Console.Write(letter);

                Console.WriteLine();
            }
        }
    }
}
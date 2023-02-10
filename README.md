# EquationSolver - консольное приложение для решения квадратных уравнений

### Функционал программы:
- решение квадратных уравнений
- проверка в реальном времени на корректность ввода
- возможность сохранения и просмотра истории введенных уравнений

## Краткая документация
### Структура проекта:
`Program.cs` - содержит метод Main, точку входа в программу

`UserInterface.cs` - отвечает за пользовательский интерфейс, дает выбор пользователю

`Equation\EquationLogger.cs` - отвечает за сохранения результатов посчитанных уравнений

`Equation\EquationParser.cs` - отвечает за парсинг (получение) коэффициентов из введенного уравнения

`Equation\EquationValidator.cs` - отвечает за ввод пользователем квадратного уравнения, не дает ввести некорректные символы

`ConsoleInterfaceLibrary\ConsoleScreen.cs` - отвечает за вывод строк в консоль

`EquationSolverLibrary\EquationSolver.cs` - отвечает за решение квадртных уравнений

`Tests\UnitTest.cs` - тестирования библиотеки `EquationSolver.cs`

### Краткая логика работы:
1. Методом `UserInterface.SelectUserAction()` передаем управление пользователю и даем способ выбрать действие <br>
2. Если пользователь выбрал "0", то методом `EquationValidator.InputEquation()` запрашиваем на ввод строку (автоматическая проверка на корректность ввода) <br>
  2.1 Полученная строка передается в метод `EquationParser.ReturnCoaficients()`, который возвращает массив коэффциентов <br>
  2.2 Полученный массив коэффициентов передаем в метод `EquationSolver.SolveQuadraticEquation()`, который возвращаем массив найденных корней <br>
  2.3 Создаем обьект класса `EquationLogger logger`, передаем в конструктор имя файла, в который будет записываться история уравнений <br>
  2.4 При помощи метода `logger.AddEquationSolving()` записываем уравнение и корни в файл <br>
3. Если пользователь выбрад "1", то выводится информация (история) о всех предыдущих решенных уравнениях. <br>
  3.1 Вызывается метод `ShowHistory()`, в котором создается обьект класса EquationLogger logger <br>
  3.2 `logger.GetGetHistory()` - получаем `List<String>` со всеми решенным уравнениями и корнями к ним<br>
  3.3 Выводим `List` в консоль <br>
4. Если пользователь выбрал "2", то выводится титры или просто информация о разработчиках

### Описание методов:
#### `UserInterface.cs`
`void ShowTitles()` - отображении информации о разработчиках <br>
`void ShowResults(double[] results, string equation)` - отображение результатов посчитанного уравнения <br>
`void ShowHistory()` - отображении истории введенных уравнений <br>
`void SelectUserAction()` - выбор пользователем пункта меню<br>

#### `EquationSolver.cs`
`string Reverse(string text)` - возвращает перевернутую строку <br>
`double[] ReturnCoaficents(string equation)` - возвращает полученные коэффициенты из уравнения<br>
`double CoaficentCath(string equation, string variable)` - возвращает конкретный коэффициент<br>
`double CoaficentCath(string equation, string variable, string[] whiteList)` - перегрузка `double CoaficentCath(string equation, string variable)`<br>
`double CoaficentCath(string equation)` - перегрузка `double CoaficentCath(string equation, string variable)`<br>
`double[] SolveQuadraticEquation(double A, double B, double C, double D, double E, double F)` - решение квадратного уравнения, возвращает корни уравнения<br>

#### `EquationValidator.cs`
`bool NumberPlaceCondition(string line, char number, int index)` - проверка на максимальную степень x<br>
`bool SimbolsPlaceCondition(string line, char simbol, int index)` - проверка на допустимые комбинации символов<br>
`bool LineCondition(string line)` - коррекность ввода сторки, наличие обеих частей уравнения<br>
`void Titles(List<string> titles)` - вывод титров<br>
`string InputEquation()` - посимвольный ввод уравнения<br>

#### `EquationLogger.cs`
`List<string> GetHistory()` - возвращает `List<String>` с историей уравнений и решений<br>
`void AddEquationSolving(string equation, double[] result)` - добавление уравнения и решения к общей истории<br>

#### `ConsoleScreen.cs`
`void addLine(String line, int index)` - добавление строки в список строк экрана консоли для выводы<br>
`void clearLines()` - очищаем буффер строк на вывод <br>
`void renderConsoleScreen()` - отображение строк в консоль по порядку <br>
`void renderConsoleScreen(bool True)` - перегрузка `void renderConsoleScreen()` <br>

## Установка
- скачать весь код можно клонировав репозиторий `https://github.com/shlyapp/equation-solver.git` <br>
- актуальная версия программы находится в `releases`<br>
- скачать проект Visual Studio 2022 можно по ссылке `https://drive.google.com/drive/folders/1hyeTsSlcVCCkyeQPB7w-aIcGGba7kBUB?usp=sharing`<br>

## Разработчики:
Насибуллин Данил - TeamLeader

Меркульев Никита, Бимаков Данил, Федотов Павел - команда тестировки

Шкляев Дмитрий, Колбин Илья - команда разработчиков библиотеки

Костенков Данил, Соболев Артур, Широбоков Илья - команда разработчиков UI

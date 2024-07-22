public class Program
{
    public static void Main(string[] args)
    {
        CountValidGames();
        
    }

    public static void CountValidGames()
    {
        int resultPart1 = 0;
        int resultPart2 = 0;
        string message = "";
        LinkedList<string> listWords = ReadFile("input.txt");
        LinkedList<int> gamesValids = new LinkedList<int>();//Guarda los juegos validos
        LinkedList<int> gamesInvalids = new LinkedList<int>();//Guarda los juegos invalidos
        Dictionary<string, int> setNumberPart2 = new Dictionary<string, int>();
        for (int i = 0; i < listWords.Count; i++)
        {
            string game = listWords.ElementAtOrDefault(i).Split(':')[1];
            if (GetGameData(game))
            {//Primer juego
                gamesValids.AddLast(i+1);//Guardo el juego valido
                resultPart1+=i+1;
            }
            resultPart2+= GetSumSetCubes(GetSetNumber(game),i+1);//Segundo juego

        }
        Console.WriteLine("----------Result---------");
        Console.WriteLine("Part 1: " + resultPart1);
        Console.WriteLine("Part 2: " + resultPart2);
    }
    public static int GetSumSetCubes(Dictionary<string, int> setNumber, int gameNumber)
    {
        int result = 0;
        string message = "Game "+gameNumber+":\n";
        for (int i = 0; i < setNumber.Count(); i++)
        {//Obtengo el valor de los cubos
            message+= setNumber.ElementAtOrDefault(i).Value+" "+setNumber.ElementAtOrDefault(i).Key+";";
            if (result > 0)
            {
                result = result * setNumber.ElementAtOrDefault(i).Value;//Multiplico los valores
            }
            else
            {
                result = setNumber.ElementAtOrDefault(i).Value;
            }
        }
        //Console.WriteLine(message);
        return result;
    }
    public static Dictionary<string,int> GetSetNumber(string game)
    {
        Dictionary<string, int> highValue = new Dictionary<string, int>()
        {
            {"red", 0},
            {"green", 0},
            {"blue", 0}
        };

        int position = 0;
        LinkedList<int> positionInvalid = new LinkedList<int>();
        foreach (string color in highValue.Keys)
        {//Primero obtengo el valor mas alto de cada color
            int highValueCube = 0;
            int positionHighValue = 0;
            foreach (string cube in game.Split(';'))
            {//Reviso en los cubos
                foreach (string colors in cube.Split(','))
                {//Obtengo el color mas alto
                    if (!positionInvalid.Contains(position))
                    {
                        if (colors.Contains(color))
                        {
                            int valueCube = int.Parse(colors.Replace(color, "").Replace(" ", ""));//Obtengo el valor del cubo
                            if (valueCube > highValueCube)
                            {//Guardo el valor mas alto
                                highValueCube = valueCube;
                                positionHighValue = position;//Guardo la posicion del cubo
                            }
                        }
                    }
                }
                position++;
            }
            //Guardo el valor del color y su posicion en el cubo 
            highValue[color] = highValueCube;
            positionInvalid.AddLast(positionHighValue);
        }

        return highValue;
    }
    public static bool GetGameData(string game) 
    {//Estamos en el juego
        int colorCubes = 0;
        Dictionary<string, int> gameRules = new Dictionary<string, int>()
        {
            {"red", 12},
            {"green", 13},
            {"blue", 14}
        };//Las reglas de juego
        //Estamos en los cubos
        foreach (string cube in game.Split(';'))
        {//En un Cubo
            foreach (string colors in cube.Split(','))
            {//revisando el cubo

                if (colors.Contains("red"))
                {//Elimino la letra y quito todos los espacios vacios para obtener el numero
                    colorCubes = int.Parse(colors.Replace("red", "").Replace(" ", ""));
                    if (colorCubes > gameRules["red"])
                    {//Si el numero de cubos es mayor al permitido
                        return false;
                    }
                }
                else if (colors.Contains("green"))
                {
                    colorCubes = int.Parse(colors.Replace("green", "").Replace(" ", ""));
                    if(colorCubes > gameRules["green"])
                    {
                        return false;
                    }
                }
                else if (colors.Contains("blue"))
                {
                    colorCubes = int.Parse(colors.Replace("blue", "").Replace(" ", ""));
                    if (colorCubes > gameRules["blue"])
                    {
                        return false;
                    }
                }
            }
        }

        return true;    
    }

    public static LinkedList<string> ReadFile(string fileName)
    {//Busca las palabras en el archivo de texto
        string file = @"C:\Users\samue\Downloads\Profesion\Advent of Code\Day-2\Solution_Day2\Resources\"+fileName;
        LinkedList<string> listWords = new LinkedList<string>();
        Console.WriteLine("Reading File using File.ReadAllText()");

        if (File.Exists(file))
        {
            StreamReader Textfile = new StreamReader(file);
            string line;

            while ((line = Textfile.ReadLine()) != null)
            {
                listWords.AddLast(line);
            }

            Textfile.Close();

        }
        return listWords;
    }
}
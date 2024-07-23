public class Program
{
    public static void Main(string[] args)
    {
        CountValidGames("input.txt");
        
    }

    public static void CountValidGames(string fileName)
    {
        int resultPart1 = 0;
        int resultPart2 = 0;
        LinkedList<string> listWords = ReadFile(fileName);
        LinkedList<int> gamesValids = new LinkedList<int>();
        LinkedList<int> gamesInvalids = new LinkedList<int>();
        Dictionary<string, int> setNumberPart2 = new Dictionary<string, int>();
        for (int i = 0; i < listWords.Count; i++)
        {
            string game = listWords.ElementAtOrDefault(i).Split(':')[1];
            if (GetGameData(game))
            {
                gamesValids.AddLast(i+1);
                resultPart1+=i+1;
            }
            resultPart2+= GetSumSetCubes(GetSetNumber(game),i+1);

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
        {
            message+= setNumber.ElementAtOrDefault(i).Value+" "+setNumber.ElementAtOrDefault(i).Key+";";
            if (result > 0)
            {
                result = result * setNumber.ElementAtOrDefault(i).Value; 
            }
            else
            {
                result = setNumber.ElementAtOrDefault(i).Value;
            }
        } 
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
        { 
            int highValueCube = 0;
            int positionHighValue = 0;
            foreach (string cube in game.Split(';'))
            { 
                foreach (string colors in cube.Split(','))
                { 
                    if (!positionInvalid.Contains(position))
                    {
                        if (colors.Contains(color))
                        {
                            int valueCube = int.Parse(colors.Replace(color, "").Replace(" ", "")); 
                            if (valueCube > highValueCube)
                            { 
                                highValueCube = valueCube;
                                positionHighValue = position; 
                            }
                        }
                    }
                }
                position++;
            }
 
            highValue[color] = highValueCube;
            positionInvalid.AddLast(positionHighValue);
        }

        return highValue;
    }
    public static bool GetGameData(string game) 
    { 
        int colorCubes = 0;
        Dictionary<string, int> gameRules = new Dictionary<string, int>()
        {
            {"red", 12},
            {"green", 13},
            {"blue", 14}
        }; 
         
        foreach (string cube in game.Split(';'))
        { 
            foreach (string colors in cube.Split(','))
            { 

                if (colors.Contains("red"))
                { 
                    colorCubes = int.Parse(colors.Replace("red", "").Replace(" ", ""));
                    if (colorCubes > gameRules["red"])
                    { 
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
    {

        string executableDir = AppDomain.CurrentDomain.BaseDirectory;
        string projectDir = Path.GetFullPath(Path.Combine(executableDir, @"..\..\.."));
        string relativePath = Path.Combine(projectDir, "Resources", fileName);
        LinkedList<string> listWords = new LinkedList<string>();
        if (File.Exists(relativePath))
        {
            StreamReader Textfile = new StreamReader(relativePath);
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
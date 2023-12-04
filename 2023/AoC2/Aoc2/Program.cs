using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] calibrationDocument = ConvertToTable("test.txt");

        if (calibrationDocument != null)
        {
            int sum = CalculateSum(calibrationDocument);
            Console.WriteLine(sum);
            sum = CalculateSum2(calibrationDocument);
            Console.WriteLine(sum);
        }
    }

    static string[] ConvertToTable(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            return lines;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    static int CalculateSum(string[] calibrationDocument)
    {
        int targetRed = 12;
        int targetGreen = 13;
        int targetBlue = 14;

        int ans = 0;

        foreach (string line in calibrationDocument)
        {
            Match match = Regex.Match(line, @"Game (\d+): (.*)");

            if (match.Success)
            {
                int gameIndex = int.Parse(match.Groups[1].Value);
                string cubeCounts = match.Groups[2].Value;

                string[] subsets = cubeCounts.Split(';');

                bool isValidGame = false;

                foreach (string subset in subsets)
                {
                    MatchCollection matches = Regex.Matches(subset, @"\d+ (red|green|blue)");

                    int redCount = 0;
                    int greenCount = 0;
                    int blueCount = 0;

                    foreach (Match cubeMatch in matches)
                    {
                        string[] parts = cubeMatch.Value.Split();
                        int count = int.Parse(parts[0]);

                        if (parts[1] == "red")
                        {
                            redCount += count;
                        }
                        else if (parts[1] == "green")
                        {
                            greenCount += count;
                        }
                        else if (parts[1] == "blue")
                        {
                            blueCount += count;
                        }
                    }

                    if (redCount > targetRed || greenCount > targetGreen || blueCount > targetBlue)
                    {
                        isValidGame = true;
                        break;
                    }
                }

                if (!isValidGame)
                {
                    ans += gameIndex;
                }
            }
        }

        return ans;
    }

    static int CalculateSum2(string[] txt)
    {
        int ans = 0;

        foreach (string line in txt)
        {
            Match match = Regex.Match(line, @"Game (\d+): (.*)");

            if (match.Success)
            {
                string cubeCounts = match.Groups[2].Value;

                string[] subsets = cubeCounts.Split(';');

                int[] minCounts = new int[3] { int.MinValue, int.MinValue, int.MinValue };

                foreach (string subset in subsets)
                {
                    MatchCollection matches = Regex.Matches(subset, @"\d+ (red|green|blue)");

                    foreach (Match cubeMatch in matches)
                    {
                        string[] parts = cubeMatch.Value.Split();
                        int count = int.Parse(parts[0]);

                        if (parts[1] == "red" && count > minCounts[0])
                        {
                            minCounts[0] = count;
                        }
                        else if (parts[1] == "green" && count > minCounts[1])
                        {
                            minCounts[1] = count;
                        }
                        else if (parts[1] == "blue" && count > minCounts[2])
                        {
                            minCounts[2] = count;
                        }
                    }
                }
                int gamePower = minCounts[0] * minCounts[1] * minCounts[2];
                ans += gamePower;
            }
        }

        return ans;
    }
}

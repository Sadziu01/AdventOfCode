using System.Text.RegularExpressions;

class Program
{
    struct StarInfo
    {
        public int Value;
        public int Neighbors;

        public StarInfo(int value)
        {
            Value = value;
            Neighbors = 1;
        }
    }

    static void Main()
    {
        string[] calibrationDocument = ConvertToTable("test.txt");

        if (calibrationDocument != null)
        {
            int sum = CalculateSum(calibrationDocument);
            Console.WriteLine(sum);
            sum = CalculateSum2(calibrationDocument);
            Console.WriteLine(sum);
        };
    }

    static string[] ConvertToTable(string filePath)
    {
        try
        {
            return File.ReadAllLines(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    static int CalculateSum(string[] txt)
    {
        int ans = 0;
        for (int i = 0; i < txt.Length; i++)
        {
            string line = txt[i];
            for (Match m = Regex.Match(line, @"\d+"); m.Success; m = m.NextMatch())
            {
                int numLength = m.Value.Length;
                bool isValid = false;

                for (int j = i - 1; j <= i + 1; j++)
                {
                    for (int k = m.Index - 1; k <= m.Index + numLength; k++)
                    {
                        if (j >= 0 && j < txt.Length && k >= 0 && k < line.Length && txt[j][k] != '.' && !Char.IsDigit(txt[j][k]))
                        {
                            isValid = true;
                        }
                    }
                }

                if (isValid)
                {
                    ans += int.Parse(m.Value);
                }
            }
        }
        return ans;
    }

    static int CalculateSum2(string[] txt)
    {
        int ans = 0;
        Dictionary<Tuple<int, int>, StarInfo> starData = new Dictionary<Tuple<int, int>, StarInfo>();

        for (int i = 0; i < txt.Length; i++)
        {
            string line = txt[i];
            for (Match m = Regex.Match(line, @"\d+"); m.Success; m = m.NextMatch())
            {
                int numLength = m.Value.Length;

                for (int j = i - 1; j <= i + 1; j++)
                {
                    for (int k = m.Index - 1; k <= m.Index + numLength; k++)
                    {
                        if (j >= 0 && j < txt.Length && k >= 0 && k < line.Length &&
                            txt[j][k] == '*' && !Char.IsDigit(txt[j][k]))
                        {
                            Tuple<int, int> starCoordinates = new Tuple<int, int>(j, k);

                            if (!starData.TryGetValue(starCoordinates, out StarInfo starInfo))
                            {
                                starInfo = new StarInfo(int.Parse(m.Value));
                                starData.Add(starCoordinates, starInfo);
                            }
                            else
                            {
                                starInfo.Value *= int.Parse(m.Value);
                                starInfo.Neighbors++;
                                starData[starCoordinates] = starInfo;
                            }
                        }
                    }
                }
            }
        }

        foreach (var star in starData)
        {
            if (star.Value.Neighbors > 1)
            {
                ans += star.Value.Value;
            }
        }

        return ans;
    }
}

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

    static int CalculateSum(string[] txt)
    {
        int sum = 0;

        foreach (string line in txt)
        {
            int firstDigit = FirstDigit(line);
            int lastDigit = LastDigit(line);

            int value = firstDigit * 10 + lastDigit;
            sum += value;
        }

        return sum;
    }

    static int FirstDigit(string line)
    {
        foreach (char c in line)
        {
            if (char.IsDigit(c))
            {
                return int.Parse(c.ToString());
            }
        }
        return 0;
    }

    static int LastDigit(string line)
    {
        for (int i = line.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(line[i]))
            {
                return int.Parse(line[i].ToString());
            }
        }
        return 0;
    }


    static int CalculateSum2(string[] txt)
    {
        string pattern = @"(?:one|two|three|four|five|six|seven|eight|nine|\d)";
        int ans = 0;

        foreach (string line in txt)
        {
            MatchCollection matches = Regex.Matches(line, pattern);

            int firstNumeral = ConvertDigit(matches[0].Value);
            int lastNumeral = ConvertDigit(matches[matches.Count - 1].Value);

            int value = firstNumeral * 10 + lastNumeral;
            ans += value;
        }

        return ans;
    }


    static int ConvertDigit(string digit)
    {
        if (int.TryParse(digit, out int result))
        {
            return result % 10;
        }
        else
        {
            switch (digit)
            {
                case "one": return 1;
                case "two": return 2;
                case "three": return 3;
                case "four": return 4;
                case "five": return 5;
                case "six": return 6;
                case "seven": return 7;
                case "eight": return 8;
                case "nine": return 9;
                default: return 0;
            }
        }
    }
}

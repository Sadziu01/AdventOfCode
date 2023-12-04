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

        foreach (string line in txt)
        {
            Match match = Regex.Match(line, @"Card\s+(\d+):\s+(.*) \|\s+(.*)");

            if (match.Success)
            {
                string[] winningNumbers = match.Groups[2].Value.Split(' ').Where(num => !string.IsNullOrWhiteSpace(num)).ToArray();
                string[] numbers = match.Groups[3].Value.Split(' ').Where(num => !string.IsNullOrWhiteSpace(num)).ToArray();

                int points = 0;

                foreach (string number in winningNumbers)
                {
                    if (Array.Exists(numbers, wn => wn == number))
                    {
                        points++;
                    }
                }
                ans += (int)Math.Pow(2, points - 1);
            }
        }

        return ans;
    }

    static int CalculateSum2(string[] txt)
    {
        int ans = 0;
        Dictionary<int, int> cards = new Dictionary<int, int>();

        foreach (string line in txt)
        {
            Match match = Regex.Match(line, @"Card\s+(\d+):\s+(.*) \|\s+(.*)");

            if (match.Success)
            {
                int id = int.Parse(match.Groups[1].Value);
                string[] winningNumbers = match.Groups[2].Value.Split(' ').Where(num => !string.IsNullOrWhiteSpace(num)).ToArray();
                string[] numbers = match.Groups[3].Value.Split(' ').Where(num => !string.IsNullOrWhiteSpace(num)).ToArray();

                int count = 0;

                foreach (string number in winningNumbers)
                {
                    if (Array.Exists(numbers, wn => wn == number))
                    {
                        count++;
                    }
                }

                if (!cards.ContainsKey(id))
                {
                    cards[id] = 0;
                }

                cards[id]++;

                for (int i = id + 1; i <= id + count; i++)
                {
                    if (!cards.ContainsKey(i))
                    {
                        cards[i] = 0;
                    }

                    cards[i] += cards[id];
                }

                ans += cards[id];
            }
        }

        return ans;
    }
}
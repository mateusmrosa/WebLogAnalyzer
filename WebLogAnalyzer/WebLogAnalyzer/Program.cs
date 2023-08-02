using System.Net;
using System.Text.RegularExpressions;

class MainClass
{
    static void Main()
    {
        WebClient client = new WebClient();
        string logData = client.DownloadString("https://coderbyte.com/api/challenges/logs/web-logs-raw");

        if (!string.IsNullOrEmpty(logData))
        {
            Dictionary<string, int> idCount = ExtractIds(logData);
            string output = FormatOutPut(idCount);
            Console.WriteLine(output);
        }
    }

    static Dictionary<string, int> ExtractIds(string logData)
    {
        Dictionary<string, int> idCount = new Dictionary<string, int>();
        string pattern = @"\?shareLinkId=(\w+)";

        foreach (Match match in Regex.Matches(logData, pattern))
        {
            string id = match.Groups[1].Value;
            if (idCount.ContainsKey(id))
                idCount[id]++;
            else
                idCount[id] = 1;
        }
        return idCount;
    }

    static string FormatOutPut(Dictionary<string, int> idCount)
    {
        List<string> outputList = new List<string>();

        foreach (var kvp in idCount)
        {
            string id = kvp.Key;
            int count = kvp.Value;

            if (count > 1)
                id += ":" + count;

            outputList.Add(id);
        }
        return string.Join(Environment.NewLine, outputList);
    }

}
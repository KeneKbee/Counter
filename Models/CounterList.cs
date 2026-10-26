using System.Collections.ObjectModel;
using System.Text.Json;

namespace Counter.Models;

class CounterList
{
    public ObservableCollection<Counter> CounterCollection { get; private set; } = new();

    public string HeadingText => "Your Counters:";
    public string AddCounterButtonText => "ADD COUNTER";

    public CounterList()
    {
        LoadCounters();
    }

    public string GetSubHeadingText()
    {
        return $"Currently You Have {CounterCollection.Count}";
    }

    public void LoadCounters()
    {
        CounterCollection.Clear();

        FileInfo[] sortedFiles = GetSortedFiles();
        FillCounterCollection(sortedFiles);
    }

    private FileInfo[] GetSortedFiles()
    {
        DirectoryInfo directoryInfo = new(FileSystem.Current.AppDataDirectory);
        var sortedFiles = directoryInfo.GetFiles().OrderBy(file => file.CreationTime).ToArray();

        return sortedFiles;
    }

    private void FillCounterCollection(FileInfo[] sortedFiles)
    {
        foreach (var file in sortedFiles)
        {
            try
            {
                string json = File.ReadAllText(file.FullName);
                Counter counter = JsonSerializer.Deserialize<Counter>(json);

                if (counter != null)
                {
                    CounterCollection.Add(counter);
                }
            }
            catch (JsonException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

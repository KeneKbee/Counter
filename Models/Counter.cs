using System.Text.Json;
using System.Text.Json.Serialization;

namespace Counter.Models;

class Counter
{
    [JsonIgnore]
    public string Filename { get; init; }

    public int Id { get; init; }
    public string Name { get; init; }
    public long CountOrigin { get; init; }
    public long Count { get; private set; }
    public string CounterColorHex { get; private set; }

    [JsonIgnore]
    public Color CounterColor { get; private set; }

    [JsonIgnore]
    public string IncrementButtonText => "+";
    [JsonIgnore]
    public string DecrementButtonText => "-";
    [JsonIgnore]
    public string ResetButtonText => "C";
    [JsonIgnore]
    public string DeleteButtonText => "DELETE";

    public Counter(int id, string name, long countOrigin, long count, Color color)
    {
        Filename = $"{name}_{id}.json";

        Id = id;
        Name = name;
        CountOrigin = countOrigin;
        Count = count;
        CounterColor = color;
        CounterColorHex = color.ToHex();
    }

    [JsonConstructor]
    public Counter(int id, string name, long countOrigin, long count, string counterColorHex)
    {
        Filename = $"{name}_{id}.json";

        Id = id;
        Name = name;
        CountOrigin = countOrigin;
        Count = count;
        CounterColor = Color.FromHex(counterColorHex);
        CounterColorHex = counterColorHex;
    }

    public void Increment()
    {
        Count++;
        Save();
    }

    public void Decrement()
    {
        Count--;
        Save();
    }

    public void SetColorRgb(int red, int green, int blue)
    {
        CounterColor = Color.FromRgb(red, green, blue);
        CounterColorHex = CounterColor.ToHex();
        Save();
    }

    public void Reset()
    {
        Count = CountOrigin;
        Save();
    }

    private void Save()
    {
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(this, options);
        
        string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, Filename);
        File.WriteAllText(filePath, jsonString);
    }

    public void Delete()
    {
        File.Delete(Path.Combine(FileSystem.Current.AppDataDirectory, Filename));
    }

    public static void CreateFile(int id, string name, long countOrigin, Color color)
    {
        Counter counter = new(id, name, countOrigin, countOrigin, color);
        counter.Save();
    }

    public static Counter LoadFromFilename(string filename)
    {
        string json = File.ReadAllText(Path.Combine(FileSystem.Current.AppDataDirectory, filename));
        return JsonSerializer.Deserialize<Counter>(json);
    }
}

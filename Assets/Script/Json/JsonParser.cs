
using System.IO;
using Newtonsoft.Json;
public class JsonParser
{
    public void SaveJson<T>(T saveData, string path)
    {
        string jsonData = ObjectToJson(saveData);

        File.WriteAllText(path, jsonData);
    }
    public T LoadJson<T>(string path)
    {
        string loadJson = File.ReadAllText(path);
        T data = JsonToOject<T>(loadJson);
        return data;

    }
    public string ObjectToJson(object obj) { return JsonConvert.SerializeObject(obj, Formatting.Indented); }
    public T JsonToOject<T>(string jsonData) { return JsonConvert.DeserializeObject<T>(jsonData); }
}

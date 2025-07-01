using System.IO;
using UnityEngine;

public class FileLogsManager: MonoBehaviour
{
    // SINGLETON
    public static FileLogsManager Instance;

    // ATTRIBUTES
    private string _basePath = Application.dataPath + "/Logs";
    private string _path;

    // PROPERTIES
    public string Path { get { return _path; } private set { } }

    // METHODS
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CreateFile(string id)
    {
        _path = _basePath + "/" + id + ".txt";
        if (!File.Exists(_path))
        {
            File.WriteAllText(_path, "Logging " + id + "\n" + System.DateTime.Now + "\n\n");
        }
    }

    public void LogToFile(string text)
    {
        File.AppendAllText(_path, text);
    }
}

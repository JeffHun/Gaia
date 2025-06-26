using System.IO;
using UnityEngine;

public class FileLogsManager: MonoBehaviour
{
    // SINGLETON
    public static FileLogsManager Instance;

    // ATTRIBUTES
    private string path;

    // PROPERTIES
    public string Path { get { return path; } private set { } }

    // METHODS
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        path = Application.dataPath + "/Logs/" + System.DateTime.Now.ToString("yy-MM-dd_h-m") + ".txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Logging " + System.DateTime.Now + "\n\n");
        }
    }

    public void LogToFile(string text)
    {
        File.AppendAllText(path, text);
    }
}

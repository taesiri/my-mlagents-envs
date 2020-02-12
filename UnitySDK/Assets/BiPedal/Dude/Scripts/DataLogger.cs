using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    public static DataLogger instance;
    private List<string> _logData = new List<string>();

    private void Awake()
    {
        if (instance != null) return;

        instance = this;
        InvokeRepeating(nameof(SaveToDisk), 5, 10);
    }

    public void DoLogging(string data)
    {
        _logData.Add(data);
    }

    private void SaveToDisk()
    {
        using (TextWriter tw = new StreamWriter("random-positions.txt", append: true))
        {
            foreach (var s in _logData)
                tw.WriteLine(s);
        }

        _logData = new List<string>();
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PosLog : MonoBehaviour
{
    public List<string> filePaths = new List<string>();
    public List<Transform> transformListToLog = new List<Transform>();
    public List<StreamWriter> StreamWriters;
    public void Start()
    {
        StreamWriters = new List<StreamWriter>(2);

        for (int i = 0; i < filePaths.Count; i++)
        {
            StreamWriters.Add(new StreamWriter(filePaths[i], true));
        }
    }

    public void Update()
    {
        for (int i = 0; i < filePaths.Count; i++)
        {
            StreamWriters[i].WriteLine(Vector3ToStr(transformListToLog[i].position));
        }
    }

    public void OnDisable()
    {
        for (int i = 0; i < filePaths.Count; i++)
        {
            StreamWriters[i].Close();
        }
    }

    public static string Vector3ToStr(Vector3 inputVector)
    {
        return string.Format("{0}, {1}, {2}", inputVector.x, inputVector.y, inputVector.z);
    }
}

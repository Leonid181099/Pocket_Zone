using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Storage
{
    public string filePath;
    private BinaryFormatter formatter;

    public Storage()
    {
        var directory = Application.persistentDataPath + "/saves";
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        filePath = directory+"/GameSave.save";
        Debug.Log(filePath);
        InitBinaryFormatter();
    }
    private void InitBinaryFormatter()
    {
        formatter = new BinaryFormatter();
        //var selector = new SurrogateSelector();
    }
    public object Load(object saveDataByDefault)
    {
        if (!File.Exists(filePath))
        {
            if (saveDataByDefault != null)
            {
                Save(saveDataByDefault);
            }
            return saveDataByDefault;
        }
        var file=File.Open(filePath, FileMode.Open);
        var savedData=formatter.Deserialize(file);
        file.Close();
        return savedData;
    }
    public void Save(object saveData)
    {
        var file = File.Create(filePath);
        formatter.Serialize(file, saveData);
        file.Close();
    }
}

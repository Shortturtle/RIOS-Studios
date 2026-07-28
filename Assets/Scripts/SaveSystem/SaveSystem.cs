using UnityEngine;
using System.IO;

//save system Main Part
public class SaveSystem
{
    private static SaveData _saveData = new SaveData();

    [System.Serializable]

    public struct SaveData
    {
        public QuestSaveData QuestData;
        public TowerSaveData TowerData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

    private static void HandleSaveData()
    {
        QuestManager.instance.Save(ref _saveData.QuestData);
        TowerRewardManager.instance.Save(ref _saveData.TowerData);
    }


    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());

        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        QuestManager.instance.Load(_saveData.QuestData);
        TowerRewardManager.instance.Load(_saveData.TowerData);
    }
}

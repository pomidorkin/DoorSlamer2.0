using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Instance
    // Singleton
    private static SaveManager instance;
    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned Save Manager",
                        typeof(SaveManager)).GetComponent<SaveManager>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public Inventory State { get => state; set => state = value; }

    #endregion
    private static SaveManager _instance = null;

    [Header("Logic")]
    [SerializeField] private string saveFileName;
    [SerializeField] private bool loadOnStart = true;
    private Inventory state;
    private BinaryFormatter formatter;

    private void Awake()
    {
        saveFileName = Application.persistentDataPath + "/data.ss";
        formatter = new BinaryFormatter();
        DontDestroyOnLoad(this.gameObject);

        if (loadOnStart)
        {
            Load();
        }

        Debug.Log(Application.persistentDataPath + "/data.ss");
    }

   

public void Save()
    {
        if (State == null)
        {
            State = new Inventory();
        }

        // Open a physical file on your disk to hold te save
        var file = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
        formatter.Serialize(file, State);
        file.Close();
    }

    public void Load()
    {

        try
        {
            // Open a physical file on your disk to hold te save
            var file = new FileStream(saveFileName, FileMode.Open, FileAccess.Read);
            // If we found the file, open and read it
            State = (Inventory)formatter.Deserialize(file);
            file.Close();
        }
        catch
        {

            Debug.Log("No save fie found, creating a new entry...");
            // Function Save(); saves the inventory or creates a new one
            Save();
        }
    }
}

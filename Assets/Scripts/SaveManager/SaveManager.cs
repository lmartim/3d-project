using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public float coins;
    public float health;
    public Vector3 lastPosition;
}

public class SaveManager : Singleton<SaveManager>
{
    private SaveSetup _saveSetup;

    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;

    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup
    {
        get {  return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Invoke(nameof(LoadFile), 1f);
    }

    public void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Lucas";
        _saveSetup.lastPosition = Vector3.zero;

        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;

        SaveItems();
    }

    public void SaveItems()
    {
        _saveSetup.coins = (int)Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.health = (int)Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;

        SavePosition();
    }

    public void SavePosition()
    {
        _saveSetup.lastPosition = PlayerController.Instance.transform.position;

        Save();
    }

    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);

        SaveFile(setupToJson);
    }

    private void SaveFile(string json)
    {
        File.WriteAllText(_path, json);
    }

    private void LoadFile()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
        
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
        
            lastLevel = _saveSetup.lastLevel;
        } 
        else
        {
            CreateNewSave();
        }

        FileLoaded.Invoke(_saveSetup);
    }
}

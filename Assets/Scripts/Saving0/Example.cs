//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Example : MonoBehaviour
//{
//    private Storage storage;
//    private GameData gameData;
//    public GameObject Inventory;

//    private void Start()
//    {
//        storage = new Storage();
//        Load();
//    }
//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.S))
//            Save();
//        if (Input.GetKeyDown(KeyCode.L))
//            Load();
//    }
//    private void Save()
//    {
//        gameData.Grid = Inventory;
//        storage.Save(gameData);
//        Debug.Log("Game saved");
//    }
//    private void Load()
//    {
//        gameData = (GameData)storage.Load(new GameData());
//        Inventory = gameData.Grid;
//    }
//}

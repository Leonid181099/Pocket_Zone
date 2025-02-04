using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float monsterRMin;
    [SerializeField] private float monsterRMax;
    [SerializeField] private float playerRMax;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private int monsterNum;
    private GameObject player;
    void Start()
    {        
        StartCoroutine(LoadMainScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject SpawnPlayer(GameObject playerPrefab, float rmax)
    {
        float angle = UnityEngine.Random.Range(0, 2 * (float)Math.PI);
        float dist = UnityEngine.Random.Range(0, rmax);
        Vector2 playerPos = new Vector2(dist * (float)Math.Cos(angle), dist * (float)Math.Sin(angle));
        player = Instantiate(playerPrefab, playerPos, Quaternion.identity);
        player.SetActive(false);
        return player;
    }

    GameObject[] SpawnMonsters(int monsterNum, Vector2 coord, float rmin, float rmax,GameObject player)
    {
        GameObject[] monsters=new GameObject[monsterNum];
        for (int i = 0; i < monsterNum; i++)
        {
            float angle = UnityEngine.Random.Range(0f, 2 * (float)Math.PI);
            float dist = UnityEngine.Random.Range(rmin, rmax);
            Vector2 monsterPos = new Vector2(dist * (float)Math.Cos(angle), dist * (float)Math.Sin(angle))+coord;
            GameObject monsterType = monsterPrefabs[UnityEngine.Random.Range(0, monsterPrefabs.Length)];
            
            monsters[i]=SpawnMonster(monsterType, monsterPos,player);
        }
        return monsters;
    }
    GameObject SpawnMonster(GameObject monsterType, Vector2 coord,GameObject player)
    {
        GameObject monster=Instantiate(monsterType, coord, Quaternion.identity);
        monster.SetActive(false);
        return monster;
    }

    IEnumerator LoadMainScene()
    {
        string m_Scene = "Main";
        // Set the current Scene to be able to unload it later
        GameObject[] monsters = new GameObject[monsterNum];
        Scene currentScene = SceneManager.GetActiveScene();
        player=SpawnPlayer(playerPrefab, playerRMax);
        //yield return new WaitForSeconds(10);
        monsters =SpawnMonsters(monsterNum, player.transform.position, monsterRMin, monsterRMax,player);
        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_Scene, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(m_Scene));
        player.SetActive(true);
        yield return new WaitForSeconds(5);
        for (int i = 0; i < monsterNum; i++)
        {
            SceneManager.MoveGameObjectToScene(monsters[i], SceneManager.GetSceneByName(m_Scene));
            monsters[i].SetActive(true);
        }
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}

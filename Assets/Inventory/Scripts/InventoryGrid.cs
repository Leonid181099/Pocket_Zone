using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class InventorySlotData
{
    public string itemId;
    public int amount;
    public string picPath; // Путь к изображению (если необходимо)

    public InventorySlotData(InventorySlot slot)
    {
        itemId = slot.itemId;
        amount = slot.amount;
        picPath = slot.slotSprite != null ? slot.slotSprite.name : ""; // или путь, если используется другой подход для хранения изображений
    }
}

[System.Serializable]
public class InventoryGridData
{
    public List<InventorySlotData> slotDataList; // Одномерный список

    // Конструктор для создания данных из Grid
    public InventoryGridData(GameObject[,] grid, int X, int Y)
    {
        slotDataList = new List<InventorySlotData>();
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                InventorySlotData slotData = new InventorySlotData(grid[i, j].GetComponent<InventorySlot>());
                slotDataList.Add(slotData); // Добавляем слот в список
            }
        }
    }
}

public class InventoryGrid : MonoBehaviour
{
    private GameObject[,] Grid;
    public int X;
    public int Y;
    public int Width;
    public int Height;
    public int Xgap;
    public int Ygap;
    public GameObject SquarePrefab;
    public GameObject SquareSlotPrefab;
    public GameObject ButtonPrefab;
    public GameObject Button1Prefab;
    private GameObject Inventory;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = Instantiate(SquarePrefab, transform.Find("Canvas"));
        //SpriteRenderer spriteRenderer = Inventory.transform.GetComponent<SpriteRenderer>();
        //spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        //spriteRenderer.size = new Vector2(300, 200);
        //Inventory.transform.localScale = new Vector3(300, 200, 1);
        RectTransform RT = Inventory.transform.GetComponent<RectTransform>();
        int width = X * Width + (X + 1) * Xgap;
        int height = Y * Height + (Y + 1) * Ygap;
        RT.sizeDelta = new Vector2(width, height);
        Grid = new GameObject[X, Y];
        int Xcoord;
        int Ycoord;
        GameObject Button;
        GameObject Button1;
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                Xcoord = i * Width + (i + 1) * Xgap;
                Ycoord = j * Height + (j + 1) * Ygap;
                Grid[i, j]= Instantiate(SquareSlotPrefab, Inventory.transform);
                RectTransform GridRT = Grid[i, j].transform.GetComponent<RectTransform>();
                GridRT.sizeDelta = new Vector2(Width, Height);
                Grid[i, j].transform.localPosition += new Vector3(Xcoord, -Ycoord,0);
                Grid[i, j].transform.GetComponent<Image>().preserveAspect = true;
                Button=Instantiate(ButtonPrefab, Grid[i, j].transform);
                Button.SetActive(false);
                RectTransform ButtonRT = Button.transform.GetComponent<RectTransform>();
                ButtonRT.sizeDelta= new Vector2(Width, Height);
                Button1 =Instantiate(Button1Prefab, Button.transform);
                Button1.SetActive(false);
                RectTransform Button1RT = Button1.transform.GetComponent<RectTransform>();
                Button1RT.sizeDelta = new Vector2(Width, Height);
                Button1.transform.localPosition += new Vector3(Width, 0, 0);
            }
        }
        LoadGrid();
    }

    // Метод для сериализации и сохранения данных
    public void SaveGrid()
    {
        string path = Path.Combine(Application.persistentDataPath, "inventoryGrid.json");
        InventoryGridData gridData = new InventoryGridData(Grid, X, Y); // Создаём данные для инвентаря
        string json = JsonUtility.ToJson(gridData, true); // Сериализация в JSON
        File.WriteAllText(path, json); // Сохраняем в файл
        Debug.Log($"Данные инвентаря сохранены по пути: {path}");
    }

    // Метод для загрузки данных
    public void LoadGrid()
    {
        string path = Path.Combine(Application.persistentDataPath, "inventoryGrid.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InventoryGridData gridData = JsonUtility.FromJson<InventoryGridData>(json);

            if (gridData != null && gridData.slotDataList != null)
            {
                int index = 0;
                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        if (index < gridData.slotDataList.Count)
                        {
                            InventorySlotData data = gridData.slotDataList[index];
                            InventorySlot slot = Grid[i, j].GetComponent<InventorySlot>();

                            if (slot != null)
                            {
                                Debug.Log($"Загружены данные: itemId = {data.itemId}, amount = {data.amount}");
                                slot.itemId = data.itemId;
                                slot.amount = data.amount;
                                // Если вам нужно восстановить изображение, используйте данные о пути к спрайту (или замените свой подход)
                                if (!string.IsNullOrEmpty(data.picPath))
                                {
                                    // Восстановление изображения
                                    // Пример: загрузить спрайт по имени или пути
                                    Sprite sprite = Resources.Load<Sprite>("Images/"+data.picPath);
                                    if (sprite != null)
                                    {
                                        slot.slotSprite = sprite;
                                    }
                                }
                                slot.update(SquareSlotPrefab);
                            }
                            else
                            {
                                Debug.LogWarning($"Компонент InventorySlot не найден на позиции ({i}, {j})");
                            }
                        }
                        index++;
                    }
                }
            }
            else
            {
                Debug.LogWarning("gridData или slotDataList равны null.");
            }
        }
        else
        {
            Debug.LogWarning($"Файл не найден: {path}");
        }
    }


    // Update is called once per frame
    void Update()
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Loot");
        for (int i = 0;i< gameObjectArray.Length; i++)
        {
            put(gameObjectArray[i]);
        }
        SaveGrid();
    }
    void put(GameObject Item)
    {
        bool flag=false;
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                InventorySlot comp = Grid[i, j].GetComponent(typeof(InventorySlot)) as InventorySlot;
                if (comp.put(Item))
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
                break;
        }
    }
    public void delete(GameObject GridIJ)
    {
        InventorySlot comp = GridIJ.GetComponent(typeof(InventorySlot)) as InventorySlot;
        comp.delete(SquareSlotPrefab);  
    }
}

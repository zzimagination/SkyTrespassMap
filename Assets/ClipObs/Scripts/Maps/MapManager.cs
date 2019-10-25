using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
public class MapManager : SerializedMonoBehaviour
{
    public MapSetting setting;
    public Transform mapContent;
    private void Awake()
    {
        MapsBase.InitMapSetting(setting);
    }
    // Start is called before the first frame update
    void Start()
    {
        mapContent = new GameObject("Maps").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        int width = Screen.width;
        int height = Screen.height;

        Rect buttonRect = new Rect(width / 2 - 50, height / 2 - 20, 100, 40);

        if (GUI.Button(buttonRect,"生成地图"))
        {
            MapsBase.GenerateMapStart();
            CreateAllMaps();
        }
        buttonRect.y -= 50;
        if (GUI.Button(buttonRect, "读取地图"))
        {
            MapsBase.LoadMapStart();
            CreateAllMaps();
        }
    }

    public void CreateAllMaps()
    {
        GenerateMapsRecursion(MapsBase.ZeroBlock);
    }
    


    void GenerateMapsRecursion(MapBlock block)
    {
        if (block.currentObject)
            return;

        GameObject go = Instantiate(setting.MapBlock[(int)block.size - 1],mapContent);
        go.transform.position = block.localPosition;
        block.currentObject = go;
        if (block.Up != null)
        {
            GenerateMapsRecursion(block.Up);
        }

        if (block.Right != null)
        {
            GenerateMapsRecursion(block.Right);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
public class MapManager : SerializedMonoBehaviour
{
    public MapSetting setting;

    public Dictionary<int, MapSerializationUtility.MapUnit> test;

    private void Awake()
    {
        MapsBase.InitMapSetting(setting);
    }
    // Start is called before the first frame update
    void Start()
    {

        MapsBase.GenerateMapStart();
        MapSerializationUtility.Serialization(MapsBase.ZeroBlock);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class MapData
{
    public int a = 100;
    public string t = "heool";
    public Dictionary<int, MapSerializationUtility.MapUnit> test;
}
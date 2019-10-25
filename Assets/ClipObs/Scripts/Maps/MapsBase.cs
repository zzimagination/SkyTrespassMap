using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.Serialization;

public class MapsBase
{

    public static MapBlock ZeroBlock;
    public static MapsBase world;
    public static string saveDataPath;
    static int ColumNumber = 1;
    static int RowNumber = 1;
    static MapSetting _Setting;
    public static void InitMapSetting(MapSetting setting)
    {
        _Setting = setting;
        RowNumber = setting.RowNumber;
        ColumNumber = setting.ColumNumber;
        saveDataPath = Application.streamingAssetsPath + "/maps";
    }

    public static void GenerateMapStart()
    {
        MapBlock[,] MapArray = new MapBlock[RowNumber, ColumNumber];
        for (int i = 0; i < RowNumber; i++)
        {
            for (int j = 0; j < ColumNumber; j++)
            {
                MapArray[i, j] = new MapBlock();

            }
        }
        for (int i = 0; i < RowNumber; i++)
        {
            for (int j = 0; j < ColumNumber; j++)
            {
                if (i - 1 >= 0)
                {
                    MapArray[i, j].Left = MapArray[i - 1, j];
                }
                if (i + 1 < RowNumber)
                {
                    MapArray[i, j].Right = MapArray[i + 1, j];
                }
                if (j - 1 >= 0)
                {
                    MapArray[i, j].Down = MapArray[i, j - 1];
                }
                if (j + 1 < ColumNumber)
                {
                    MapArray[i, j].Up = MapArray[i, j + 1];
                }

            }
        }
        ZeroBlock = MapArray[0, 0];

        SetBlock(ZeroBlock);
        SetMapPosition(ZeroBlock);
        CreatPrefab(ZeroBlock);
    }
    static void SetBlock(MapBlock block)
    {
        if (block.size != MapBlockSize.Type.none)
            return;
        UnityEngine.Random.InitState(block.GetHashCode());
        if ((block.Left != null && block.Left.size == MapBlockSize.Type.big) ||
            (block.Down != null && block.Down.size == MapBlockSize.Type.big) ||
            (block.Up != null && block.Up.size == MapBlockSize.Type.big) ||
            (block.Right != null && block.Right.size == MapBlockSize.Type.big))
        {
            int t = UnityEngine.Random.Range(0, 100);
            block.size = t < 33 ? MapBlockSize.Type.normal : MapBlockSize.Type.small;
        }
        else
        {
            int t = UnityEngine.Random.Range(0, 100);
            if (t < 17)
            {
                block.size = MapBlockSize.Type.big;
            }
            else if (t < 50)
            {
                block.size = MapBlockSize.Type.small;
            }
            else
            {
                block.size = MapBlockSize.Type.normal;
            }
        }
        if (block.Up != null)
        {
            SetBlock(block.Up);
        }
        if (block.Right != null)
        {
            SetBlock(block.Right);
        }
    }
    static void SetMapPosition(MapBlock block)
    {
        float _x = 0;
        float _z = 0;

        block.localPosition = new Vector3(_x, 0, _z);
        Queue<MapBlock> mapBlocks = new Queue<MapBlock>();
        mapBlocks.Enqueue(block);

        while (mapBlocks.Count > 0)
        {
            block = mapBlocks.Dequeue();
            if (block.isTranslated)
                continue;
            if (block.Left != null)
            {
                _x = block.Left.localPosition.x + _Setting.gridSpacing;
            }
            else
            {
                _x = 0;
            }
            if (block.Down != null)
            {
                _z = block.Down.localPosition.z + _Setting.gridSpacing;
            }
            else
            {
                _z = 0;
            }
            block.localPosition = new Vector3(_x, 0, _z);
            block.isTranslated = true;
            if (block.Up != null)
                mapBlocks.Enqueue(block.Up);
            if (block.Right != null)
                mapBlocks.Enqueue(block.Right);
        }
    }
    static void CreatPrefab(MapBlock block)
    {
        if (block.currentObject)
            return;

        GameObject go = GameObject.Instantiate(_Setting.MapBlock[(int)block.size - 1]);
        go.transform.position = block.localPosition;
        block.currentObject = go;
        if (block.Up != null)
        {
            CreatPrefab(block.Up);
        }

        if (block.Right != null)
        {
            CreatPrefab(block.Right);
        }
    }
}


public class MapBlock
{
    public MapBlockSize.Type size;
    public Vector3 localPosition;
    public int key;
    public MapBlock Up;
    public MapBlock Down;
    public MapBlock Left;
    public MapBlock Right;

    public GameObject currentObject;
    public bool isTranslated;


}

public class MapBlockSize
{
    static Vector2 normal = new Vector2(20, 20);
    static Vector2 big = new Vector2(30, 30);
    static Vector2 small = new Vector2(10, 10);
    public enum Type
    {
        none,
        normal,
        small,
        big,
    }


    public static Vector2 GetSize(Type type)
    {

        switch (type)
        {
            case Type.normal:
                return normal;
            case Type.big:
                return big;
            case Type.small:
                return small;
            default:
                return normal;
        }
    }
}


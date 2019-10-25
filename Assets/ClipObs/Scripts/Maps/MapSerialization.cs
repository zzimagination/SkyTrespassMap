using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using UnityEngine;
using Sirenix.Serialization;

public class MapSerializationUtility
{
    static MapsData mapsData;
    static int codeTemp;
    public static void Serialization(MapBlock zero)
    {
        mapsData = new MapsData();
        mapsData.mapsDic = new Dictionary<int, MapUnit>();
        mapsData.idCode = 10000;
        codeTemp = 10000;
        SetMapKey(zero);
        SetMapReference(zero);
        byte[] buffer = SerializationUtility.SerializeValue<MapsData>(mapsData, DataFormat.Binary);
        using (FileStream fs = new FileStream(MapsBase.saveDataPath, FileMode.Create))
        {
            fs.Write(buffer, 0, buffer.Length);
        }

    }

    public static MapBlock Deserialization()
    {

        using (FileStream fs = new FileStream(MapsBase.saveDataPath, FileMode.OpenOrCreate))
        {
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            mapsData = SerializationUtility.DeserializeValue<MapsData>(buffer, DataFormat.Binary);

        }

        Dictionary<int, MapBlock> blocks = new Dictionary<int, MapBlock>();

        foreach (int key in mapsData.mapsDic.Keys)
        {
            MapUnit unit = mapsData.mapsDic[key];
            MapBlock mapBlock = new MapBlock();
            mapBlock.key = key;
            mapBlock.size = unit.size;
            mapBlock.localPosition = unit.localPosition;
            blocks[key] = mapBlock;
        }
        foreach (int key in mapsData.mapsDic.Keys)
        {
            MapUnit unit = mapsData.mapsDic[key];
            if(unit.Up>0)
            {
                blocks[key].Up = blocks[unit.Up];
            }
            if(unit.Down>0)
            {
                blocks[key].Down = blocks[unit.Down];
            }
            if(unit.Left>0)
            {
                blocks[key].Left = blocks[unit.Left];
            }
            if(unit.Right>0)
            {
                blocks[key].Right=blocks[unit.Right];
            }
        }
        return blocks[mapsData.idCode];
    }

    static void SetMapKey(MapBlock block)
    {
        if (block.key != 0)
            return;
        MapUnit unit = new MapUnit();
        unit.key = block.key = codeTemp;
        codeTemp++;
        unit.size = block.size;
        unit.localPosition = block.localPosition;
        mapsData.mapsDic.Add(unit.key, unit);
        if (block.Up != null)
        {
            SetMapKey(block.Up);
        }
        if (block.Right != null)
        {
            SetMapKey(block.Right);
        }
    }

    static void SetMapReference(MapBlock block)
    {
        MapUnit unit = mapsData.mapsDic[block.key];
        if (unit.referenced)
            return;
        if (block.Up != null && unit.Up == 0)
        {
            unit.Up = block.Up.key;
        }

        if (block.Down != null && unit.Down == 0)
        {
            unit.Down = block.Down.key;
        }

        if (block.Left != null && unit.Left == 0)
        {
            unit.Left = block.Left.key;
        }

        if (block.Right != null && unit.Right == 0)
        {
            unit.Right = block.Right.key;
        }
        unit.referenced = true;

        if (block.Up != null)
        {
            SetMapReference(block.Up);
        }
        if (block.Right != null)
        {
            SetMapReference(block.Right);
        }

    }

    [Serializable]
    public class MapUnit
    {
        public int key;
        public MapBlockSize.Type size;
        public Vector3 localPosition;

        public int Up;
        public int Down;
        public int Left;
        public int Right;

        public bool referenced;

    }
    [Serializable]
    public class MapsData
    {
        public Dictionary<int, MapUnit> mapsDic = new Dictionary<int, MapUnit>();
        public int idCode;
    }
}
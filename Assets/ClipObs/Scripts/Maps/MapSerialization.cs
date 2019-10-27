using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using UnityEngine;
using Sirenix.Serialization;
using System.Threading.Tasks;
using System.Threading;

namespace SkyTrepass.Map.Serialization
{
    public class MapSerializationUtility
    {
        static MapsData mapsData;
        static int codeTemp;
        public static void Serialization()
        {
            if (mapsData == null)
                mapsData = new MapsData();
            Dictionary<int, MapBlock> mapsDic = MapsBase.MapsDic;
            mapsData.mapsDic = new Dictionary<int, MapUnit>();
            foreach (int key in mapsDic.Keys)
            {
                MapUnit unit = new MapUnit();
                unit.key = mapsDic[key].roofCode;
                unit.size = mapsDic[key].size;
                unit.localPosition = mapsDic[key].localPosition;
                unit.buildings = mapsDic[key].buildings;
                if (mapsDic[key].Up != null)
                {
                    unit.Up = mapsDic[key].Up.roofCode;
                }
                if (mapsDic[key].Down != null)
                {
                    unit.Down = mapsDic[key].Down.roofCode;
                }
                if (mapsDic[key].Left != null)
                {
                    unit.Left = mapsDic[key].Left.roofCode;
                }
                if (mapsDic[key].Right != null)
                {
                    unit.Right = mapsDic[key].Right.roofCode;
                }


                mapsData.mapsDic.Add(key, unit);
            }

            List<Bridge> bridges = BridgeBase.bridges;
            mapsData.bridges = new List<BridgeUnit>();
            for (int i = 0; i < bridges.Count; i++)
            {
                BridgeUnit unit = new BridgeUnit();
                unit.length = bridges[i].length;
                unit.position = bridges[i].position;
                unit.rotation = bridges[i].rotation;
                mapsData.bridges.Add(unit);
            }

            byte[] buffer = SerializationUtility.SerializeValue<MapsData>(mapsData, DataFormat.Binary);

            using (FileStream fs = new FileStream(MapsBase.saveDataPath, FileMode.Create))
            {
                fs.Write(buffer, 0, buffer.Length);
            }

        }

        public static void Deserialization()
        {

            using (FileStream fs = new FileStream(MapsBase.saveDataPath, FileMode.OpenOrCreate))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                mapsData = SerializationUtility.DeserializeValue<MapsData>(buffer, DataFormat.Binary);

            }
            if (mapsData.mapsDic != null)
            {
                Dictionary<int, MapBlock> blocks = new Dictionary<int, MapBlock>();

                foreach (int key in mapsData.mapsDic.Keys)
                {
                    MapUnit unit = mapsData.mapsDic[key];
                    MapBlock mapBlock = new MapBlock();
                    mapBlock.roofCode = key;
                    mapBlock.size = unit.size;
                    mapBlock.localPosition = unit.localPosition;
                    mapBlock.buildings = unit.buildings;
                    blocks[key] = mapBlock;
                }
                foreach (int key in mapsData.mapsDic.Keys)
                {
                    MapUnit unit = mapsData.mapsDic[key];
                    if (unit.Up > 0)
                    {
                        blocks[key].Up = blocks[unit.Up];
                    }
                    if (unit.Down > 0)
                    {
                        blocks[key].Down = blocks[unit.Down];
                    }
                    if (unit.Left > 0)
                    {
                        blocks[key].Left = blocks[unit.Left];
                    }
                    if (unit.Right > 0)
                    {
                        blocks[key].Right = blocks[unit.Right];
                    }


                }

                MapsBase.MapsDic = blocks;
                MapsBase.ZeroBlock = blocks[MapsBase.mapStartCode];
            }
            if (mapsData.bridges != null)
            {
                List<Bridge> bridges = new List<Bridge>();

                for (int i = 0; i < mapsData.bridges.Count; i++)
                {
                    Bridge bridge = new Bridge();
                    bridge.length = mapsData.bridges[i].length;
                    bridge.position = mapsData.bridges[i].position;
                    bridge.rotation = mapsData.bridges[i].rotation;
                    bridges.Add(bridge);
                }
                BridgeBase.bridges = bridges;
            }

        }

        public static SerializationAsyncResult DeserializationAsync()
        {
            SerializationAsyncResult result = new SerializationAsyncResult();
            Task.Run(() =>
            {
                result.complete = false;
                using (FileStream fs = new FileStream(MapsBase.saveDataPath, FileMode.OpenOrCreate))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    mapsData = SerializationUtility.DeserializeValue<MapsData>(buffer, DataFormat.Binary);
                }
                if (mapsData.mapsDic != null)
                {
                    Dictionary<int, MapBlock> blocks = new Dictionary<int, MapBlock>();
                    List<MapBlock> small = new List<MapBlock>();
                    List<MapBlock> normal = new List<MapBlock>();
                    List<MapBlock> big = new List<MapBlock>();
                    foreach (int key in mapsData.mapsDic.Keys)
                    {
                        MapUnit unit = mapsData.mapsDic[key];
                        MapBlock mapBlock = new MapBlock();
                        mapBlock.roofCode = key;
                        mapBlock.size = unit.size;
                        mapBlock.localPosition = unit.localPosition;
                        mapBlock.buildings = unit.buildings;
                        blocks[key] = mapBlock;

                        if (mapBlock.size == MapBlockSize.Type.big)
                            big.Add(mapBlock);
                        else if (mapBlock.size == MapBlockSize.Type.small)
                            small.Add(mapBlock);
                        else
                            normal.Add(mapBlock);
                    }
                    MapsBase.smallBlocks = small.ToArray();
                    MapsBase.normalBlocks = normal.ToArray();
                    MapsBase.bigBlocks = big.ToArray();

                    foreach (int key in mapsData.mapsDic.Keys)
                    {
                        MapUnit unit = mapsData.mapsDic[key];
                        if (unit.Up > 0)
                        {
                            blocks[key].Up = blocks[unit.Up];
                        }
                        if (unit.Down > 0)
                        {
                            blocks[key].Down = blocks[unit.Down];
                        }
                        if (unit.Left > 0)
                        {
                            blocks[key].Left = blocks[unit.Left];
                        }
                        if (unit.Right > 0)
                        {
                            blocks[key].Right = blocks[unit.Right];
                        }
                    }

                    MapsBase.MapsDic = blocks;
                    MapsBase.ZeroBlock = blocks[MapsBase.mapStartCode];
                }
                if (mapsData.bridges != null)
                {
                    List<Bridge> bridges = new List<Bridge>();

                    for (int i = 0; i < mapsData.bridges.Count; i++)
                    {
                        Bridge bridge = new Bridge();
                        bridge.length = mapsData.bridges[i].length;
                        bridge.position = mapsData.bridges[i].position;
                        bridge.rotation = mapsData.bridges[i].rotation;
                        bridges.Add(bridge);
                    }
                    BridgeBase.bridges = bridges;
                }
                result.complete = true;
            });
            return result;
        }
        public static void SerializationAsync()
        {

            var t = Task.Run(() =>
            {
                if (mapsData == null)
                    mapsData = new MapsData();
                Dictionary<int, MapBlock> mapsDic = MapsBase.MapsDic;
                mapsData.mapsDic = new Dictionary<int, MapUnit>();
                foreach (int key in mapsDic.Keys)
                {
                    MapUnit unit = new MapUnit();
                    unit.key = mapsDic[key].roofCode;
                    unit.size = mapsDic[key].size;
                    unit.localPosition = mapsDic[key].localPosition;
                    unit.buildings = mapsDic[key].buildings;
                    if (mapsDic[key].Up != null)
                    {
                        unit.Up = mapsDic[key].Up.roofCode;
                    }
                    if (mapsDic[key].Down != null)
                    {
                        unit.Down = mapsDic[key].Down.roofCode;
                    }
                    if (mapsDic[key].Left != null)
                    {
                        unit.Left = mapsDic[key].Left.roofCode;
                    }
                    if (mapsDic[key].Right != null)
                    {
                        unit.Right = mapsDic[key].Right.roofCode;
                    }


                    mapsData.mapsDic.Add(key, unit);
                }

                List<Bridge> bridges = BridgeBase.bridges;
                mapsData.bridges = new List<BridgeUnit>();
                for (int i = 0; i < bridges.Count; i++)
                {
                    BridgeUnit unit = new BridgeUnit();
                    unit.length = bridges[i].length;
                    unit.position = bridges[i].position;
                    unit.rotation = bridges[i].rotation;
                    mapsData.bridges.Add(unit);
                }

                byte[] buffer = SerializationUtility.SerializeValue<MapsData>(mapsData, DataFormat.Binary);

                using (FileStream fs = new FileStream(MapsBase.saveDataPath, FileMode.Create))
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
            });
        }

        //static void SetMapKey(MapBlock block)
        //{
        //    if (block.roofCode != 0)
        //        return;
        //    MapUnit unit = new MapUnit();
        //    unit.key = block.roofCode = codeTemp;
        //    codeTemp++;
        //    unit.size = block.size;
        //    unit.localPosition = block.localPosition;
        //    mapsData.mapsDic.Add(unit.key, unit);
        //    if (block.Up != null)
        //    {
        //        SetMapKey(block.Up);
        //    }
        //    if (block.Right != null)
        //    {
        //        SetMapKey(block.Right);
        //    }
        //}

        //static void SetMapReference(MapBlock block)
        //{
        //    MapUnit unit = mapsData.mapsDic[block.roofCode];
        //    if (unit.referenced)
        //        return;
        //    if (block.Up != null && unit.Up == 0)
        //    {
        //        unit.Up = block.Up.roofCode;
        //    }

        //    if (block.Down != null && unit.Down == 0)
        //    {
        //        unit.Down = block.Down.roofCode;
        //    }

        //    if (block.Left != null && unit.Left == 0)
        //    {
        //        unit.Left = block.Left.roofCode;
        //    }

        //    if (block.Right != null && unit.Right == 0)
        //    {
        //        unit.Right = block.Right.roofCode;
        //    }
        //    unit.referenced = true;

        //    if (block.Up != null)
        //    {
        //        SetMapReference(block.Up);
        //    }
        //    if (block.Right != null)
        //    {
        //        SetMapReference(block.Right);
        //    }

        //}

        [Serializable]
        public class MapUnit
        {
            public int key;
            public MapBlockSize.Type size;
            public Vector3 localPosition;
            public int buildings;
            public int Up;
            public int Down;
            public int Left;
            public int Right;
        }
        [Serializable]
        public class BridgeUnit
        {
            public float length;
            public Vector3 position;
            public Quaternion rotation;
        }
        [Serializable]
        public class MapsData
        {
            public Dictionary<int, MapUnit> mapsDic;
            public int idCode;
            public List<BridgeUnit> bridges;
        }
    }


    public class SerializationAsyncResult
    {
        public bool complete;
        public bool IsCompleted()
        {
            return complete;
        }
    }
}
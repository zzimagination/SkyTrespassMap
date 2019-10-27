using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.Serialization;
namespace SkyTrepass.Map
{
    using Serialization;

    public class MapsBase
    {

        public static MapBlock ZeroBlock;
        public static MapsBase world;
        public static string saveDataPath;
        public static Dictionary<int, MapBlock> MapsDic;
        static int ColumNumber;
        static int RowNumber;

        static MapSetting _Setting;

        const int mapStartCode = 10000;

        public static void InitMapSetting(MapSetting setting)
        {
            _Setting = setting;
            RowNumber = setting.RowNumber;
            ColumNumber = setting.ColumNumber;
            saveDataPath = Application.streamingAssetsPath + "/maps";
        }

        public static void GenerateMapStart()
        {
            MapsDic = new Dictionary<int, MapBlock>();
            MapBlock[,] MapArray = new MapBlock[RowNumber, ColumNumber];

            int roofCode = mapStartCode;
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumNumber; j++)
                {
                    MapArray[i, j] = new MapBlock();
                    MapArray[i, j].roofCode = roofCode;
                    MapsDic.Add(roofCode, MapArray[i, j]);

                    roofCode++;
                }
            }

            ZeroBlock = MapArray[0, 0];

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

            SetBlockSize();

            SetMapPosition(ZeroBlock);
            SetBuildings();
            BridgeBase.GenerateStart(ZeroBlock);

            MapSerializationUtility.Serialization();
        }

        public static void LoadMapStart()
        {
            MapSerializationUtility.Deserialization();
            ZeroBlock = MapsDic[mapStartCode];
        }

        public static MapBlock GetIndexMap(int x, int y)
        {
            if (x >= ColumNumber || y >= RowNumber)
                return null;

            MapBlock result = ZeroBlock;

            int count = 0;
            while (count < x)
            {
                if (result.Right != null)
                    result = result.Right;
                count++;
            }
            count = 0;
            while (count < y)
            {
                if (result.Up != null)
                    result = result.Up;
            }
            return result;
        }

        static void SetBlockSize()
        {
            foreach (int key in MapsDic.Keys)
            {
                UnityEngine.Random.InitState(MapsDic[key].GetHashCode());
                if ((MapsDic[key].Left != null && MapsDic[key].Left.size == MapBlockSize.Type.big) ||
                (MapsDic[key].Down != null && MapsDic[key].Down.size == MapBlockSize.Type.big) ||
                (MapsDic[key].Up != null && MapsDic[key].Up.size == MapBlockSize.Type.big) ||
                (MapsDic[key].Right != null && MapsDic[key].Right.size == MapBlockSize.Type.big))
                {
                    int t = UnityEngine.Random.Range(0, 100);
                    MapsDic[key].size = t < 33 ? MapBlockSize.Type.normal : MapBlockSize.Type.small;
                }
                else
                {
                    int t = UnityEngine.Random.Range(0, 100);
                    if (t < 20)
                    {
                        MapsDic[key].size = MapBlockSize.Type.big;
                    }
                    else if (t < 40)
                    {
                        MapsDic[key].size = MapBlockSize.Type.small;
                    }
                    else
                    {
                        MapsDic[key].size = MapBlockSize.Type.normal;
                    }
                }
            }
        }

        #region 遍历图元法
        //static void SetBlockSize(MapBlock block)
        //{
        //    if (block.size != MapBlockSize.Type.none)
        //        return;
        //    UnityEngine.Random.InitState(block.GetHashCode());
        //    if ((block.Left != null && block.Left.size == MapBlockSize.Type.big) ||
        //        (block.Down != null && block.Down.size == MapBlockSize.Type.big) ||
        //        (block.Up != null && block.Up.size == MapBlockSize.Type.big) ||
        //        (block.Right != null && block.Right.size == MapBlockSize.Type.big))
        //    {
        //        int t = UnityEngine.Random.Range(0, 100);
        //        block.size = t < 33 ? MapBlockSize.Type.normal : MapBlockSize.Type.small;
        //    }
        //    else
        //    {
        //        int t = UnityEngine.Random.Range(0, 100);
        //        if (t < 20)
        //        {
        //            block.size = MapBlockSize.Type.big;
        //        }
        //        else if (t < 40)
        //        {
        //            block.size = MapBlockSize.Type.small;
        //        }
        //        else
        //        {
        //            block.size = MapBlockSize.Type.normal;
        //        }
        //    }
        //    if (block.Up != null)
        //    {
        //        SetBlockSize(block.Up);
        //    }
        //    if (block.Right != null)
        //    {
        //        SetBlockSize(block.Right);
        //    }
        //}
        #endregion

        static void SetMapPosition(MapBlock block)
        {
            float _x = 0;
            float _z = 0;

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
                block.localPosition = new Vector3(_x, 0f, _z);
                block.isTranslated = true;
                if (block.Up != null)
                    mapBlocks.Enqueue(block.Up);
                if (block.Right != null)
                    mapBlocks.Enqueue(block.Right);
            }
        }

        static void SetBuildings()
        {
            foreach (int key in MapsDic.Keys)
            {
                UnityEngine.Random.InitState(MapsDic[key].GetHashCode());
                switch (MapsDic[key].size)
                {
                    case MapBlockSize.Type.none:
                        break;
                    case MapBlockSize.Type.normal:
                        int t = _Setting.normalBuildings.Count;
                        t = UnityEngine.Random.Range(0, t);
                        MapsDic[key].buildings = t;
                        break;
                    case MapBlockSize.Type.small:
                        t = _Setting.smallBuildings.Count;
                        t = UnityEngine.Random.Range(0, t);
                        MapsDic[key].buildings = t;
                        break;
                    case MapBlockSize.Type.big:
                        t = _Setting.bigBuildings.Count;
                        t = UnityEngine.Random.Range(0, t);
                        MapsDic[key].buildings = t;
                        break;
                }

            }
        }
    }


    public class MapBlock
    {
        public MapBlockSize.Type size;
        public Vector3 localPosition;
        public int roofCode;
        public int buildings;
        public MapBlock Up;
        public MapBlock Down;
        public MapBlock Left;
        public MapBlock Right;

        public bool upBridge;
        public bool downBridge;
        public bool leftBridge;
        public bool rightBridge;

        public GameObject currentObject;
        public bool isTranslated;


    }

    public class MapBlockSize
    {
        public static Vector2 normal = new Vector2(20, 20);
        public static Vector2 big = new Vector2(30, 30);
        public static Vector2 small = new Vector2(10, 10);
        public enum Type
        {
            none,
            normal,
            small,
            big,
        }

        public static void Setting(MapSetting setting)
        {

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

}
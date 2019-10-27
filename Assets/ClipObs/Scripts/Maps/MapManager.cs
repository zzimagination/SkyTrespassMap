using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

namespace SkyTrepass.Map
{
    public class MapManager : SerializedMonoBehaviour
    {
        public MapSetting setting;
        Transform mapContent;
        Transform bridgeContent;
        private void Awake()
        {
            MapsBase.InitMapSetting(setting);
        }
        // Start is called before the first frame update
        void Start()
        {
            mapContent = new GameObject("Maps").transform;
            bridgeContent = new GameObject("Bridges").transform;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnGUI()
        {
            int width = Screen.width;
            int height = Screen.height;

            Rect buttonRect = new Rect(0, 0, 100, 40);

            if (GUI.Button(buttonRect, "生成地图"))
            {
                MapsBase.GenerateMapStart();
                //CreateAllMaps();
                //CreateAllBridges();
            }
            buttonRect.y += 50;
            if (GUI.Button(buttonRect, "读取地图"))
            {
                MapsBase.LoadMapStart();
                //CreateAllMaps();
                //CreateAllBridges();
            }
        }
        public void CreateAllBridges()
        {
            for (int i = 0; i < BridgeBase.bridges.Count; i++)
            {
                var b= BridgeBase.bridges[i];
                GameObject bo = Instantiate(setting.bridge, bridgeContent);
                var m= bo.GetComponent<BridgeManager>();
                m.position = b.position;
                m.rotation = b.rotation;
                m.length = b.length;
                m.DefineBridge();
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
            GameObject obj = null;
            GameObject building = null;
            switch (block.size)
            {
                case MapBlockSize.Type.none:
                    return;
                case MapBlockSize.Type.normal:
                    obj = setting.normalBlock;
                    building = setting.normalBuildings[block.buildings];
                    break;
                case MapBlockSize.Type.small:
                    obj = setting.smallBlock;
                    building = setting.smallBuildings[block.buildings];
                    break;
                case MapBlockSize.Type.big:
                    obj = setting.bigBlock;
                    building = setting.bigBuildings[block.buildings];
                    break;
            }
            GameObject go = Instantiate(obj, mapContent);
            go.transform.position = block.localPosition;
            block.currentObject = go;
            GameObject b = Instantiate(building, go.transform);
            b.GetComponent<Renderer>().sortingOrder = 1;
            b.transform.localPosition = new Vector3(0, 0.001f, 0);
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
}
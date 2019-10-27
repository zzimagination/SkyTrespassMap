using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkyTrepass.Map
{

    public class BridgeBase
    {
        public static List<Bridge> bridges;

        static int bridgeCode;

        public static void GenerateStart(MapBlock block)
        {
            bridges = new List<Bridge>();
            Queue<MapBlock> mapBlocks = new Queue<MapBlock>();
            mapBlocks.Enqueue(block);

            while (mapBlocks.Count > 0)
            {
                block = mapBlocks.Dequeue();
                if (!block.upBridge&&block.Up!=null)
                {

                    GenerateOne(block, block.Up);
                    block.upBridge = true;
                    block.Up.downBridge = true;
                    mapBlocks.Enqueue(block.Up);
                }
                if(!block.downBridge&&block.Down!=null)
                {
                    GenerateOne(block, block.Down);
                    block.downBridge = true;
                    block.Down.upBridge = true;
                    mapBlocks.Enqueue(block.Down);
                }
                if(!block.leftBridge&&block.Left!=null)
                {
                    GenerateOne(block, block.Left);
                    block.leftBridge = true;
                    block.Left.rightBridge = true;
                    mapBlocks.Enqueue(block.Left);
                }
                if (!block.rightBridge && block.Right != null)
                {
                    GenerateOne(block, block.Right);
                    block.rightBridge = true;
                    block.Right.leftBridge = true;
                    mapBlocks.Enqueue(block.Right);
                }
            }
        }


        public static void GenerateOne(MapBlock forward, MapBlock back)
        {

            Quaternion quaternion = Quaternion.FromToRotation(new Vector3(0, 0, 1), back.localPosition - forward.localPosition);
            Vector3 bridgePos = new Vector3(0, 0, 0);
            float length = 0;
            if (back.localPosition.z == forward.localPosition.z)
            {
                float forwardL = MapBlockSize.GetSize(forward.size).x / 2;
                float backL = MapBlockSize.GetSize(back.size).x / 2;
                length = Mathf.Abs(back.localPosition.x - forward.localPosition.x);
                length = length - forwardL - backL;

                bridgePos= forward.localPosition + new Vector3(forwardL+length/2, 0, 0);
            }
            else
            {
                float forwardL = MapBlockSize.GetSize(forward.size).y / 2;
                float backL = MapBlockSize.GetSize(back.size).y / 2;
                length = Mathf.Abs(back.localPosition.z - forward.localPosition.z);
                length = length - forwardL - backL;

                bridgePos = forward.localPosition + new Vector3(0, 0, forwardL+length/2);
            }

             

            Bridge bridge = new Bridge();
            bridge.position = bridgePos;
            bridge.rotation = quaternion;
            bridge.length = length;
            bridges.Add(bridge);
        }

    }

    public class Bridge
    {

        public Vector3 position;
        public Quaternion rotation;
        public float length;

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrepass.Map
{
    public class BridgeManager : MonoBehaviour
    {
        public Vector3 position;
        public Quaternion rotation;
        public float length;

        public GameObject headForward;
        public GameObject headBack;
        public GameObject middle;
        public BoxCollider bodyCollider;
        Vector3 localforwardPos;
        Vector3 localbackPos;
        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DefineBridge()
        {
          
            transform.position = position;
            transform.rotation = rotation;

            localforwardPos = new Vector3(0, 0, -length / 2);
            localbackPos = new Vector3(0, 0, length / 2);

            headForward.transform.localPosition = localforwardPos;
            headBack.transform.localPosition = localbackPos;

            float dis = localbackPos.z - localforwardPos.z;


            Vector3 wpos = new Vector3(0, 0, -length / 2 + 0.5f);
            middle.transform.localPosition = wpos;
            while (wpos.z < length / 2 - 0.5f)
            {
                GameObject w = Instantiate(middle, transform);
                wpos = wpos + new Vector3(0, 0, 1);
                w.transform.localPosition = wpos;
            }
            bodyCollider.size = new Vector3(bodyCollider.size.x, bodyCollider.size.y, length);
        }
    }
}
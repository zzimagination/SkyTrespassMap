using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace SkyTrepass.Map
{
    public class CamereObstacleCheck : MonoBehaviour
    {
        public Transform player;
        RaycastHit[] raycastHits;

        List<IHiddenObstacle> hiddens;
        Camera _camera;
        Vector3 dis;
        // Start is called before the first frame update
        void Start()
        {
            hiddens = new List<IHiddenObstacle>();
            _camera = GetComponent<Camera>();
            dis = _camera.transform.position - player.position;
            raycastHits = new RaycastHit[4];
        }

        // Update is called once per frame
        void Update()
        {
            _camera.transform.position = player.position + dis;
            Vector3 dir = player.position - _camera.transform.position;
            int cnumber = Physics.RaycastNonAlloc(_camera.transform.position, dir.normalized, raycastHits);
            for (int i = 0; i < cnumber; i++)
            {
                var t = raycastHits[i].transform.GetComponent<IHiddenObstacle>();
                if (t != null)
                {
                    t.JudgeOn();
                    if (!hiddens.Contains(t))
                        hiddens.Add(t);
                }
            }
        }

        private void LateUpdate()
        {
            for (int i = hiddens.Count - 1; i >= 0; i--)
            {
                if (hiddens[i].CompareIsOn())
                {
                    hiddens.Remove(hiddens[i]);
                }
            }
        }
    }
}
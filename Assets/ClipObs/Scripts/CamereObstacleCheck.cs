using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CamereObstacleCheck : MonoBehaviour
{
    public Transform player;
    RaycastHit[] raycastHits;

    List<HiddenObstacle> currentObs = new List<HiddenObstacle>();
    Camera _camera;
    Vector3 dis;
    // Start is called before the first frame update
    void Start()
    {

        _camera = GetComponent<Camera>();
        dis = _camera.transform.position - player.position;
        raycastHits = new RaycastHit[4];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < raycastHits.Length; i++)
        {
            var tr = raycastHits[i].transform;
            if (tr)
            {
                var t = tr.GetComponent<IHiddenObstacle>();
                t?.CompareIsOn();
            }
        }
        Vector3 dir = player.position - _camera.transform.position;
        Physics.RaycastNonAlloc(_camera.transform.position, dir.normalized, raycastHits);
        for (int i = 0; i < raycastHits.Length; i++)
        {
            var tr = raycastHits[i].transform;
            if (tr)
            {
                var t = tr.GetComponent<IHiddenObstacle>();
                t?.JudgeOn();
            }
        }
    }

    private void LateUpdate()
    {
        _camera.transform.position = player.position + dis;
    }
}

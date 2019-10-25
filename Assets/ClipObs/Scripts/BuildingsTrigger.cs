using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
public class BuildingsTrigger : MonoBehaviour
{
   
    public HouseEvent<TriggerInfo> exit;

    Vector3 targetPos;

    private void OnTriggerEnter(Collider other)
    {
        targetPos = other.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        Vector3 pos1 = transform.forward;
        Vector3 pos2 = other.transform.position - targetPos;
        TriggerInfo info = new TriggerInfo();
        if ( Vector3.Dot(pos1, pos2) >= 0)
        {
            info.isForward = true;
        }else
        {
            info.isForward = false;
        }

        exit?.Invoke(info);

        var move = other.GetComponent<MoveObj>();
        move?.UpdataPath();
    }


    public struct TriggerInfo
    {
        public bool isForward;
    }



    public delegate void HouseEvent<T>(T info);

}


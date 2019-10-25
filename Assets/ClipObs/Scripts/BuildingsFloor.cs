using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsFloor : MonoBehaviour
{
    public BuildingsFloor underFloor;
    public BuildingsFloor onFloor;
    public BuildingsTrigger[] upFloorTriggers;
    public BuildingsTrigger[] downTriggers;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < upFloorTriggers.Length; i++)
        {
            upFloorTriggers[i].exit = UpFloorEvent;
        }
        for (int i = 0; i < downTriggers.Length; i++)
        {
            downTriggers[i].exit = DownFloorEvent;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenFloor()
    {
        //var renders= GetComponentsInChildren<Renderer>();
        //var colliders = GetComponentsInChildren<Collider>();
        //for (int i = 0; i < renders.Length; i++)
        //{
        //    renders[i].enabled = true;
        //}
        //for (int i = 0; i < colliders.Length; i++)
        //{
        //    colliders[i].enabled = true;
        //}
        gameObject.SetActive(true);
    }

    public void CloseFloor()
    {
        //var renders = GetComponentsInChildren<Renderer>();
        //for (int i = 0; i < renders.Length; i++)
        //{
        //    renders[i].enabled = false;
        //}
        //var colliders = GetComponentsInChildren<Collider>();
        //for (int i = 0; i < colliders.Length; i++)
        //{
        //    colliders[i].enabled = false;
        //}
        gameObject.SetActive(false);
    }

    public void StandByFloor()
    {
        gameObject.SetActive(true);
    }

    public void UpFloorEvent(BuildingsTrigger.TriggerInfo info)
    {
        if (!info.isForward)
            onFloor?.OpenFloor();
    }

    public void DownFloorEvent(BuildingsTrigger.TriggerInfo info)
    {
        if(info.isForward)
        {
            CloseFloor();
        }
    }
}

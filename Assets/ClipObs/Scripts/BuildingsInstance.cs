using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class BuildingsInstance : MonoBehaviour
{

    public BuildingsFloor[] floors;
    public BuildingsTrigger DoorTrigger;
    public Light MainLight;
    public EnvironmentConfig indoor;
    public EnvironmentConfig outDoor;
    public BuildingRoof Roof;
    public GameObject bottom;

    //bool isIn;
    // Start is called before the first frame update
    void Start()
    {

        DoorTrigger.exit = BuildingsDoorTrigger;

    }

    // Update is called once per frame
    void Update()
    {

    }


    void StandByBuildings()
    {
        for (int i = 1; i < floors.Length; i++)
        {
            floors[i].StandByFloor();
        }
        Roof.Appear();
    }

    void RunningBuildings()
    {
        for (int i = 1; i < floors.Length; i++)
        {
            floors[i].CloseFloor();
        }
        Roof.Disappear();
    }

    public void BuildingsDoorTrigger(BuildingsTrigger.TriggerInfo info)
    {


        EnvironmentConfig c;
        if (info.isForward)
        {
            c = indoor;
            RunningBuildings();
            //isIn = true;
        }
        else
        {
            c = outDoor;
            StandByBuildings();
            //isIn = false;
        }
        RenderSettings.ambientSkyColor = c.SkyColor;
        RenderSettings.ambientEquatorColor = c.EquatorColor;
        RenderSettings.ambientGroundColor = c.GroundColor;
        MainLight.intensity = c.MainLightIntensity;
        MainLight.shadows = c.mainShadows;

        
    }

    public void DispearBuilding()
    {
        for (int i = 0; i < floors.Length; i++)
        {
            floors[i].CloseFloor();
        }
        bottom.SetActive(true);
    }
    public void ApearBuilding()
    {
        for (int i = 0; i < floors.Length; i++)
        {
            floors[i].StandByFloor();
        }
        bottom.SetActive(false);
    }
}

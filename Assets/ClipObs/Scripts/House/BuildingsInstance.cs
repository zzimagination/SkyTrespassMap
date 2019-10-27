using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SkyTrepass.Map
{

    public class BuildingsInstance : MonoBehaviour
    {

        public BuildingsFloor[] floors;
        public Light MainLight;
        public EnvironmentConfig environmentConfig;

        public BuildingRoof Roof;
        public GameObject bottom;

        bool inBuilding;
        //bool isIn;
        // Start is called before the first frame update
        void Start()
        {
            MainLight = GameObject.FindGameObjectWithTag("MainLight").GetComponent<Light>();
            StandByBuildings();
        }


        void StandByBuildings()
        {
            for (int i = 0; i < floors.Length; i++)
            {
                floors[i].StandByFloor();
            }
            Roof.Appear();
        }

        void RunningBuildings()
        {
            floors[0].ActiveFloor();
            for (int i = 1; i < floors.Length; i++)
            {
                floors[i].DisableFloor();
            }
            Roof.Disappear();
        }

        public void InBuilding()
        {
            if (inBuilding)
                return;

            for (int i = 0; i < environmentConfig.configs.Length; i++)
            {
                if (environmentConfig.configs[i].name.Equals("InDoor"))
                {
                    var c = environmentConfig.configs[i];
                    RenderSettings.ambientSkyColor = c.SkyColor;
                    RenderSettings.ambientEquatorColor = c.EquatorColor;
                    RenderSettings.ambientGroundColor = c.GroundColor;
                    MainLight.intensity = c.MainLightIntensity;
                    MainLight.shadows = c.mainShadows;
                    break;
                }
            }
            RunningBuildings();
            inBuilding = true;
        }
        public void OutBuilding()
        {
            if (inBuilding == false)
                return;

            for (int i = 0; i < environmentConfig.configs.Length; i++)
            {
                if (environmentConfig.configs[i].name.Equals("OutDoor"))
                {
                    var c = environmentConfig.configs[i];
                    RenderSettings.ambientSkyColor = c.SkyColor;
                    RenderSettings.ambientEquatorColor = c.EquatorColor;
                    RenderSettings.ambientGroundColor = c.GroundColor;
                    MainLight.intensity = c.MainLightIntensity;
                    MainLight.shadows = c.mainShadows;
                    break;
                }
            }
            StandByBuildings();
            inBuilding = false;
        }

        public void DispearBuilding()
        {
            for (int i = 0; i < floors.Length; i++)
            {
                floors[i].HiddenRender(false);
            }
            bottom.SetActive(true);
        }
        public void ApearBuilding()
        {
            for (int i = 0; i < floors.Length; i++)
            {
                floors[i].HiddenRender(true);
            }
            bottom.SetActive(false);
        }
    }
}
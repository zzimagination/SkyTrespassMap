using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SkyTrepass
{

    [CreateAssetMenu(fileName = "enviroment", menuName = "Setting/Enviroment", order = 0)]
    public class EnvironmentConfig : ScriptableObject
    {

        public Color SkyColor;
        public Color EquatorColor;
        public Color GroundColor;

        public float MainLightIntensity;
        public LightShadows mainShadows;
    }
}
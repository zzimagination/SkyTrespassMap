using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace SkyTrepass
{

    [CreateAssetMenu(fileName = "enviroment", menuName = "Setting/Enviroment", order = 0)]
    public class EnvironmentConfig : ScriptableObject
    {
        [Title("Light Config")]
        public Config[] configs;
    }
    [System.Serializable]
    public struct Config
    {
        public string name;
        public Color SkyColor;
        public Color EquatorColor;
        public Color GroundColor;

        public float MainLightIntensity;
        public LightShadows mainShadows;
    }
}
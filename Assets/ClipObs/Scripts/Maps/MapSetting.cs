using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SkyTrepass.Map
{

    [CreateAssetMenu(fileName = "MapSetting", menuName = "Setting/MapSetting")]
    public class MapSetting : ScriptableObject
    {
        public int RowNumber = 5;
        public int ColumNumber = 5;
        public int gridSpacing = 30;


        [BoxGroup("MapBlock")]
        public GameObject normalBlock;

        [BoxGroup("MapBlock")]
        public GameObject smallBlock;

        [BoxGroup("MapBlock")]
        public GameObject bigBlock;
        [Title("MapBlock Size")]
        public Vector2 small;
        public Vector2 normal;
        public Vector2 big;

        [BoxGroup("MapBuilding")]
        public List<GameObject> smallBuildings;
        [BoxGroup("MapBuilding")]
        public List<GameObject> normalBuildings;
        [BoxGroup("MapBuilding")]
        public List<GameObject> bigBuildings;

        [BoxGroup("Bridge")]
        public GameObject bridge;
    }
}
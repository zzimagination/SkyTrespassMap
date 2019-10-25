using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "MapSetting", menuName = "Setting/MapSetting")]
public class MapSetting : ScriptableObject
{
    public int RowNumber = 5;
    public int ColumNumber = 5;
    public int gridSpacing=30;
    [BoxGroup("MapBlock")]
    public GameObject normalBlock;
    [BoxGroup("MapBlock")]
    public GameObject smallBlock;
    [BoxGroup("MapBlock")]
    public GameObject bigBlock;
    [Title("MapBlock Size")]
    public Vector3 normalSize;
    public Vector3 smallSize;
    public Vector3 bigSize;
}
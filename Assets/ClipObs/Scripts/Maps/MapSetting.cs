using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSetting", menuName = "Setting/MapSetting")]
public class MapSetting : ScriptableObject
{
    public int RowNumber = 5;
    public int ColumNumber = 5;
    public int gridSpacing=30;
    public GameObject[] MapBlock;
}
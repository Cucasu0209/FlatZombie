using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapData", menuName = "Data/MapData", order = 0)]
public class MapData : ScriptableObject
{
    public List<LevelData> Levels;
}

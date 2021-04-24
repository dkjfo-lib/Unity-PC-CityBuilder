using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Building", menuName = "MY/Building")]
public class BuildingType : ScriptableObject
{
    public int valuePerStoryHeight;
    [Space]
    public GameObject Prefab;
    public Vector3 offset;

    public BuildingStats Construct(Transform lot, int floors)
    {
        BuildModel(lot, floors);
        return GenerateStats(floors);
    }

    void BuildModel(Transform lot, int stories)
    {
        for (int i = 0; i < stories; i++)
        {
            BuildFloor(lot, i);
        }
    }

    void BuildFloor(Transform lot, int floorCount)
    {
        GameObject.Instantiate(Prefab, offset * floorCount + lot.position, Quaternion.identity, lot);
    }

    BuildingStats GenerateStats(int floors)
    {
        var buildingStats = new BuildingStats
        {
            ValuePerFloor = valuePerStoryHeight,
            FloorCount = floors,
        };
        return buildingStats;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lot : MonoBehaviour
{
    public CityField field;
    public CityFieldPoint point;

    [System.Obsolete("METHODS BUCKO!")]
    public BuildingSlot buildingSlot;
    [System.Obsolete("METHODS BUCKO!")]
    public BuildingType style;

    public BuildingSlot BuildingSlot => buildingSlot;
    public BuildingStats ConstructedBuilding => BuildingSlot.ConstructedBuilding;
    public int LandValue => point.Value;

    private void Start()
    {
        StartCoroutine(HostDevelopment());
    }

    IEnumerator HostDevelopment()
    {
        var wait = new WaitForSeconds(1);
        while (true)
        {
            UpdateField();
            RethinkBuilding();
            yield return wait;
        }
    }

    void UpdateField()
    {
        point = field.GetClosestPoint(transform.position);
    }

    void RethinkBuilding()
    {
        if (ConstructedBuilding != null)
        {
            // maybe deconstruct | abandon | change type | upgrade ?
            if (MaybeUpgrade(ConstructedBuilding))
            {
                // upgrade
                BuildingSlot.Demolish();
                BuildingSlot.Construct(GetBuildingType(), LandValue);
            }
        }
        else
        {
            // maybe build ?
            if (MaybeBuild(LandValue))
            {
                // build
                BuildingSlot.Construct(GetBuildingType(), LandValue);
            }
        }
    }

    bool MaybeBuild(int landValue)
    {
        return landValue > 0;
    }

    bool MaybeUpgrade(BuildingStats constructedBuilding)
    {
        return constructedBuilding.Value < LandValue;
    }

    BuildingType GetBuildingType()
    {
        return style;
    }
}

[System.Serializable]
public class BuildingSlot
{
    [System.Obsolete("METHODS BUCKO!")]
    public Transform slot;
    [System.Obsolete("METHODS BUCKO!")]
    private BuildingStats constructedBuilding;

    public Transform Slot => slot;
    public BuildingStats ConstructedBuilding => constructedBuilding;

    public void Construct(BuildingType buildingType, int value)
    {
        int floors = value / buildingType.valuePerStoryHeight;
        constructedBuilding = buildingType.Construct(Slot, floors);
    }
    public void Demolish()
    {
        Slot.DestroyAllChildren();
    }
}

[System.Serializable]
public class BuildingStats
{
    [System.Obsolete("METHODS BUCKO!")]
    private int valuePerFloor;
    [System.Obsolete("METHODS BUCKO!")]
    private int floors;

    public int FloorCount { get => floors; set => floors = value; }
    public int ValuePerFloor { get => valuePerFloor; set => valuePerFloor = value; }
    public int Value => FloorCount * ValuePerFloor;
}
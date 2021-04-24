using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICityField
{
    bool Constructed { get; }
    CityFieldPoint[] CityFieldPoints { get; }
}

public class CityField : MonoBehaviour, ICityField
{
    public Vector2Int size = Vector2Int.one * 5;
    public CityFieldPoint[] cityFieldPoints;

    public CityFieldPoint[] CityFieldPoints => cityFieldPoints;
    public bool Constructed => CityFieldPoints != null;

    private void Start()
    {
        IFieldBuilder fieldBuilder = new CityFieldBuilder();
        cityFieldPoints = fieldBuilder.CreateField(size);
    }

    public CityFieldPoint GetClosestPoint(Vector3 coord)
    {
        Vector2 coordXY = new Vector2(coord.x, coord.z);
        float minDist = float.MaxValue;
        CityFieldPoint closestPoint = null;
        foreach (var point in CityFieldPoints)
        {
            var dist = Vector2.Distance(point.Coord, coordXY);
            if (dist < minDist)
            {
                minDist = dist;
                closestPoint = point;
            }
        }
        return closestPoint;
    }
}

public interface ICityFieldPoint
{
    int DemandR { get; }
    int DemandC { get; }
    int DemandI { get; }
    int Value { get; }
    Vector2 Coord { get; }
}

[System.Serializable]
public class CityFieldPoint : ICityFieldPoint
{
    public int demandR;
    public int demandC;
    public int demandI;
    public Vector2Int coord;

    public int DemandR => demandR;
    public int DemandC => demandC;
    public int DemandI => demandI;
    public int Value => DemandR + DemandC + DemandI;
    public Vector2 Coord => coord;
}
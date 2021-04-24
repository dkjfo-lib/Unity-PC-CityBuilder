using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFieldBuilder
{
    CityFieldPoint[] CreateField(Vector2Int size);
}

[System.Serializable]
public class CityFieldBuilder : IFieldBuilder
{
    public CityFieldPoint[] CreateField(Vector2Int size)
    {
        var points = new CityFieldPoint[size.x * size.y];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                points[x * size.y + y] = new CityFieldPoint
                {
                    demandC = x,
                    demandR = y,
                    demandI = 2,
                    coord = new Vector2Int(x, y)
                };
            }
        }
        return points;
    }
}

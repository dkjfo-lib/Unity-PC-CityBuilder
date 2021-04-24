using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityFieldVisualizer : MonoBehaviour
{
    public Vector3 pointSize = new Vector3(.25f, .35f, .25f);
    public visY mode = visY.none;

    ICityField field;

    System.Func<ICityFieldPoint, int>[] funcY = new System.Func<ICityFieldPoint, int>[]
    {
        (p)=>0,
        (p)=>p.DemandR,
        (p)=>p.DemandC,
        (p)=>p.DemandI,
        (p)=>p.Value,
    };
    Color[] colors = new[]
    {
        Color.gray,
        Color.green,
        Color.blue,
        new Color(.75f,.25f,0f),
        Color.cyan,
    };

    System.Func<ICityFieldPoint, int> chFuncY;
    Color chColor;

    private void Start()
    {
        SetUp();
        field = GetComponent<ICityField>();
        StartCoroutine(mUpdate());
    }

    IEnumerator mUpdate()
    {
        var wait = new WaitForSeconds(.25f);
        while (true)
        {
            yield return wait;
            SetUp();
        }
    }

    void SetUp()
    {
        chFuncY = funcY[(int)mode];
        chColor = colors[(int)mode];
    }

    private void OnDrawGizmos()
    {
        if (field == null) return;
        if (!field.Constructed) return;

        Gizmos.color = chColor;
        foreach (var point in field.CityFieldPoints)
        {
            float y = chFuncY(point);
            Vector3 position = new Vector3(point.Coord.x, y, point.Coord.y);
            Gizmos.DrawCube(position, pointSize);
        }
    }
}

public enum visY
{
    none,
    demandR,
    demandC,
    demandI,
    value
}
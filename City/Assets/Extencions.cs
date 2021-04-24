using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extencions 
{
    public static void DestroyAllChildren(this Transform transform)
    {
        for (int i = transform.transform.childCount; i > 0; --i)
            GameObject.DestroyImmediate(transform.transform.GetChild(0).gameObject);
    }
}

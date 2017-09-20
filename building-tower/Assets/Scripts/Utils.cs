using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace BuildingTower
{
    public class Utils
    {
        public static GameObject Spawn(GameObject paramPrefab, Transform paramParent = null)
        {
            GameObject newObject = GameObject.Instantiate(paramPrefab) as GameObject;
            newObject.transform.SetParent(paramParent);
            newObject.transform.localPosition = Vector3.zero;
            newObject.transform.localScale = paramPrefab.transform.localScale;
            newObject.SetActive(true);
            return newObject;
        }
        public static void removeAllChildren(Transform paramParent, bool paramInstant = true)
        {
            if (paramParent == null)
                return;
            for (int i = paramParent.childCount - 1; i >= 0; i--)
            {
                if (paramInstant)
                {
                    GameObject.DestroyImmediate(paramParent.GetChild(i).gameObject);
                }
                else
                {
                    paramParent.GetChild(i).gameObject.SetActive(false);
                    GameObject.Destroy(paramParent.GetChild(i).gameObject);
                }
            }
        }
    }
}
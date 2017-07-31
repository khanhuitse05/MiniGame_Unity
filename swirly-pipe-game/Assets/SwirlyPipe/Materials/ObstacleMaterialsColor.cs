using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMaterialsColor : MonoBehaviour {
    public static readonly Color[] colors = { Color.blue, Color.red, Color.green, Color.yellow, Color.magenta };
    private void OnEnable()
    {
        gameObject.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
    }
}

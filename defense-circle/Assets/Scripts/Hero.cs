using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    public float radius = 4.0f;
    float timeMax = 3.0f;
    float timeCurrent = 0;
    public DrawCircles circle;
    void Start()
    {
        circle.setRadius(radius);
    }

    void Update()
    {
        timeCurrent += Time.deltaTime;
        if (timeCurrent > timeMax)
        {
            timeCurrent -= timeMax;
        }
        var angle = (360 / timeMax) * timeCurrent;
        transform.position = radius * new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
    }
    public int GetCurrentValue()
    {
        float _div = (float)timeCurrent / timeMax;
        int _value = (int)(_div * 60);
        return _value;
    }
}

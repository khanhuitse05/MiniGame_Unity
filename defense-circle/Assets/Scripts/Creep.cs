using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour {

    public float minSpeed = 0.5f;
    public float maxSpeed = 1.0f;
    float speed;
    Vector3 posTarget;
    [HideInInspector]
    public int index;
	void Start ()
    {
        posTarget = GameConstants.posCenter;
    }
    private void OnEnable()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update () {
        if (MoveToPoint(posTarget))
        {
            SpawnController.instance.UnSpawnCreep(gameObject);
        }
        GameController.instance.OnCreepTriger(this);
    }

    bool MoveToPoint(Vector3 _point)
    {
        float dist = Vector3.Distance(_point, transform.position);
        if (dist < 0.05f)
        {
            return true;
        }
        Vector3 dir = (_point - transform.position).normalized;
        transform.Translate(dir * Mathf.Min(dist, speed * Time.deltaTime), Space.World);
        return false;
    }
    public void SetIndex(int _index, float _radius)
    {
        index = _index;
        var _angle = (360 / GameConstants.resolution) * _index;
        Vector3 _pos = _radius * new Vector3(Mathf.Cos(Mathf.Deg2Rad * _angle), Mathf.Sin(Mathf.Deg2Rad * _angle));
        transform.position = _pos;
    }
}

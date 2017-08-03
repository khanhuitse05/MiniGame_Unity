using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    public static SpawnController instance { private set; get; }

    public GameObject pfCreep;

    List<GameObject> listActive = new List<GameObject>();
    List<GameObject> listInactive = new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }
    public void ResetSpawn()
    {
        while (listActive.Count > 0)
        {
            UnSpawnCreep(listActive[0]);
        }
    }
    void Update () {
        OnSpawnUpdate();
	}

    bool isSpawn = true;
    float rateCount = 0;
    public float rate = 1;
    public float minRate = 0.5f;
    public float speedRate = 0.01f;
    void OnSpawnUpdate()
    {
        if (isSpawn)
        {
            rateCount += Time.deltaTime;
            if (rateCount>rate)
            {
                rateCount = 0;
                if (rate > minRate)
                {
                    rate -= speedRate;
                }
                SpawnCreep();
            }
            
        }
    }


    public float radius = 5;
    void SpawnCreep()
    {
        Debug.Log("SpawnCreep");
        if (listInactive.Count == 0)
        {
            GameObject _obj = GameObject.Instantiate(pfCreep) as GameObject;
            Creep _creep = _obj.GetComponent<Creep>();
            _creep.SetIndex(GetPosSpawn(), radius);
            listActive.Add(_obj);
        }
        else
        {
            GameObject _obj = listInactive[0];
            listInactive.RemoveAt(0);
            listActive.Add(_obj);
            _obj.gameObject.SetActive(true);
            Creep _creep = _obj.GetComponent<Creep>();
            _creep.SetIndex(GetPosSpawn(), radius);
        }
    }
    int GetPosSpawn()
    {
        int _angle = Random.Range(0, GameConstants.resolution);
        return _angle;
    }
    public void UnSpawnCreep(GameObject _creep)
    {
        _creep.SetActive(false);
        listActive.Remove(_creep);
        listInactive.Add(_creep);
    }
}

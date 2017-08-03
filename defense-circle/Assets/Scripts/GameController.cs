using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController instance { private set; get; }
    private void Awake()
    {
        instance = this;
    }
    float[] RADIUS = { 1.5f, 1.0f, 0.5f , 0.2f};
    public float radiusShield = 4;
    public GameObject pfCircle;
    public GameObject pfArc;
    public int live = 3;
    public Hero hero;
    private List<DrawCircles> listCircle;
      
    private List<DrawArc> listArc;
    private DrawArc currentArc;
    bool isDraw = false;
    int score = 0;
    int time = 0;
    private void Start()
    {
        StartCoroutine(CountTime());
    }
    IEnumerator CountTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (isOver == false)
            {
                time++;
                UI.instance.UpdateText(score, time);
            }
        }
    }
    void Update()
    {
        if (isOver)
        {
            return;
        }
        if (Input.GetMouseButton(0) )
        {
            //Debug.Log("OnMouseDown");
            if (isDraw == false)
            {
                isDraw = true;
                int _index = hero.GetCurrentValue();
                int _exist = CheckExist(_index);
                if (_exist < 0)
                {
                    addCurrentArc(_index);
                }
                else
                {
                    getCurrentArc(_exist);
                }
            }
            else
            {

                int _index = hero.GetCurrentValue();
                int _exist = CheckExist(_index);
                if (_exist >= 0)
                {
                    combinedCurrentArc(_exist);
                }
                else if (_exist == -2)
                {
                    if (_index == 0)
                    {
                        currentArc.end = _index;
                    }
                    else
                    {
                        currentArc.end = _index;
                    }
                }                
            }
            
        }
        else
        {
            if (isDraw)
            {
                isDraw = false;
                listArc.Add(currentArc);
                currentArc = null;
            }
        }
    }
    void addCurrentArc(int _index)
    {
        GameObject _newArc = Instantiate(pfArc) as GameObject;
        currentArc = _newArc.GetComponent<DrawArc>();
        currentArc.radius = radiusShield;
        currentArc.start = _index;
        currentArc.end = _index + 1;
    }
    void getCurrentArc(int _index)
    {
        currentArc = listArc[_index];
        listArc.RemoveAt(_index);
    }
    void combinedCurrentArc(int _index)
    {
        currentArc.start = currentArc.start;
        currentArc.end = listArc[_index].end;
        DrawArc _temp = listArc[_index];
        listArc.RemoveAt(_index);
        Destroy(_temp.gameObject);
    }

    // = -1 : current
    // = -2 : no
    // > 0 : list
    int CheckExist(int _index)
    {
        for (int i = 0; i < listArc.Count; i++)
        {
            if (listArc[i].start < listArc[i].end)
            {
                if (_index >= listArc[i].start && _index <= listArc[i].end)
                {
                    return i;
                }
            }
            else
            {
                if (_index >= listArc[i].start || _index <= listArc[i].end)
                {
                    return i;
                }
            }
            
        }
        if (currentArc != null)
        {
            if (currentArc.start < currentArc.end)
            {
                if (_index >= currentArc.start && _index <= currentArc.end)
                {
                    return -1;
                }
            }
            else
            {
                if (_index >= currentArc.start || _index <= currentArc.end)
                {
                    return -1;
                }
            }
            
        }
        return -2;
    }

    public void OnCreepTriger(Creep _creep)
    {
        float _distance = Vector3.Distance(_creep.transform.position, GameConstants.posCenter);
        if (Mathf.Abs(_distance - radiusShield) <= 0.1f)
        {
            for (int i = 0; i < listArc.Count; i++)
            {
                if (listArc[i].CheckCollision(_creep.index))
                {
                    SpawnController.instance.UnSpawnCreep(_creep.gameObject);
                    DrawArc _temp = listArc[i];
                    listArc.RemoveAt(i);
                    Destroy(_temp.gameObject);
                    score++;
                    return;
                }
            }
        }
        else 
        {
            if (Mathf.Abs(_distance - hero.radius) <= 0.1f)
            {
                SpawnController.instance.UnSpawnCreep(_creep.gameObject);
                if (isOver == false)
                {
                    DrawCircles _temp = listCircle[0];
                    listCircle.RemoveAt(0);
                    Destroy(_temp.gameObject);
                    if (listCircle.Count == 0)
                    {
                        OnGameOver();
                    }
                }
                return;
            }
        }
    }

    public void OnStartGame()
    {
        time = 0;
        score = 0;
        if (listArc != null)
        {
            for (int i = 0; i < listArc.Count; i++)
            {
                Destroy(listArc[i].gameObject);
            }
        }
        listArc = new List<DrawArc>();
        SpawnController.instance.ResetSpawn();
        listCircle = new List<DrawCircles>();
        for (int i = 0; i < live; i++)
        {
            GameObject _circle = Instantiate(pfCircle) as GameObject;
            DrawCircles _draw = _circle.GetComponent<DrawCircles>();
            _draw.radius = RADIUS[i];
            listCircle.Add(_draw);
        }
        isOver = false;
    }
    bool isOver = true;
    void OnGameOver()
    {
        isOver = true;
        UI.instance.OnMain();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public static UI instance { private set; get; }
    private void Awake()
    {
        instance = this;
    }
    public GameObject mainGui;
    public Text txtScore;
    public Text txtTime;
    public void OnPlay()
    {
        mainGui.SetActive(false);
        GameController.instance.OnStartGame();
    }
    public void OnMain()
    {
        mainGui.SetActive(true);
    }
    public void UpdateText(int _score, int _time)
    {
        txtScore.text = _score.ToString();
        txtTime.text = _time.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour {
    public static Pos Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    // pos
    public Transform _posLeft;
    public static Vector3 posLeft { get { return Instance._posLeft.position; } }
    public Transform _posRight;
    public static Vector3 posRight { get { return Instance._posRight.position; } }
    public Transform _posBottom;
    public static Vector3 posBottom { get { return Instance._posBottom.position; } }
    public Transform _posBasket;
    public static Vector3 posBasket { get { return Instance._posBasket.position; } }

    // Object
    public GameObject _objBack;
    public static GameObject objBack { get { return Instance._objBack; } }
    public GameObject _objFront;
    public static GameObject objFront { get { return Instance._objFront; } }
}

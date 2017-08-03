
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    public static GameConstants instance { private set; get; }
    private void Awake() { instance = this; }

    public Transform _posCenter;
    public static Vector3 posCenter { get { return instance._posCenter.position;} }
    public static int resolution = 60;
}

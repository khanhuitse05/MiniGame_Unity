using UnityEngine;
using UnityEngine.UI;

public class GSGamePlay : MonoBehaviour
{
    public static GSGamePlay Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        score = 0;
        highScore = 0;
        txtScore.text = "" + score;
        txtHighScore.text = "" + highScore;
    }
    //
    int score;
    int highScore;
    public Text txtScore;
    public Text txtHighScore;
    public void ResetScore()
    {
        score = 0;
        txtScore.text = "" + score;
    }
    public void OnScore()
    {
        score += 1;
        txtScore.text = "" + score;
        if (score > highScore)
        {
            txtHighScore.text = "" + highScore;
        }
    }
    void DisPlayScore()
    {
        txtScore.text = "" + score;
    }
}

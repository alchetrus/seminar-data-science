using UnityEngine;

public class Score : MonoBehaviour
{
    public int scoreValue { get; private set; }

    private void Awake()
    {
        scoreValue = 0;
    }

    public void takeScore(int _score) {
        scoreValue += _score;
    }
}

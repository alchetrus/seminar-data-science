using UnityEngine;
using TMPro;
public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Score playerScore;

    [SerializeField] private TextMeshProUGUI textMesh;
    
    // Update is called once per frame
    void Update()
    {
        textMesh.text = "Score:" + playerScore.scoreValue;
    }
}

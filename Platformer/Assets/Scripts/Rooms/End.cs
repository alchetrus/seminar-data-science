using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private GameObject[] gems;
    [SerializeField] private GameObject[] spikes;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            string name = "Level";
            Application.LoadLevel(name + 2);
            //UnityEditor.EditorApplication.isPlaying = false;
            //Application.Quit();

            /* float[] possiblePositionX = { -3.5f, 3.5f, 2f, 1f, 6f, 5f };
             float[] possiblePositionY = { -2.5f, -1.5f, -1f, 0f, 1f, 1.5f, 2.5f };
             int randomIndex = Random.Range(0, possiblePositionX.Length);
             int randomIndexY = Random.Range(0, possiblePositionY.Length);
             float positionX = possiblePositionX[randomIndex];
             float positionY = possiblePositionY[randomIndexY];

             foreach (var spike in spikes)
             {
                 spike.transform.localPosition = new Vector3(positionX, -2.5f, 0);
                 randomIndex = Random.Range(0, possiblePositionX.Length);
                 positionX = possiblePositionX[randomIndex];
             }

             foreach (var gem in gems)
             {
                 gem.SetActive(true);
                 gem.transform.localPosition = new Vector3(positionX, positionY, 0);
                 randomIndexY = Random.Range(0, possiblePositionY.Length);
                 positionY = possiblePositionY[randomIndexY];
             }
             player.transform.localPosition = new Vector3(-7.82f, 0.557f, 0);*/
        }
    }
}

using UnityEngine;

public class ScoreCollectible : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private AudioClip pickupSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<Score>().takeScore(value);
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField] private GameOverScreen gameOverScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            FindObjectOfType<AudioManager>().Play("BloodSplash");
            CinemachineShake.Instance.ShakeCamera(5f, .1f);
            Destroy(collision.gameObject);
            gameOverScreen.Setup();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ExitBtn()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

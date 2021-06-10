using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIscript : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}

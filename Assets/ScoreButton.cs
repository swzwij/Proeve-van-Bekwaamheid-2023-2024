using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreButton : MonoBehaviour
{
    public void click(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}

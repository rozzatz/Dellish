using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneloader : MonoBehaviour
{
    public int scene = 1;
    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }
}


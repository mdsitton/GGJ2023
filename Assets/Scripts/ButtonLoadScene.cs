using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{
    //public int sceneIndexToLoad;
    public void LoadNextScene(int sceneIndexToLoad)
    {
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}

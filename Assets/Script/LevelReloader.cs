using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReloader : MonoBehaviour
{
    public void ReloadScene()
    {
        Debug.Log("reloadscene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

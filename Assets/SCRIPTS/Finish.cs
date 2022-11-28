using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Finish : MonoBehaviour
{




    private void OnTriggerEnter(Collider collision)
    {
        ReloadLevel();
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }
}


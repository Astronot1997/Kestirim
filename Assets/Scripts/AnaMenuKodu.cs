using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenuKodu : MonoBehaviour
{
    // Start is called before the first frame update
   

    public void ExitApp()
    {
        Application.Quit();
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadTutor()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}

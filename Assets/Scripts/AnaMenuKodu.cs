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
    public void Load1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Load2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void Load3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
    public void Load4()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }
    public void Load5()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
    }
}

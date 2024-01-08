using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFuncs : MonoBehaviour
{
    public GameObject credits;
    public GameObject menu;
    public void Quit()
    {
        Application.Quit();
    }
    public void ToggleCredits()
    {
        if (credits.activeSelf)
        {
            credits.SetActive(false);
            menu.SetActive(true);
        }
        else
        {
            credits.SetActive(true);
            menu.SetActive(false);
        }
    }
    public void StartGame(int id)
    {
        SceneManager.LoadScene(id);
    }
    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }
}

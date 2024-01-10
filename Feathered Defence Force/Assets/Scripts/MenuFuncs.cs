using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFuncs : MonoBehaviour
{
    public GameObject credits;
    public GameObject menu;
    public GameObject patchnotes;
    public List<GameObject> menuList;
    public void Start()
    {
        menuList = new List<GameObject>();
        menuList.Add(credits);
        menuList.Add(menu);
        menuList.Add(patchnotes);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OpenCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }
    public void OpenNotes() 
    {
        menu.SetActive(false);
        patchnotes.SetActive(true);
    }
    public void GoToMenu()
    {
        DisableAll();
        menu.SetActive(true);
    }
    public void StartGame(int id)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(id);
    }
    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }
    public void DisableAll()
    {
        for (int i = 0; i < menuList.Count; i++)
        {
            if (menuList[i] != null)
            {
                menuList[i].SetActive(false);
            }
        }
    }
}

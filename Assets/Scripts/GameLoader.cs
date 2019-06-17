using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour
{

    private GameObject menu;
    private GameObject inventory;

    void Start()
    {
        menu = GameObject.Find("MENU");
        menu.SetActive(false);

        inventory = GameObject.Find("Inventory");
        inventory.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
            {
                showMenu();
            }
            else
            {
                hideMenu();
            }
        }
        if (Input.GetKeyDown(KeyCode.I)) { 
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                inventory.SetActive(true);
                Time.timeScale = 0;
            }

        }
    }


    public void showMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void hideMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void continueGame()
    {
        Application.LoadLevel(1);
        Time.timeScale = 1;
    }

    public void newGame()
    {
        Application.LoadLevel(1);
        Time.timeScale = 1;
    }

    public void loadGame()
    {
        Application.LoadLevel(1);
        Time.timeScale = 1;
    }

    public void settings()
    {
        Application.Quit();
    }

    public void restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1;
    }

    public void exit()
    {
        if (Application.loadedLevel > 0)
        {
            Application.LoadLevel(0);
            Time.timeScale = 1;
        }
        else
        {
            Application.Quit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject passMenu;
    public GameObject failureMenu;
    public GameObject pauseMenu;
    public GameObject selectMenu;
    public GameObject settings_Back;

    public GameManager gameManager;
    public Player player;
    public CameraFollow cameraFollow;

    public void GameFail()
    {
        gameManager.levels[gameManager.currentLevel - 1].CleanLevel();
        failureMenu.SetActive(true);
    }

    #region 按钮区域

    public void PassToSelectMenu()
    {
        gameManager.levels[gameManager.currentLevel - 1].CleanLevel();
        passMenu.SetActive(false);
        selectMenu.SetActive(true);
        player.ToSafe();
    }

    public void FailureToSelectMenu()
    {
        gameManager.levels[gameManager.currentLevel - 1].CleanLevel();
        failureMenu.SetActive(false);
        selectMenu.SetActive(true);
        player.ToSafe();
    }

    public void NextLevel()
    {
        gameManager.levels[gameManager.currentLevel - 1].CleanLevel();
        if (gameManager.currentLevel < gameManager.maxLevel)
        {
            gameManager.currentLevel += 1;
            player.Init();
            cameraFollow.Init();
            gameManager.levels[gameManager.currentLevel - 1].Init();
            passMenu.SetActive(false);
        }
        else
        {
            PassToSelectMenu();
        }
    }

    public void PauseToSelect()
    {
        pauseMenu.SetActive(false);
        gameManager.levels[gameManager.currentLevel - 1].CleanLevel();
        selectMenu.SetActive(true);
    }

    public void PauseRestartGame()
    {
        pauseMenu.SetActive(false);
        gameManager.levels[gameManager.currentLevel - 1].CleanLevel();
        player.Init();
        cameraFollow.Init();
        gameManager.levels[gameManager.currentLevel - 1].Init();
    }

    public void RestartGame()
    {
        gameManager.levels[gameManager.currentLevel - 1].CleanLevel();
        player.Init();
        cameraFollow.Init();
        gameManager.levels[gameManager.currentLevel - 1].Init();
        if(passMenu.activeSelf == true)
        {
            passMenu.SetActive(false);
        }
        if(failureMenu.activeSelf == true)
        {
            failureMenu.SetActive(false);
        }
    }
    
    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameManager.LoadData();
        selectMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        gameManager.SaveData();
    }

    public void OprateSettings()
    {
        mainMenu.SetActive(false);
        if(settings_Back.activeSelf == false)
        {
            settings.SetActive(true);
        }
        settings.SetActive(true);
    }

    public void EnablePause()
    {
        pauseMenu.SetActive(true);
    }

    //选关区域

    public void SharedPart()
    {
        player.enabled = true;
        gameManager.levels[gameManager.currentLevel - 1].Init();  
        player.Init();                                           
        cameraFollow.Init();
        selectMenu.SetActive(false);
    }
    public void Level_1()
    {
        gameManager.currentLevel = 1;
        SharedPart();
    }

    public void Level_2()
    {
        gameManager.currentLevel = 2;
        SharedPart();
    }

    public void Level_3()
    {
        gameManager.currentLevel = 3;
        SharedPart();
    }

    public void Level_4()
    {
        gameManager.currentLevel = 4;
        SharedPart();
    }

    //回退区域
    public void SettingsToMainMenu()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SelectToMainMenu()
    {
        selectMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void PauseBackToGame()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(false);
    }
    #endregion

}

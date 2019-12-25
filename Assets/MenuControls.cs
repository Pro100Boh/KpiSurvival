using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class MenuControls : MonoBehaviour
{
    void Update()
    {
        if (Game.gameState == GameState.PlayerDied)
        {
            UpdateMenuText("you died!");
        }
        else if (Game.gameState == GameState.PlayerWon)
        {
            UpdateMenuText("you won!");
        }
    }


    public void UpdateMenuText(string text)
    {
        Text menuText = Resources.FindObjectsOfTypeAll(typeof(Text)).Cast<Text>().Where(t => t.name == "MenuText").First();
        menuText.alignment = TextAnchor.MiddleCenter;
        menuText.text = text;
    }

    public void PlayPressed()
    {
        SceneManager.LoadScene("Game");

        Game.StartNewGame();
    }

    public void ExitPressed()
    {
        Game.Exit();
    }
}

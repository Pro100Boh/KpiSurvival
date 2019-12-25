using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game
{
    public const int TILE_LAYER = 9;
    public const int ACTOR_LAYER = 10;
    public const int NODE_LAYER = 11;

    public const string PLAYER_TAG = "P";

    private static Game instance;

    private static bool playerDied = false;

    public static Game Instance
    {
        get
        {
            if (instance == null)
                instance = new Game();
            return instance;
        }
    }
    public static bool PlayerDied
    {
        get
        {
            return playerDied;
        }
    }

    public World world;

    public AudioCache audio;

    private Game()
    {
        world = GameObject.Find("World").GetComponent<World>();
        audio = new AudioCache().Load();
    }

    public static void PlayerDeath()
    {
        playerDied = true;
        SceneManager.LoadScene("Menu");
    }

    public static void StartNewGame()
    {
        instance = null;
    }

    public static void Exit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }
}

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

    public static Game Instance
    {
        get
        {
            if (instance == null)
                instance = new Game();
            return instance;
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
        if (EditorUtility.DisplayDialog("Game over", "You died!", "Exit", "Restart"))
        {
            Exit();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            Restart();
        }
    }

    public static void Restart()
    {
        instance = null;
    }

    public static void Exit()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}

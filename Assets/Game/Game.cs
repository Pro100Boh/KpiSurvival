using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
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

    private Game()
    {
        world = GameObject.Find("World").GetComponent<World>();
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

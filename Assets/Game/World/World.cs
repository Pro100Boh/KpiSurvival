using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour
{
    public new Camera camera;

    public Font font;

    public Material spriteMat;

    public Material deskActorMat;

    public Material deskNodeMat;

    public Material deskGroundMat;

    public Sprite[] weaponSprites;

    public Sprite health;

    public GameObject mobDeath;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // exit by Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Game.Exit();
        }

        if (Game.GetCurrentMobsCount() == 0)
            Game.PlayerVictory();
    }
}

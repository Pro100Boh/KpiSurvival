using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // exit by 'P'
        if (Input.GetKeyDown(KeyCode.P))
        {
            Game.Exit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombiesCountDisplay : MonoBehaviour
{
    void Update()
    {
        Text zombiesCountText = GetComponent<Text>();
        zombiesCountText.text = "zombies - " + Game.GetCurrentMobsCount();
    }
}

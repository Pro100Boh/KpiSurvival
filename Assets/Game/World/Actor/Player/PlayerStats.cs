using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : ActorBehaviour<Player>
{
    public const int HEALTH_IMG_INDEX = 1;
    public const int HEALTH_TXT_INDEX = 2;
    public const float HEALTH_SPEED_CHANGE = 0.2f;

    private float health = 100;
    private float visibleHealth = 100;
    private float healthBarCount;
    private Image healthBar;
    private Text healthText;

    void Start()
    {
        healthBar = transform.GetChild(HEALTH_IMG_INDEX).GetComponent<Image>();
        healthText = transform.GetChild(HEALTH_TXT_INDEX).GetComponent<Text>();
        healthBarCount = healthBar.rectTransform.sizeDelta.x / 100f;
    }

    void Update()
    {
        float dif = health - visibleHealth;
        if (dif > 0.5f || dif < -0.5f)
        {
            float move = (dif) / 30f;
            visibleHealth += move;
            float y = healthBar.rectTransform.sizeDelta.y;
            healthBar.rectTransform.sizeDelta = new Vector2(healthBarCount * visibleHealth, y);
        }
    }

    public void UpdateText()
    {
        healthText.text = Mathf.RoundToInt(health) + "%";
    }

    public float Health { get { return health; } }

    public void DecreaseHealth(float health)
    {
        this.health -= health;
        UpdateText();
    }
}

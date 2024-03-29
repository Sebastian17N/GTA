using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [SerializeField]
    Text HealthCounter;

    [SerializeField]
    Text BulletsCounter;
    void Awake()
    {
        FindObjectOfType<Player>().GetComponent<Entity>().OnHealthChanged += health =>
        {
            HealthCounter.text = health.ToString();
        };
        FindObjectOfType<PlayerShooting>().OnBulletsChange += bullets =>
        {
            BulletsCounter.text = bullets.ToString();
        };
    }
}

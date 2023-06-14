using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image player1HealthBar;
    [SerializeField] private Image player2HealthBar;
    void Start()
    {
        player1HealthBar.fillAmount = GameManager.Instance.Player2Health / 100f;
        player2HealthBar.fillAmount = GameManager.Instance.Player2Health/100f;
    }

    void Update()
    {

        if (player1HealthBar.fillAmount != GameManager.Instance.Player1Health / 100f)
        {
            player1HealthBar.fillAmount = GameManager.Instance.Player1Health / 100f;

        }
        if (player2HealthBar.fillAmount != GameManager.Instance.Player2Health / 100f)
        {
            player2HealthBar.fillAmount = GameManager.Instance.Player2Health / 100f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownUI : MonoBehaviour
{
    protected Game game;

    [SerializeField] protected Text timeText;


    private void Start()
    {
        game = Game.Instance;
    }

    private void Update()
    {
        UpdateTimeUI();
    }

    protected void UpdateTimeUI()
    {
        int min = Mathf.FloorToInt(game.gameCountDown / 60);
        int sec = Mathf.FloorToInt(game.gameCountDown - min*60) % 60;
        timeText.text = $"{min}:{sec}";
        return;
    }

}

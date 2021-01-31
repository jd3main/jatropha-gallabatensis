using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public float startDeley = 1f;
    public UnityEvent gameStartEvent = new UnityEvent();
    public UnityEvent gameEndEvent = new UnityEvent();

    public float gameTimeLimit = 120;
    private float gameStartTime;
    public float gameTime => Time.time - gameStartTime;
    public float gameCountDown => Mathf.Max(gameTimeLimit - gameTime, 0);

    public int gameResult = 0;

    public string endScene;

    void Awake()
    {
        gameStartTime = 0;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Has multiple Game in scene");
            Destroy(this);
        }
    }

    void Start()
    {
        StartCoroutine(GameCoroutine());
    }

    private IEnumerator GameCoroutine()
    {
        yield return new WaitForSeconds(startDeley);
        gameStartTime = Time.time;
        gameStartEvent.Invoke();
        yield return new WaitForSeconds(gameTimeLimit);
        gameEndEvent.Invoke();
        SceneManager.LoadScene(endScene);
    }
}

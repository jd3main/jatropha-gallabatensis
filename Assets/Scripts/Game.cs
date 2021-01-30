using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public UnityEvent globalStartEvent = new UnityEvent();
    public float globalTimer { get; private set; }

    void Awake()
    {
        globalTimer = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        globalStartEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        globalTimer += Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

class RandomDelay
{
    public float delay { get; private set; }
    public float range_min { get; private set; }
    public float range_max { get; private set; }

    public RandomDelay(float min, float max)
    {
        range_min = min;
        range_max = max;
        delay = 0.0f;
    }

    public void Tick(float timeDelta)
    {
        delay -= timeDelta;
    }

    public bool Due()
    {
        return delay <= 0.0f;
    }

    public void Reset()
    {
        delay = Random.Range(range_min, range_max);
    }

    public void ForceDue()
    {
        delay = 0.0f;
    }

    public bool CheckDueAndReset()
    {
        bool ret = Due();
        if (ret)
        {
            Reset();
        }
        return ret;
    }
}

public class AI : CharacterController
{
    private Vector2 previous_pos;
    private RandomDelay pathfind_delay;
    private int pathfind_state;

    float[,] map;
    public Tilemap walls;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    void PathFinder()
    {

    }

}

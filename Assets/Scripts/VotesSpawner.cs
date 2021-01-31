using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum VotesSpawnMode
{
    Random,
    FixedPos,
}

public class VotesSpawner : MonoBehaviour
{
    public int initialNumVotes = 20;
    public VotesSpawnMode initialSpawnMode = VotesSpawnMode.Random;
    public Transform spawnPoint;
    public Vector3 spawnPos;
    public BoundsInt mapBound;

    public GameObject prefab;

    public Tilemap walls;
    public Tilemap ground;

    [SerializeField] private float tileScale = 0.64f;


    private void Awake()
    {
        if (prefab == null)
        {
            prefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            prefab.transform.localScale *= 0.05f;
            prefab.SetActive(false);
        }
    }

    private void Start()
    {
        if (initialNumVotes > 0)
        {
            switch (initialSpawnMode)
            {
                case VotesSpawnMode.Random:
                    RandomSpawn(mapBound, initialNumVotes);
                    break;
                case VotesSpawnMode.FixedPos:
                    SpawnAt(spawnPoint.position, initialNumVotes);
                    break;
            }
        }
    }


    void RandomSpawn(BoundsInt bounds, int n)
    {
        for (int i = 0; i < n; i++)
        {
            float x = Random.Range(bounds.xMin, bounds.xMax) + Random.value;
            float y = Random.Range(bounds.yMin, bounds.yMax) + Random.value;
            Vector3 pos = new Vector3(x, y, 0);
            //print(pos);
            pos *= tileScale;
            SpawnAt(pos);
        }
    }

    void SpawnAt(Vector3 pos, int n=1)
    {
        for (int i = 0; i < n; i++)
        {
            var dot = Instantiate(prefab);
            dot.transform.position = pos;
        }
    }
}

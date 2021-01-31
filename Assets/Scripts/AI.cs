using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public Tilemap walls;
    BoundsInt tileBounds;
    float[,] map;
    PathFind.Grid pathfind_grid;
    PathFind.Point currentPos;
    PathFind.Point lastPos;
    List<PathFind.Point> path;


    // For debugging
    GameObject dest;
    GameObject next;
    [SerializeField] Vector2Int currGridVec;
    [SerializeField] Vector2Int nextGridVec;
    [SerializeField] Vector2Int destGridVec;


    // Start is called before the first frame update
    void Start()
    {
        // Debug
        //dest = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //next = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        currGridVec = new Vector2Int();
        nextGridVec = new Vector2Int();
        destGridVec = new Vector2Int();


        tileBounds = walls.cellBounds;
        //  Debug.Log("Bounds: [" + tileBounds.xMin + "," + tileBounds.yMin + "," + tileBounds.zMin + "] - [" + tileBounds.xMax + ", " + tileBounds.yMax + ", " + tileBounds.zMax + "]");

        map = new float[tileBounds.xMax - tileBounds.xMin, tileBounds.yMax - tileBounds.yMin];

        for (int x = tileBounds.xMin; x < tileBounds.xMax; x++)
        {
            for (int y = tileBounds.yMin; y < tileBounds.yMax; y++)
            {
                TileBase tile = walls.GetTile(new Vector3Int(x, y, tileBounds.zMin));
                if (tile == null)
                {
                    map[x - tileBounds.xMin, y - tileBounds.yMin] = 1.0f;
                }
                else
                {
                    //  Debug.Log((x - tileBounds.xMin) + " " + (y - tileBounds.yMin) + " " + tile.name);
                    map[x - tileBounds.xMin, y - tileBounds.yMin] = 0.0f;
                }
            }
        }

        pathfind_grid = new PathFind.Grid(tileBounds.xMax - tileBounds.xMin, tileBounds.yMax - tileBounds.yMin, map);
    }

    // Update is called once per frame
    new void Update()
    {

    }

    new void FixedUpdate()
    {
        if (CheckPlayer())
        {

        }
        else if (CheckVote())
        {

        }
        else
        {
            Wander();
        }

        rb.MovePosition(rb.position + movement * Speed * DashSpeedMultiplier * Time.fixedDeltaTime);
        if (movement.sqrMagnitude != 0) animator.SetBool("Move", true);
        else { animator.SetBool("Move", false); }
        voteCountsText.text = votes.ToString();
    }

    int RandomInt(int range)
    {
        return RandomInt(0, range);
    }

    int RandomInt(int min, int max)
    {
        return Mathf.FloorToInt(Random.Range(min, max));
    }

    bool Between(PathFind.Point corner_a, PathFind.Point corner_b, PathFind.Point test)
    {
        return (((corner_a.x <= test.x && test.x <= corner_b.x) ||
                 (corner_a.x >= test.x && test.x >= corner_b.x)) &&
                ((corner_a.y <= test.y && test.y <= corner_b.y) ||
                 (corner_a.y >= test.y && test.y >= corner_b.y)));
    }

    void FindDestination()
    {
        int randomX = RandomInt(map.GetUpperBound(0) + 1);
        int randomY = RandomInt(map.GetUpperBound(1) + 1);

        while (map[randomX, randomY] == 0.0)
        {
            randomX = RandomInt(map.GetUpperBound(0) + 1);
            randomY = RandomInt(map.GetUpperBound(1) + 1);
        }

        path = PathFind.Pathfinding.FindPath(pathfind_grid, currentPos, new PathFind.Point(randomX, randomY));
        //  Debug.Log("Path length = " + path.Count);
    }

    bool CheckPlayer()
    {
        return false;
    }

    bool CheckVote()
    {
        return false;
    }

    void Wander()
    {
        Vector3Int cellPos = walls.WorldToCell(new Vector3(rb.position.x, rb.position.y - 0.56f, tileBounds.zMin));

        // Debug
        Debug.Log("rb.position: " + rb.position.x + " " + (rb.position.y - 0.56f));

        if (currentPos == null)
        {
            currentPos = new PathFind.Point(cellPos.x - tileBounds.xMin, cellPos.y - tileBounds.yMin);
            lastPos = currentPos;
        }
        else
        {
            lastPos = currentPos;
            currentPos = new PathFind.Point(cellPos.x - tileBounds.xMin, cellPos.y - tileBounds.yMin);
        }

        currGridVec.x = currentPos.x;
        currGridVec.y = currentPos.y;

        if (path == null || path.Count == 0)
        {
            FindDestination();
        }
        else if (Between(lastPos, currentPos, path.First()))
        {
            path.RemoveAt(0);
        }
        else
        {
            //  Debug.Log((tileBounds.xMin + path.First().x) + " " + (tileBounds.yMin + path.First().y));
            Vector3 worldPos = walls.CellToWorld(new Vector3Int(tileBounds.xMin + path.First().x, tileBounds.yMin + path.First().y, 0)) + new Vector3(0.32f, 0.32f, 0.0f);
            movement = new Vector2(worldPos.x - rb.position.x, worldPos.y - rb.position.y + 0.56f).normalized;

            // Debug
            //next.transform.position = worldPos;
            //dest.transform.position = walls.CellToWorld(new Vector3Int(tileBounds.xMin + path.Last().x, tileBounds.yMin + path.Last().y, 0)) + new Vector3(0.32f, 0.32f, 0.0f);
            nextGridVec.x = path.First().x;
            nextGridVec.y = path.First().y;
            destGridVec.x = path.Last().x;
            destGridVec.y = path.Last().y;
        }
    }

}

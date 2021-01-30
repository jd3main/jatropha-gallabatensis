using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum VoteCountingMethod
{
    Debug,
    SumIncludingPerson,
}

public class VotesUI : MonoBehaviour
{
    protected Game game;

    [SerializeField] protected RectTransform votesBar;
    [SerializeField] protected Vector2Int electorBarSize = new Vector2Int(20, 3);
    [SerializeField] protected Vector2 electorDist = new Vector2(10, 20);
    [SerializeField] protected int electorsCount => electorBarSize.x * electorBarSize.y;
    [SerializeField] protected GameObject electorPrefab;
    protected List<List<Image>> electors;

    public Color colorA = new Color(0.9f, 0.6f, 0.1f);
    public Color colorB = new Color(0.1f, 0.6f, 0.9f);

    public VoteCountingMethod countingMethodA;
    public VoteCountingMethod countingMethodB;

    [SerializeField] protected int[] debugVoteCounts = new int[] { 10, 20 };

    [SerializeField] protected Text votesAText;
    [SerializeField] protected Text votesBText;

    [SerializeField] GameObject player;
 
    void Start()
    {
        game = Game.Instance;
        CreateVoteBar();
    }

    void Update()
    {
        int votesA = CountVotes(0, countingMethodA);
        int votesB = CountVotes(1, countingMethodB);
        UpdateVotesBar(votesA, votesB);
        UpdateVotesNumerUI(votesA, votesB);
    }

    protected void CreateVoteBar()
    {
        electors = new List<List<Image>>();
        for (int y = 0; y < electorBarSize.y; y++)
            electors.Add(new List<Image>());

        var width = electorBarSize.x * electorDist.x;
        var height = electorBarSize.y * electorDist.y;
        for (int y = 0; y < electorBarSize.y; y++)
        {
            for (int x = 0; x < electorBarSize.x; x++)
            {
                GameObject elector = Instantiate(electorPrefab);
                elector.transform.SetParent(votesBar);
                elector.transform.position = votesBar.position
                    + new Vector3((x+0.5f)*electorDist.x - width/2, (y+0.5f)*electorDist.y- height/2, 0);

                Image img = elector.GetComponent<Image>();
                img.color = Color.white;
                electors[y].Add(img);
            }
        }
    }

    protected void UpdateVotesBar(int votesA, int votesB)
    {
        float ratio = votesA / (float)(votesA+votesB);

        for (int y = 0; y < electorBarSize.y; y++)
        {
            for (int x = 0; x < electorBarSize.x; x++)
            {
                Image img = electors[y][x];
                int index = x * electorBarSize.y + y;

                if (index < electorsCount * ratio)
                    img.color = colorA;
                else
                    img.color = colorB;
            }
        }
    }

    protected int CountVotes(int team, VoteCountingMethod method)
    {
        // TODO
        switch (method)
        {
            case VoteCountingMethod.SumIncludingPerson:
                return player.GetComponent<CharacterController>().votes;
            case VoteCountingMethod.Debug:
                return debugVoteCounts[team];
            default:
                return 0;
        }
    }

    protected void UpdateVotesNumerUI(int votesA, int votesB)
    {
        votesAText.text = $"{votesA}";
        votesBText.text = $"{votesB}";
    }
}

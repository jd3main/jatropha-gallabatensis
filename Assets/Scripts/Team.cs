using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamType
{
    Neutral,
    Thief,
    Protector,
}

public class Team : MonoBehaviour
{
    [SerializeField] private TeamType teamType = TeamType.Neutral;
    public TeamType TeamType
    {
        get { return teamType; }
        private set { teamType = value; }
    }

    public int TeamNumber()
    {
        return (int)TeamType;
    }
}

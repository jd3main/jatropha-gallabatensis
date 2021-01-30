using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlaceVotes : MonoBehaviour
{
    CharacterController character;
    Team team;
    VoteContainer target = null;

    private void Start()
    {
        team = GetComponent<Team>();
        character = GetComponent<CharacterController>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        var vc = other.GetComponent<VoteContainer>();
        if (vc != null)
            target = vc;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (target.tag == "PUIPUI")
            {
                if (team.TeamType == TeamType.Protector)
                {
                    target.Take(character);
                }
                else if (team.TeamType == TeamType.Thief)
                {
                    target.Place(character);
                }
            }
            else
            {
                if (team.TeamType == TeamType.Protector)
                {
                    target.Place(character);
                }
                else if (team.TeamType == TeamType.Thief)
                {
                    target.Take(character);
                }
            }
        }
    }
}

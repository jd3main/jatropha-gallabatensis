using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteContainerUI : MonoBehaviour
{
    [SerializeField] protected VoteContainer container;
    [SerializeField] protected Text votesText;

    void Update()
    {
        votesText.text = $"{container.Votes}";
    }
}

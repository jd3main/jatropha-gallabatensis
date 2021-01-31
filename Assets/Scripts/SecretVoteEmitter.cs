using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretVoteEmitter : MonoBehaviour
{
    public GameObject VotePrefab;
    public int amounts = 30;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < amounts; i++)
            {
                GameObject ins = Instantiate(VotePrefab, transform, false);
                ins.transform.SetParent(null, true);
                ins.GetComponent<Vote>().Fling(Random.insideUnitCircle * 3);
            }
        }
        Debug.Log("There's something weird here....hmm.....");
    }

}

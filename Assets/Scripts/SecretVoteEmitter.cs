using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SecretVoteEmitter : MonoBehaviour
{
    public GameObject VotePrefab;
    public int amounts = 30;
    [SerializeField] Light2D light2d;
    [SerializeField] AudioSource sfx;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OhYeahLight());
            for (int i = 0; i < amounts; i++)
            {
                GameObject ins = Instantiate(VotePrefab, transform, false);
                ins.transform.SetParent(null, true);
                ins.GetComponent<Vote>().Fling(Random.insideUnitCircle * 3);
            }
        }
    }

    IEnumerator OhYeahLight()
    {
        light2d.intensity = 0.8f;
        float timeElapsed = 0;
        sfx.Play();
        yield return new WaitForSeconds(1);

        while (timeElapsed < 2)
        {
            light2d.intensity = Mathf.Lerp(0.8f, 0.0f, timeElapsed / 2);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        light2d.intensity = 0.0f;
    }

}

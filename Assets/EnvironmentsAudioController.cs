using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Experimental.Rendering.Universal;

public class EnvironmentsAudioController : MonoBehaviour
{
    [SerializeField] private EmitterRef emitterRef;
    [SerializeField] private Transform lightsRoot;
    [SerializeField] private Light2D[] lights;
    [SerializeField] private GameObject player;

    [SerializeField] private bool isIndoor = false;

    private void Start()
    {
        lights = lightsRoot.GetComponentsInChildren<Light2D>();
    }

    void Update()
    {
        if (isIndoor)
        {
            if (IsBright())
                emitterRef.Target.SetParameter("Space", 1);
            else
                emitterRef.Target.SetParameter("Space", 2);
        }
        else
        {
            emitterRef.Target.SetParameter("Space", 0);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag=="in-door")
            isIndoor = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "in-door")
            isIndoor = false;
    }

    bool IsBright()
    {
        foreach (var light in lights)
        {
            Vector3 lightPos = light.transform.position;
            Vector3 playerPos = player.transform.position;
            float dist = Vector3.Distance(lightPos, playerPos);
            float lightRadius = light.pointLightOuterRadius;
            if (dist < lightRadius && light.gameObject.activeSelf && light.enabled)
            {
                var hit = Physics2D.Linecast(lightPos, playerPos);
                if (!hit)
                    return true;
            }
        }
        return false;
    }
}

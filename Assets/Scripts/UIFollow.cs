using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[ExecuteInEditMode]
public class UIFollow : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        if (target != null)
        {
            Vector3 uiPos = Camera.main.WorldToScreenPoint(target.position);
            this.transform.position = uiPos;
        }
        else if (Application.isPlaying)
        {
            Debug.LogError("UIFollow.target is missing in game object: " + gameObject.name, this);
            this.enabled = false;
        }
    }
}
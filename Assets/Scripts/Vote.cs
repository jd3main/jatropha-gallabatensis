using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vote : MonoBehaviour
{
    public bool pickable = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pickable) Destroy(gameObject);
    }

    public void SetUnpickable()
    {
        StartCoroutine(UnPickable());
        StartCoroutine(Blinking());
    }

    
    IEnumerator UnPickable()
    {
        pickable = false;
        yield return new WaitForSeconds(3);
        pickable = true;
    }

    IEnumerator Blinking()
    {
        SpriteRenderer SR = GetComponent<SpriteRenderer>();
        for(int i=0; i<6; i++)
        {
            SR.enabled = false;
            yield return new WaitForSeconds(0.25f);
            SR.enabled = true;
            yield return new WaitForSeconds(0.25f);
        }
        GetComponents<Collider2D>()[1].enabled = false;
        
    }
}

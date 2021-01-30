using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vote : MonoBehaviour
{
    public Tilemap walls;
    public bool pickable = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pickable) Destroy(gameObject);
    }

    public void Fling(Vector2 direction)
    {
        StartCoroutine(FlingCoroutine(direction));
    }

    IEnumerator FlingCoroutine(Vector2 direction)
    {
        pickable = false;
        float R = direction.magnitude;
        float H = R*2;
        float dr = R/60;
        float a = -4 * H / (R * R);
        Vector3 initialPos = transform.position;
        float rotateSpeed = 10 + Random.Range(10,15);
        float rotOffset = Random.Range(0, 4);

        for (int i=0; i<32; i++)
        {
            var hit = Physics2D.CircleCast(initialPos, 0.2f, direction, R, (1 << LayerMask.NameToLayer("Wall")));
            if (hit.collider == null)
                break;
            direction *= 0.5f;
        }

        for (float r = 0; r <= R; r += dr)
        {
            float h = a * r * (r - R);
            float x = direction.x * r;
            float y = direction.y * r + h;
            Vector3 targetPosition = initialPos + new Vector3(x, y, 0);
            transform.position = targetPosition;
            transform.rotation = Quaternion.Euler(0, 0, rotOffset + r / dr * rotateSpeed);
            yield return new WaitForFixedUpdate();
        }
        pickable = true;
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

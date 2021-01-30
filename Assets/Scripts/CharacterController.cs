using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float Speed = 5.0f;
    public int votes = 0;
    public PlayerState State = PlayerState.moving;
    public enum PlayerState { moving, dashing };

    protected Vector2 movement;
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    protected float dashDuration = 3.0f;
    [SerializeField]
    protected float dashSpeedMultiplier = 1.0f;
    [SerializeField]
    protected float dashMaxMultiplier = 3.0f;
    [SerializeField]
    protected float dashMinMultiplier = 1.0f;

    protected virtual void Update()
    {
        // hard coded temporarily.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(dash());


        //transform.localScale = new Vector3(transform.localScale.x * movement.x == 0 ? 1: movement.x, transform.localScale.y, transform.localScale.z);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Speed * dashSpeedMultiplier * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "PUIPUI")
        {
            votes = 0;
        }
        // getComponent here is not a good way.
        if (collision.gameObject.tag == "player" && collision.gameObject.GetComponent<CharacterController>().State == PlayerState.dashing)
        {
            votesStolen();
        }
        if (collision.gameObject.tag == "player" && State == PlayerState.dashing)
        {
            // steal others' votes
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "vote") votes += 1;
    }

    void votesStolen()
    {

    }
    

    IEnumerator dash()
    {
        float timeElapsed = 0;
        State = PlayerState.dashing;

        while (timeElapsed < dashDuration)
        {
            dashSpeedMultiplier = Mathf.Lerp(dashMaxMultiplier, dashMinMultiplier, timeElapsed / dashDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        dashSpeedMultiplier = 1.0f;
        State = PlayerState.moving;
    }
}

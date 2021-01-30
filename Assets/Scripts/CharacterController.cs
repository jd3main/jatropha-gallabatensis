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
    protected float dashSpeedMultiplier;


    protected virtual void Update()
    {
        // hard coded temporarily.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        //transform.localScale = new Vector3(transform.localScale.x * movement.x == 0 ? 1: movement.x, transform.localScale.y, transform.localScale.z);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Speed * Time.fixedDeltaTime);
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

    void dash()
    {

    }
}

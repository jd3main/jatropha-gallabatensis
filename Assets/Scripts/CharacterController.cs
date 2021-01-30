using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public float Speed = 5.0f;
    public int votes = 0;
    public PlayerState State = PlayerState.moving;
    public enum PlayerState { moving, dashing };
    public float DashDuration = 3.0f;
    public float DashSpeedMultiplier = 1.0f;
    public float DashMaxMultiplier = 3.0f;
    public float DashMinMultiplier = 0.0f;

    public int MaxVoteLost = 4;
    public float VoteEmittedForce = 0.1f;

    protected Vector2 movement;
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    protected Text voteCountsText;
    [SerializeField]
    protected GameObject votePrefab;
    protected float lastMovementX = 1.0f;
    [SerializeField]
    protected Animator animator;

    protected void Update()
    {
        // hard coded temporarily.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(dash());

        if (lastMovementX != movement.x && movement.x != 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
            lastMovementX = movement.x;
        }

    }

    protected void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Speed * DashSpeedMultiplier * Time.fixedDeltaTime);
        if (movement.x != 0 || movement.y != 0) animator.SetBool("Move", true);
        else { animator.SetBool("Move", false); }
        voteCountsText.text = votes.ToString();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //votesStolen();
        if (collision.gameObject.tag == "PUIPUI")
        {
            votes = 0;
        }
        // getComponent here is not a good way.
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<CharacterController>().State == PlayerState.dashing)
        {
            votesStolen();
        }
        if (collision.gameObject.tag == "Player" && State == PlayerState.dashing)
        {
            // steal others' votes
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "vote" && collision.GetComponent<Vote>().pickable)
        {
            votes += 1;
            animator.SetTrigger("PickUp");
        }
    }

    protected void votesStolen()
    {
        if (votes == 0) return;
        int lostVotes = 1 + (int)Random.Range(0.0f, 1.0f * MaxVoteLost);

        if (votes < lostVotes) lostVotes = votes;
        votes -= lostVotes;
        for (int i = 0; i < lostVotes; i++)
        {
            GameObject ins = Instantiate(votePrefab, transform, false);
            ins.transform.SetParent(null, true);
            ins.GetComponent<Vote>().Fling(Random.insideUnitCircle * 3);
            /*
            ins.GetComponent<Vote>().SetUnpickable();
            Vector2 v = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * VoteEmittedForce;
            Debug.Log(v);
            ins.GetComponent<Rigidbody2D>().AddForce(v);
            */
        }
    }


    protected IEnumerator dash()
    {
        float timeElapsed = 0;
        State = PlayerState.dashing;
        animator.SetBool("Rush", true);

        while (timeElapsed < DashDuration)
        {
            DashSpeedMultiplier = Mathf.Lerp(DashMaxMultiplier, DashMinMultiplier, timeElapsed / DashDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        DashSpeedMultiplier = 1.0f;
        State = PlayerState.moving;
        animator.SetBool("Rush", false);
    }
}

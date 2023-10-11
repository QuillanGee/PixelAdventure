using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;
    
    public CharacterDatabase charDB;
    private int selectedOption = 0;

    [SerializeField]private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        if(!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
            print("Didn't have key");
        }
        else
        {
            Load();
            print("loaded key");
        }
        UpdateCharacterAnimation(selectedOption);
    }

    //update character method: changes character in database, changes animation
    private void UpdateCharacterAnimation(int selectedOption)
    {
        Character character = charDB.GetCharacter(selectedOption);
        anim.runtimeAnimatorController = character.animatorOverrideController;
    }
    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    
    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }
        
        UpdateAnimationUpdate();
    }

    private void UpdateAnimationUpdate()
    {
        MovementState state; 

        if(dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;

        }
        else if(dirX <0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}

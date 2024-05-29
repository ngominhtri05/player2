using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class move : MonoBehaviour
{
    public Rigidbody2D r;
    public Animator a;
    public int speed = 4;
    public float player_move;
    public bool isFacingRight = true;
    private bool isAttacking = false;

    public Transform groundcheck;
    public LayerMask groundlayer;
    [SerializeField] bool isgrounded;
    Vector2 vecgravity;
    [SerializeField] int jumppower = 30;
    [SerializeField] float fall;
    // Start is called before the first frame update
    void Start()
    {
        vecgravity = new Vector2(0, -Physics2D.gravity.y);
        r = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player_move = Input.GetAxisRaw("Horizontal");
        //run
        if (Input.GetMouseButton(1))
        {
            // Apply increased speed when the right mouse button is held down
            r.velocity = new Vector2(speed * 1.5f * player_move, r.velocity.y);
            a.SetBool("run", true);
        }
        else
        {
            // Normal speed when the right mouse button is not held down
            r.velocity = new Vector2(speed * player_move, r.velocity.y);
            a.SetBool("run", false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            a.SetBool("run", false);
        }
        if(isFacingRight == true && player_move == - 1) {
        transform.localScale = new Vector3(-1,1,1);
        isFacingRight = false;}
        if(isFacingRight == false && player_move == 1) {
        transform.localScale = new Vector3(1,1,1);
        isFacingRight = true;}
        a.SetFloat("move", Mathf.Abs(player_move));
        //randon attack
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            
            if (Random.Range(1, 3) == 1)
            {
                a.SetBool("attack", true);
            }
            else
            {
                a.SetBool("attack2", true);
            }
        }
        AnimatorStateInfo stateInfo = a.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("attack") && stateInfo.normalizedTime >= 1.0f)
        {
            a.SetBool("attack", false);
            isAttacking = false;
        }
        else if (stateInfo.IsName("attack2") && stateInfo.normalizedTime >= 1.0f)
        {
            a.SetBool("attack2", false);
            isAttacking = false;
        }
        //
        isgrounded = Physics2D.OverlapCapsule(groundcheck.position, new Vector2(0.91f,0.3f),CapsuleDirection2D.Horizontal,0,groundlayer);
        if(Input.GetButtonDown("Jump") && isgrounded){
            r.velocity = new Vector2(r.velocity.x, 15);
        }
        if(r.velocity.y < 0){
            r.velocity -= vecgravity * fall * 5 * Time.deltaTime;
        }
        if(isgrounded==false){
            a.SetBool("jump", true);
        }
        if(isgrounded==true){
            a.SetBool("jump", false);
        }
    }
}

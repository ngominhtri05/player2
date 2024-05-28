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
        r.velocity = new Vector2(speed*player_move,r.velocity.y);
        if(isFacingRight == true && player_move == - 1) {
        transform.localScale = new Vector3(-1,1,1);
        isFacingRight = false;}
        if(isFacingRight == false && player_move == 1) {
        transform.localScale = new Vector3(1,1,1);
        isFacingRight = true;}
        a.SetFloat("move", Mathf.Abs(player_move));
        if(Input.GetMouseButtonDown(0)){
            a.SetTrigger("attack");
        }
        isgrounded = Physics2D.OverlapCapsule(groundcheck.position, new Vector2(0.91f,0.21f),CapsuleDirection2D.Horizontal,0,groundlayer);
        if(Input.GetButtonDown("Jump") && isgrounded){
            r.velocity = new Vector2(r.velocity.x, 15);
        }
        if(r.velocity.y < 0){
            r.velocity -= vecgravity * fall * 2 * Time.deltaTime;
        }
        if(isgrounded==false){
            a.SetBool("jump", true);
        }
        if(isgrounded==true){
            a.SetBool("jump", false);
        }
    }
}

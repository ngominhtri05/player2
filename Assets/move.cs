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

    private bool CanDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown =1f;
    [SerializeField] private TrailRenderer tr;
    int stamina = 100;
    // Start is called before the first frame update
    void Start()
    {
        vecgravity = new Vector2(0, -Physics2D.gravity.y);
        r = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDashing){
            return;
        }
        player_move = Input.GetAxisRaw("Horizontal");
        //run
        // if (Input.GetMouseButton(1))
        // {
        //     // Apply increased speed when the right mouse button is held down
        //     r.velocity = new Vector2(speed * 1.5f * player_move, r.velocity.y);
        //     a.SetBool("run", true);
        // }
        // else
        // {
        //     // Normal speed when the right mouse button is not held down
            //  r.velocity = new Vector2(speed * player_move, r.velocity.y);
        //     a.SetBool("run", false);
        // }
        if(Input.GetMouseButton(1) && CanDash){
            StartCoroutine(Dash());
        }
        if(isFacingRight == true && player_move == - 1) {
        transform.localScale = new Vector3(-1,1,1);
        isFacingRight = false;}
        if(isFacingRight == false && player_move == 1) {
        transform.localScale = new Vector3(1,1,1);
        isFacingRight = true;}
        a.SetFloat("move", Mathf.Abs(player_move));
        //randon attack
        if(Input.GetMouseButtonDown(0)){
            if(Random.Range(1,3) == 1){
            a.SetTrigger("attack");
            }
            else {
            a.SetTrigger("attack2");
            }
        }
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
    private void FixedUpdate() {
        if(isDashing){
            return;
        }
        r.velocity = new Vector2(speed * player_move, r.velocity.y);
    }
    private IEnumerator Dash(){
        CanDash = false;
        isDashing = true;
        float org = r.gravityScale;
        r.gravityScale = 0f;
        r.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        r.gravityScale = org;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        CanDash = true;
    }
}

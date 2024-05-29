using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    // Start is called before the first frame update
    public float followspeed = 2;
    public Transform target;
    // Update is called once per frame
    void Update()
    {
        Vector3 newpos = new Vector3(target.position.x,target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newpos, followspeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 6f;
    private UnityEngine.Vector3 Movementdirection;
    private Animator Myanimator;
    //var moveanima : bool = Animator.StringToHas
    private Rigidbody playerrigidbody;
    private float camRayLength = 100f;
    private int floorMask;
    /*void Start()
    {
        
    }*/

    private void Awake(){
        Myanimator = GetComponent<Animator>();
        playerrigidbody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
    }
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;
        if(Physics.Raycast(camRay, out floorHit,camRayLength,floorMask))
        {
            UnityEngine.Vector3 PlayerToMouse = floorHit.point - transform.position;
            PlayerToMouse.y = 0;//不往上看
            Quaternion newRotation = Quaternion.LookRotation(PlayerToMouse);

            playerrigidbody.MoveRotation(newRotation);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
    void Animating(float h, float v)
    {
        bool walking = h != 0 || v != 0;

        Myanimator.SetBool("Iswalking", walking);
        
    }
    private void FixedUpdate()
    {
        //bool Walking = false;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //Walking = true;
        //Myanimator.SetBool("Iswalking", Walking);
        //Debug.Log(h + ':' + v);
       // Myanimator.SetBool(Iswalking);
        PlayerMove(h, v);
        Turning();
        Animating(h, v);
        
    }

    void PlayerMove(float h, float v) //x,z 没有y 没有跳
    {
        Movementdirection.Set(h,0f,v);
        Movementdirection = Movementdirection.normalized * speed * Time.deltaTime;
        playerrigidbody.MovePosition(transform.position + Movementdirection);
    }
}

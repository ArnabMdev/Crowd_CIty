using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    // Use this for initialization
    public float speed = 6.0f;
    float rotatespeed = 1.0f;
    public float jumpSpeed = 8.0f;
 //   public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private float prevRotMag;
    Vector3 pos; 
    Transform thisTrans;
   public Camera cam;

    public Transform target  ;
    float strength = 5;
    public float moveSpeed = 6;

    Rigidbody rb;
    Vector3 velocity;


    //  public DynamicJoystick dynamicJoystick;

    void Start()
    {
        controller = GetComponent<CharacterController>();
      

        // let the gameObject fall down
        thisTrans = transform;

        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

  


    private float zAngle = 0.0f;
  
    private void Update()
    {

        // We are grounded, so recalculate
        // move direction directly from axes

       // Vector3 direction = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;
        moveDirection = new Vector3(0.0f, 0.0f, 1);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
        Vector3 pos = thisTrans.position;
        if (pos.y != 0.0f)

        {
            thisTrans.position = new Vector3(pos.x, 0, pos.z);

        }
        //limiting Y
        if (Application.isEditor)
        {
            // thisTrans.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * 300, 0, Space.World);
        }

        /*Debug.Log(TouchRotateSingle.eulerRotation);*/
        
        
            //  thisTrans.localRotation = Quaternion.LookRotation(TouchRotateSingle.eulerRotation);
            /*
            Vector3 oldAngles = transform.eulerAngles; // save angles
            Vector3 upAxis = new Vector3(0, 0, 1); // rotation axis
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)); // get world coordinates of mouse position

            transform.LookAt(point, upAxis); // rotate the transform around Z axis     
            zAngle = -transform.eulerAngles.z; // save angle of Z axis - negative or positive depends on your camera view direction    
                                               //  transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, zAngle);  // reset rotations, except on Z axis   

            transform.eulerAngles = new Vector3(oldAngles.x, zAngle, oldAngles.y );  // reset rotations, except on Z axis   
            */
            /*
            cam.transform.LookAt(this.thisTrans);
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            Vector3 dirToWorldPoint = (worldPoint - cam.transform.position).normalized;
            cam.transform.rotation = Quaternion.FromToRotation(dirToWorldPoint, cam.transform.forward);
            */
          /*
            float h = Input.mousePosition.x - Screen.width / 2;
            float v = Input.mousePosition.y - Screen.height / 2;
            float angle = -Mathf.Atan2(v, h) * Mathf.Rad2Deg;
            */
          //  Vector2 direction = mousePos.position - player.transform.position;
          //  player.transform.right /* Maybe you need Up or -Up or -right */ = direction;


          //  transform.rotation = Quaternion.Euler(0, angle, 0);


       }

    private void FixedUpdate()
    {
        Vector3 roto = Quaternion.LookRotation(TouchRotateSingle.eulerRotation).eulerAngles;
        if (Mathf.Abs(prevRotMag - roto.magnitude) <= 10f)
        {
            return;
        }
        Debug.Log(transform.localEulerAngles);
        Debug.Log(roto);

        transform.Rotate(roto);
        prevRotMag = roto.magnitude;

    }
    /*
    if (Input.touchCount > 0)
    {
        Vector3 touchPos = Input.GetTouch(0).position;
       // touchPos.y = 0.0f;
        touchPos.x = 0.0f;
        touchPos.z = 0.0f;

        thisTrans.localRotation = Quaternion.EulerAngles(touchPos * rotatespeed *Time.deltaTime);

      //  thisTrans.transform.LookAt(touchPos);


    }

    */
    /*
    //Gather the inputs on which lane we should be
    if (SwipeManager.swipeRight)
    {
        Debug.Log("Sipe Right");

    }
    else if(SwipeManager.swipeLeft)
    {
        Debug.Log("Sipe Left");

    }
    else if(SwipeManager.swipeUp)
    {

        Debug.Log("Sipe Up");
        // Move translation along the object's z-axis
        //    transform.Translate(Vector3.forward * (speed) * Time.deltaTime);
        //   transform.Translate(Vector3.forward * (speed) * Time.deltaTime);
        // speed will be a float variable.
        //  transform.Translate(Vector3.up * Time.deltaTime , Space.World);
      transform.position += transform.forward * Time.deltaTime * speed;


    }
    else if(SwipeManager.swipeDown)
    {
        Debug.Log("Sipe Down");
    }
    */


    // }
    /*
     void Update()
     {
         Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
         transform.LookAt(mousePos + Vector3.right * transform.position.y);
         velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed;


     }

     void FixedUpdate()
     {
         rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
     }

     */




}
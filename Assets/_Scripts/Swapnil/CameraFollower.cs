using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    /*
    public Transform TargetPlayer;
    public Vector3 camera_offset;
    public bool lookat = true;
*/

    private Vector3 targetchords;
    public Transform player;

    public float Turnspeed = 2.0f;
    public Quaternion Turnto;
    [Header(header: "Offset")]
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
     //   camera_offset = transform.position - TargetPlayer.transform.position;
        targetchords = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        // Vector3 newPos = player.transform.position + offset;
        //  transform.position = newPos;


        Turnto = player.transform.rotation;
        transform.position = player.transform.position + offset;
        transform.rotation = Quaternion.Slerp(transform.rotation, Turnto, Time.deltaTime * Turnspeed);



    }

}

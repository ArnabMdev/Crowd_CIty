using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization

    public AnimationController thisAnim;
    public CinemachineFollowZoom cam;
    public GroupData data;
    bool isPlayer = true;
    private float prevCount= 1;

    Transform thisTrans;
    Indicator indicator;
    public PlayerMovement movementScript;

    /************* Multiplayer *************/
    public MoveController multimovementScript;

    public float playerspeed=10.0f;
    public float playerRotationSpeed = 100.0f;

    public bool ismultiplayer;

    


    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;
    }

    private void OnEnable()
    {
        
        
        if (ismultiplayer)
        {
            multimovementScript.enabled = true;
            movementScript.enabled = false;
        }
        else
        {
            movementScript.enabled = false;
        }
        
    
    }
    void Start()
    {
        thisTrans = transform;
        killedBy = "";
    }
    public void SetUp()
    {
        data = GameManager.Instance.data[0];//get data object
        data.groupLeader = this.gameObject;


        /*** Working on Multiplayer Game***/
        if (ismultiplayer)
        {
            ChangeMaterial(data.leaderMaterial);
        }
    }
    public void startPlayer()
    {
        data.indicatorInstance.setUpIndicator(data);
       // if{ MoveController}

        if (ismultiplayer)
        {
            multimovementScript.enabled = true;
            movementScript.enabled = true;
        }
        else
        {
            movementScript.enabled = true;
        }
      


        thisAnim.currentAnimState = 2;

    }
    public void stopPlayer()
    {

        if (ismultiplayer)
        {
            multimovementScript.enabled = false;
            movementScript.enabled = false;
        }
        else
        {
            movementScript.enabled = false;
        }
           


        thisAnim.currentAnimState = 0;
    }
    public void ChangeMaterial(Material mat)
    {
        foreach (var mesh in transform.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            mesh.material = mat;
        }
    }
    // Update is called once per frame
    void Update()
    {

        float translation = Input.GetAxis("Vertical") * playerspeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * playerRotationSpeed * Time.deltaTime;
        
      //  transform.Translate(0, 0, translation);
       // transform.Rotate(0, rotation, 0);
       if(data.GroupCount - prevCount >=20)
       {
           cam.m_Width += 5f;
           prevCount = data.GroupCount;
       }
       if (data.GroupCount - prevCount <= -20)
       {
           cam.m_Width -= 5f;
           prevCount = data.GroupCount;
       }


    }


    void OnTriggerEnter(Collider incoming)
    {
        if (GameManager.isGameEnded) return;
        GameObject IncomingObj = incoming.gameObject;

        if (IncomingObj.tag.Contains("Followers"))
        {
            Follower followInstance = IncomingObj.GetComponent<Follower>();


            if (!followInstance.isFollowingLeader)
            {
                data.GroupCount++;
                followInstance.data = data;
                followInstance.ChangeMaterial();
                data.indicatorInstance.UpdateText();
                IncomingObj.layer = data.PhysicsLayerID;

            }
            else
            {

                if (followInstance.data.groupId != data.groupId)
                {
                    if (followInstance.data.GroupCount < data.GroupCount)
                    {

                        followInstance.data.GroupCount--;
                        followInstance.data.indicatorInstance.UpdateText();

                        followInstance.data = data;
                        data.GroupCount++;
                        data.indicatorInstance.UpdateText();


                        IncomingObj.layer = data.PhysicsLayerID;
                        followInstance.ChangeMaterial();
                    }
                    else if (data.GroupCount == 0)
                    {
                        data.isDead = true;
                        killedBy = followInstance.data.LeaderName;
                        GameManager.Instance.StopGameByPlayerDead();

                    }
                }
            }
        }
    }
    bool isMouseDown = false;
    int directional = -1;
    public static string killedBy;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{





    // Use this for initialization

    NavMeshAgent thisAgent;
    public Transform target;
    public float distanceToTarget;
    Transform thisTrans;

    public bool isFollowingLeader = false;
    Material thisMaterial;
    public GroupData data;
    public AnimationController thisAnimator;

    public Animator[] animators;

    Rigidbody thisRigidbody;
    public static int TotalFollowersCount = 0;

    public int walkingSpeed = 4;
    public int walkingAccel = 4;
    public int FolowingSpeed = 8;
    public int FolowingAccel = 8;

    public particleController particle;

    public GameObject baseModel;
    public GameObject newModel;

    public bool isPlayer = false;
    public GameObject warriorPrefab;
    public string Spawnname;




    void Start()
    {
        isFollowingLeader = false;
        //   if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
        thisAgent = GetComponent<NavMeshAgent>();
        thisTrans = GetComponent<Transform>();

        thisRigidbody = this.GetComponent<Rigidbody>();
        TotalFollowersCount++;
        gameObject.name = "Follower " + TotalFollowersCount;

        if (target == null)
        {
            FindOneNode();
        }

        if (thisAgent.isOnNavMesh) thisAgent.SetDestination(target.position);

        //changing follower material on Start
        foreach (var mesh in transform.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            mesh.material = GameManager.Instance.followerMaterial;
        }


  //      transform.localPosition = new Vector3(0, 0.133f, 0);
    }

    void FindOneNode()
    {
        target = AllNodes.Instance.getOneRandomNode();
    }

    float timeSinceGotLeader;

    public void ChangeMaterial()
    {
        if (data != null)
        {
            particle.PlayParticle(data.groupColor);
        }
       
        thisMaterial = data.leaderMaterial;
        isFollowingLeader = true;
        target = data.groupLeader.transform;
        timeSinceGotLeader = Time.timeSinceLevelLoad;
        StartCoroutine( 
        AceHelper.waitThenCallback(0.7f, () => {
            foreach (var mesh in transform.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (target.gameObject.tag != "Player")
                    mesh.material = thisMaterial;
            }

        }));

        if (target.gameObject.tag == "Player")
        {
            baseModel.SetActive(false);

            if (newModel == null)
            {
                newModel = PoolManager.Instance.Spawn2D(warriorPrefab);
                Spawnname = newModel.name;

                Spawnname = Spawnname.Replace("(Clone)", "");
                newModel.name = Spawnname;

              
                newModel.transform.SetParent(this.transform);
                newModel.SetActive(true);
                newModel.transform.localPosition = new Vector3(0, 0.133f, 0);
                newModel.transform.localScale = new Vector3(2, 2, 2);

            }
            else
            {
               
                newModel.SetActive(true);
            }
            newModel.transform.localPosition = new Vector3(0, 0, 0);
            animators[1] = newModel.GetComponent<Animator>();

            thisAnimator.thisAnimator = animators[1];
            thisAnimator.thisAnimator.SetTrigger("run");


        }
        else
        {
            baseModel.SetActive(true);
            if (newModel != null)
            {

                PoolManager.Instance.Recycle(Spawnname, newModel);
             //   newModel.SetActive(false);


            }


            thisAnimator.thisAnimator = animators[0];
            thisAnimator.thisAnimator.SetTrigger("run");
        }
    }

    float lastSetTime;

    void Update()
    {



        if (GameManager.isGameEnded)
        {

            isFollowingLeader = false;

        }
        if (!isFollowingLeader)
        {
            float distanceDifference = Vector3.Distance(transform.position, target.position);
            if (distanceDifference < 2)
            {
                FindOneNode();
            }
            thisAnimator.currentAnimState = 1;

            if (Time.timeSinceLevelLoad - lastSetTime > 3)
            {
                thisAgent.SetDestination(target.position);
                lastSetTime = Time.timeSinceLevelLoad;
            }

            thisAgent.speed = walkingSpeed;
            thisAgent.acceleration = walkingAccel;
        }
        else
        {

            thisAnimator.currentAnimState = 2;

            thisAgent.speed = FolowingSpeed;
            thisAgent.acceleration = FolowingAccel;

            thisAgent.updateRotation = false;
            thisAgent.SetDestination(target.position);
            lastSetTime = Time.timeSinceLevelLoad;

            thisTrans.rotation = target.rotation;
        }



    }

    private void LateUpdate()
    {
        if (newModel != null)
        {

            newModel.transform.localPosition = new Vector3(0, 0.133f, 0);

            float degrees = 0;
            Vector3 to = new Vector3(degrees, 0, 0);

            newModel.transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
         //   newModel.transform.localRotation = new Vector3(0, 0.133f, 0);

        }
    }


    float lastTime;
    Vector3 lasPos;

    void OnTriggerEnter(Collider incoming)
    {
        if (GameManager.isGameEnded) return;
        GameObject IncomingObj = incoming.gameObject;

        if (IncomingObj.tag.Contains("Followers") && isFollowingLeader && Time.timeSinceLevelLoad - timeSinceGotLeader > 1)
        {
            Follower followInstance = IncomingObj.GetComponent<Follower>();

            if (followInstance.isFollowingLeader)
            {
                if (followInstance.data.groupId != data.groupId)
                {
                    if (followInstance.data.GroupCount < data.GroupCount)
                    {

                        followInstance.data.GroupCount--;

                        if (followInstance.data.indicatorInstance == null)
                        {
                            Debug.Log("follwInstnace " + followInstance.gameObject.name);
                        }

                        followInstance.data.indicatorInstance.UpdateText();

                        followInstance.target = data.groupLeader.transform;
                        followInstance.isFollowingLeader = true;
                        followInstance.data = data;
                        if (newModel == null)
                        {
                            newModel = PoolManager.Instance.Spawn2D(warriorPrefab);
                            newModel.transform.localPosition = new Vector3(0, 0, 0);
                           
                            newModel.transform.SetParent(this.transform);
                            newModel.SetActive(true);
                            Spawnname = newModel.name;

                            Spawnname = Spawnname.Replace("(Clone)", "");
                            newModel.name = Spawnname;
                            newModel.transform.localScale = new Vector3(1, 1, 1);

                        }
                        followInstance.ChangeMaterial();

                        data.GroupCount++;

                        data.indicatorInstance.UpdateText();
                        IncomingObj.layer = data.PhysicsLayerID;
                    }
                }
            }
            else
            {
                followInstance.target = data.groupLeader.transform;
                followInstance.isFollowingLeader = true;
                followInstance.data = data;
                followInstance.ChangeMaterial();

                data.GroupCount++;

                data.indicatorInstance.UpdateText();
                IncomingObj.layer = data.PhysicsLayerID;
            }
        }
    }

    void OnBecameInvisible()
    {
        thisAnimator.enabled = false;
        Debug.Log("Changed Animator to False");
    }
    void OnBecameVisible()
    {
        thisAnimator.enabled = true;
        Debug.Log("Changed Animator to True");
    }


    public void SpawnWarroir()
    {

    }




}

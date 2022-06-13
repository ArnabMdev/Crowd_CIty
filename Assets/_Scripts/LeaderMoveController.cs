﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Colyseus;
using LucidSightTools;

public class LeaderMoveController : ExampleNetworkedEntityView
{

    // Use this for initialization
    public int followingSpeed, followingAccel;
    public TriggerSearch scanTrigger;
    NavMeshAgent thisAgent;
    public int killCount = 0;

    public float distanceToTarget;
    Transform thisTrans;

    public AnimationController animationController;
    public GroupData data; // we grab data from gameMamanger ,no need to assagin 
    Indicator indicator;

    public LeaderStates currentState;
    LeaderStates lastState;
    float TimeStarted;
    public enum LeaderStates
    {
        getFollowers,
        TravelArround,
        AttackOthers,
        Die
    }
    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        thisTrans = GetComponent<Transform>();
        ChangeMaterial();
        currentState = LeaderStates.getFollowers;

        scanTrigger.data = data;
        animationController.currentAnimState = 2;
		killCount = 0;
        TimeStarted = Time.timeSinceLevelLoad;
        StartCoroutine("WaitForConnect");
    }

    IEnumerator WaitForConnect()
    {
        if (ExampleManager.Instance.CurrentUser != null && !IsMine) yield break;

        while (!ExampleManager.Instance.IsInRoom)
        {
            yield return 0;
        }
        LSLog.LogImportant("HAS JOINED ROOM - CREATING ENTITY");
        ExampleManager.CreateNetworkedEntityWithTransform(new Vector3(0f, 0f, 0f), Quaternion.identity, new Dictionary<string, object>() { ["prefab"] = "VMEViewPrefab" }, this, (entity) => {
            LSLog.LogImportant($"Network Entity Ready {entity.id}");
        });
    }
    public override void OnEntityRemoved()
    {
        base.OnEntityRemoved();
        LSLog.LogImportant("REMOVING ENTITY", LSLog.LogColor.lime);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();
        switch (currentState)
        {
            case LeaderStates.getFollowers:
                getFollowers();

                break;
            case LeaderStates.TravelArround:
                break;
            case LeaderStates.AttackOthers:
                break;
            case LeaderStates.Die:
                break;

        }

    }
    void getFollowers()
    {

    }
    void FixedUpdate()
    {
		if (GameManager.isGameEnded) {
			thisAgent.speed = 0;
			thisAgent.acceleration = 0;
			animationController.currentAnimState = 0;
			return;
		}
        if (scanTrigger.SearchedTransform != null)
        {
            distanceToTarget = Vector3.Distance(transform.position, scanTrigger.SearchedTransform.position);
            if (distanceToTarget > 4)
            {

                thisAgent.SetDestination(scanTrigger.SearchedTransform.position);
            }
            else
            {
                thisAgent.SetDestination(GameManager.Instance.data[0].groupLeader.transform.position);
                scanTrigger.StartScan();
            }
        }
        thisAgent.speed = followingSpeed;
        thisAgent.acceleration = followingAccel;




    }

    void OnTriggerEnter(Collider incoming)
    {
        if (GameManager.isGameEnded) return;
        if (Time.timeSinceLevelLoad - TimeStarted < 5) return;
        GameObject IncomingObj = incoming.gameObject;

        if (IncomingObj.tag.Contains("Followers"))
        {
            Follower followInstance = IncomingObj.GetComponent<Follower>();
            if (!followInstance.isFollowingLeader)
            {
                followInstance.target = this.transform;
                followInstance.isFollowingLeader = true;

                followInstance.data = data;
                followInstance.ChangeMaterial();
                data.GroupCount++;
                data.indicatorInstance.UpdateText();
                IncomingObj.layer = data.PhysicsLayerID;
            }
            else
            {
                if (followInstance.data.groupId != data.groupId)
                {
                    if (data.GroupCount > followInstance.data.GroupCount)
                    {
                        followInstance.data.GroupCount--;
                        followInstance.data.indicatorInstance.UpdateText();

                        followInstance.data = data;
                        data.GroupCount++;
                        data.indicatorInstance.UpdateText();
                        followInstance.target = data.groupLeader.transform;
                        followInstance.isFollowingLeader = true;
                        followInstance.ChangeMaterial();
                        IncomingObj.layer = data.PhysicsLayerID;
                    }
                    else if (data.GroupCount < followInstance.data.GroupCount)
                    {
                        if (data.GroupCount <= 0)
                        {
                            data.isDead = true;
                            data.indicatorInstance.OnDeath();
                            gameObject.SetActive(false);
                            followInstance.data.KillCount++;
								 

                        }
                    }
                }
            }
        }
    }


    public void ChangeMaterial()
    {
        foreach (var mesh in transform.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            mesh.material = data.leaderMaterial;
            data.groupColor = mesh.material.GetColor("_Color");
        }

    }

	
}

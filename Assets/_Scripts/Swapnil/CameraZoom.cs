using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{


    public TextMeshProUGUI CamText;
    // Use this for initialization
    public Transform target;
    public Vector2 remapDistance;
    public Vector3 positionOffset;
    Vector3 targetPos;

    public string followingName;
    GroupData data;
    public float distance;
    Transform thisTrans;
    


    // Start is called before the first frame update
    void Start()
    {
        thisTrans = transform;
    //    this.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 50F;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            data = GameManager.Instance.data[0];

            //   data = GameManager.Instance.data;

            target = data.groupLeader.transform;

        };
        //    CamText.text = "ObjectCount " + OnScreenObjsCount.ObjectCount;
        if (target == null)
        {
            Debug.Log("Player Not Found");
        }

        // if (data != null)
        // {
        //  distance = data.GroupCount;
        distance = GameManager.Instance.data[0].GroupCount;

            // distance = data.GroupCount;
            //   distance = ((float)OnScreenObjsCount.ObjectCount).RemapClamped(0, 100, remapDistance.x, remapDistance.y);

        //            positionOffset = new Vector3(0, distance, -distance * 1f);

        if (distance < 5)
            {
                this.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 40f;
            }
            else if(distance < 15)
            {

                this.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 45F;
            }
        else if (distance < 25)
        {
            this.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 50F;
        }
        else if (distance < 35)
        {
            this.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 55F;
        }
        else if (distance < 45)
        {
            this.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 60F;
        }
        else if (distance < 100)
        {
            this.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 70F;
        }else
         {
            this.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 90F;
        }
        //}
        // targetPos = target.position + positionOffset;
        // thisTrans.position = Vector3.Lerp(thisTrans.position, targetPos, Time.deltaTime * 20);







#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            List<GroupData> allData = GameManager.Instance.data;

            target = allData[Random.Range(0, allData.Count - 1)].groupLeader.transform;
            followingName = target.name;
        }

#endif
    }

    private void LateUpdate()
    {

       
    }
}

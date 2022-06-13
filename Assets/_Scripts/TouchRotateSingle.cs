using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TouchRotateSingle : MonoBehaviour, IBeginDragHandler, IDragHandler
{

    // Use this for initialization
    public void RotateMesh(Vector3 dir, bool withoutCheck = false)
    {
       
       // this.meshInstance.localRotation = Quaternion.LookRotation(dir);
    }


     float maxDistanceInInch =0.3f;

    /** Touch Original ***/
  //  public float maxDistanceInInch = 0.3f;
    private float dpi;
    private Vector2 initialPosition;
    public static Vector3 eulerRotation;
    private Quaternion pov;
   // public Transform PlayerTransform;
    private void Start()
    {
        if ((double)this.dpi != 0.0 && !float.IsNaN(this.dpi))
            return;
        this.dpi = 96f;
      //  PlayerTransform = GameManager.Instance.data[0].groupLeader.GetComponent<Transform>();
    }


   

    public void OnBeginDrag(PointerEventData data)
    {
        
        this.initialPosition = data.position;
    }

    public void OnDrag(PointerEventData data)
    {

        /*
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 1f);
        Vector3 screenTouch = screenCenter + new Vector3(eventData.delta.x, eventData.delta.y, 0f);

        Vector3 worldCenterPosition = Camera.main.ScreenToWorldPoint(screenCenter);
        Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(screenTouch);

        Vector3 dir = worldTouchPosition - worldCenterPosition;

        eulerRotation = dir;
        */

        
        Vector2 vector2_1 = (data.position - this.initialPosition) / this.dpi;
        Vector2 vector2_2 = vector2_1.normalized * Mathf.Min(vector2_1.magnitude, this.maxDistanceInInch);
        Vector3 dir = this.pov * new Vector3(vector2_2.x / this.maxDistanceInInch, 0.0f, vector2_2.y / this.maxDistanceInInch);


        eulerRotation = dir.normalized;
       // Debug.Log("Mouse was Dragged " + dir);
       
    }

    public void OnRelease(PointerEventData data)
    {
        
    }
}

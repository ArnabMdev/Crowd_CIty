using UnityEngine;
using System.Collections;

public class RoadnObstaclesGenerator : MonoBehaviour 
{

	GameObject obj;
	private string resourceName1;
	private float delay;
	private int randomPath;
	private int pathNumber=1;
	private float incr = 0.1f;
	public float pathLength;

	
	[HideInInspector]
	public bool flag = true;
	// Use this for initialization
	void Start () {

				
		for (int i =0; i < 1; i++)
		{
			resourceName1 = "Road/" + i; 
			obj = PoolManager.Instance.Spawn (resourceName1);
			obj.transform.position = transform.position;
			PoolManager.Instance.Recycle(resourceName1,obj);
		}
	}
	//
	void Update()
	{
		/*if (Time.fixedTime % 15 == 0 && PathProp.speed > -4f ) 
		{
			
			PathProp.speed -= incr;
			ObstacleProperty.nonRigidBodySpeed -= incr;
			
			if(Time.fixedTime % 50 == 0 && anim.speed < 1.4f)
			{
				anim.speed += 0.1f; 
			}
		}*/
	/*	if ( PathProp.speed < 0 && flag) {
			flag = false;
			delay = pathLength /(-PathProp.speed);
			randomPath = Random.Range (0, pathNumber);		
			resourceName1 = "Road/" + randomPath;
			obj = PoolManager.Instance.Spawn (resourceName1);
			obj.transform.position = transform.position;
			obj.transform.rotation = Quaternion.identity;
			
			StartCoroutine (WaitPath ());
		} */
	}
	IEnumerator WaitPath()
	{
		yield return new WaitForSeconds (delay);
		flag = true;
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// A pool of objects that can be reused.
public class ObjectPool {

	#region PRIVATE VARIABLES
	private Queue<GameObject> pool;
	private GameObject prefab;
	private string prefabName;

	private Transform parent;
	#endregion


	#region PUBLIC METHODS
	// Create a new object pool.


	public ObjectPool(string _prefabName)
	{
	
		pool = new Queue<GameObject>();
		prefabName = _prefabName;
		parent = new GameObject(prefabName + " Pool").transform;
//		parent.parent = GameObject.Find ("GameSceneRoot").transform;

	}

	// Spawn an object from the pool.
	public GameObject Spawn()
	{
		GameObject obj;

		if (pool.Count > 0)
			obj = pool.Dequeue();
		else
		{
			obj = GameObject.Instantiate(Resources.Load(prefabName))as GameObject ;
			obj.transform.parent = parent;
		}
		obj.SetActive(true);
		return obj;
	}

	public GameObject Spawn2D()
	{
		GameObject obj;
		
		if (pool.Count > 0)
			obj = pool.Dequeue();
		else
		{
			obj = GameObject.Instantiate(Resources.Load(prefabName))as GameObject ;
			//obj.transform.parent = parent;
		}
		obj.SetActive(true);
		return obj;
	}

	public GameObject Spawn2D(GameObject gameObj)
	{
		GameObject obj;
		
		if (pool.Count > 0)
			obj = pool.Dequeue();
		else
		{
			obj = GameObject.Instantiate(gameObj)as GameObject ;
			//obj.transform.parent = parent;
		}
		obj.SetActive(true);
		return obj;
	}
		
	// Spawn an object from the pool at specific position.
	public GameObject SpawnAtPosition(Vector3 pos)
	{
		GameObject obj = Spawn();
		obj.transform.position = pos;
		return obj;
	}

	// Recycle an object back into the pool.
	public void Recycle(GameObject obj)
	{
		pool.Enqueue(obj);
		obj.SetActive(false);
	}
	#endregion
}

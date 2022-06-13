using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// A struct for objects to be pooled.
[System.Serializable]
public class ObjectToPool
{
	//public GameObject prefab;
	public int initialCapacity;
}

// Singleton for managing pools of different objects.
public class PoolManager : MonoBehaviour {

	#region PUBLIC VARIABLES
	// Objects to be pooled at initialization.
	public ObjectToPool[] prefabsToPool;
	#endregion

	#region PRIVATE VARIABLES
	private Dictionary<string, ObjectPool> pools;
	#endregion

	#region SINGLETON PATTERN
	public static PoolManager _instance;
	
	public static PoolManager Instance
	{
		get {
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<PoolManager>();
				
				if (_instance == null)
				{
					GameObject container = new GameObject("PoolManager");
//					container.transform.parent = GameObject.Find("GameSceneRoot").transform;
					_instance = container.AddComponent<PoolManager>();
				}
			}
			
			return _instance;
		}
	}
	#endregion



	#region PUBLIC METHODS

	/*void Awake()
	{
		if(PoolManager.Instance != null)
		{
			DontDestroyOnLoad(gameObject);
		}
	}*/

	// Create a new pool of objects at runtime.
	public void CreatePool(string prefabName)
	{
		if (pools == null)
			pools = new Dictionary<string, ObjectPool>();

		ObjectPool newPool = new ObjectPool(prefabName);
		pools.Add(prefabName, newPool);
	}

	// Spawn an object with the given name.
	public GameObject Spawn(string prefabName)
	{
		if (pools == null || !pools.ContainsKey(prefabName))
			CreatePool (prefabName);

		return pools[prefabName].Spawn();
	}

	public GameObject Spawn2D(string prefabName)
	{
		if (pools == null || !pools.ContainsKey(prefabName))
			CreatePool (prefabName);
		
		return pools[prefabName].Spawn2D();
	}

	public GameObject Spawn2D(GameObject gameObj)
	{
		if (pools == null || !pools.ContainsKey(gameObj.name))
			CreatePool (gameObj.name);
		
		return pools[gameObj.name].Spawn2D(gameObj);
	}

	// Spawn an object with the given name and position.
	public GameObject SpawnAtPosition(string prefabName, Vector3 pos)
	{
		if (!pools.ContainsKey(prefabName))
			return null;
		
		return pools[prefabName].SpawnAtPosition(pos);
	}

	// Recycle an object with the given name.
	public void Recycle(string prefabName, GameObject obj)
	{

		if(prefabName==null || prefabName.Equals(""))
		{
			return;
		}

	
		if (!pools.ContainsKey(prefabName))
			return;

		pools[prefabName].Recycle(obj);
	}





	#endregion
}

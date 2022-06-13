using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectWithPoolManger : MonoBehaviour
{

    /**Prefab which you want Spawn **/
    public GameObject[] SpawnPrefabObject;

    /* Prefab Where you want to Spawn Parent Object */ 
    public GameObject ParentGameObject;

    /* Private Object which Spawn from Parent */
    private GameObject SpawnBulletObject;


    public int HowManySpawn= 3;
     int[] SpeedOfSpawn = {6,14, 18} ;

    public bool IsStartEnemy;


   // public bool IsGameOver;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartPoolingObject()
    {
       /*
         for (int i = 0; i < HowManySpawn; i++)
        {
        */
            SpawnBulletObject = PoolManager.Instance.Spawn2D(SpawnPrefabObject[Random.Range(0, SpawnPrefabObject.Length)]);
            //  SpawnBulletObject.transform.SetParent(ParentGameObject.transform);
            Vector3 ParentPos = new Vector3(ParentGameObject.transform.position.x, ParentGameObject.transform.position.y, 0);
            SpawnBulletObject.transform.position = ParentPos;
       // }


    }


    IEnumerator SpawnEnemy()
    {
        int GetNum = Random.Range(0, SpeedOfSpawn.Length);

        /*
        if (IsStartEnemy && !GameManager.Instance.IS_GAME_OVER)
        {
            StartPoolingObject();

            yield return new WaitForSeconds(SpeedOfSpawn[GetNum]);
            //spawnBullet();
            StartCoroutine("SpawnEnemy");
        }
        */

        if (IsStartEnemy)
        {
            StartPoolingObject();

            yield return new WaitForSeconds(SpeedOfSpawn[GetNum]);
            //spawnBullet();
            StartCoroutine("SpawnEnemy");
        }

    }









}

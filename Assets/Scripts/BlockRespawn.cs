using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BlockRespawn : MonoBehaviour
{

    public float respawnTimeInitial;
    public float respawnTime;
    public List<GameObject> blockPrefabs = new List<GameObject>();
    private GameObject blockTypeSelected;
    private GameObject blockInstantiated;
    public List<Vector3> spawningPoints = new List<Vector3>();
    public Vector2 randomPos;
    public float scanRadius;
    public LayerMask layerBlocks;
    private Collider2D checkCollider;
    public GameObject gameController;
    public int maxBlocksOnScreen;
    public bool canSpawn = false;
    public bool mouseOnScreen = false;


    void Start()
    {
        //Set initial countdown
        respawnTime = respawnTimeInitial;

        foreach (Transform child in transform)
        {
            spawningPoints.Add(child.position);
        }

    }

    void Update()
    {
        //Countdown starts
        respawnTime -= Time.deltaTime;

        //Update number of blocks on screen, to see if it can spawn or not
        if (gameController.GetComponent<GenControl>().blocksOnScreen < maxBlocksOnScreen)
        {
            canSpawn = true;
        }
        else canSpawn = false;

        //Spawn block
        if (canSpawn == true && respawnTime <= 0)
        {
            //Selection of block hold according to progress in level
            if (gameController.GetComponent<GenControl>().blocksDestroyed >= 3 && gameController.GetComponent<GenControl>().blocksHoldOnScreen <= 0)
            {
                blockTypeSelected = blockPrefabs[1];
                gameController.GetComponent<GenControl>().blocksHoldOnScreen += 1;
            }
            else if (gameController.GetComponent<GenControl>().blocksDestroyed >= 6 && gameController.GetComponent<GenControl>().blocksHoldOnScreen == 1)
            {
                blockTypeSelected = blockPrefabs[1];
                gameController.GetComponent<GenControl>().blocksHoldOnScreen += 1;
            }
            else if (gameController.GetComponent<GenControl>().blocksDestroyed >= 9 && gameController.GetComponent<GenControl>().blocksHoldOnScreen == 2)
            {
                blockTypeSelected = blockPrefabs[1];
                gameController.GetComponent<GenControl>().blocksHoldOnScreen += 1;
            }
            //Instantiate mouse
            else if (gameController.GetComponent<GenControl>().blocksHoldOnScreen == 3 && mouseOnScreen == false)
            {
                blockTypeSelected = blockPrefabs[2];
                mouseOnScreen = true;
            }
            else if (gameController.GetComponent<GenControl>().blocksDestroyed >= 12 && gameController.GetComponent<GenControl>().blocksHoldOnScreen == 3)
            {
                blockTypeSelected = blockPrefabs[1];
                gameController.GetComponent<GenControl>().blocksHoldOnScreen += 1;
            }
            else
            {
                blockTypeSelected = blockPrefabs[0];
            }


            //Select random location within the spawning area
            int randomNumber = Random.Range(0, spawningPoints.Count);
            randomPos = spawningPoints[randomNumber];

            //Check if in that position, there is already a block (collider)
            checkCollider = Physics2D.OverlapCircle(randomPos, scanRadius, layerBlocks);

            if (checkCollider != null)
            {
                //Restart countdown
                Debug.Log("Hit!");
                respawnTime = 0;
            }
            else
            {
                //Instantiate the object with the selected type in the selected location
                blockInstantiated = Instantiate(blockTypeSelected, randomPos, Quaternion.identity);
            }

            //Reset Respawn time to initial value, to start the countdown again
            respawnTime = respawnTimeInitial;
        }

        if (canSpawn == false)
        {
            Debug.Log("Too many blocks");
        }
    }







}

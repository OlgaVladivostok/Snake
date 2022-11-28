using UnityEngine;
using System.Collections.Generic;

public class FoodManager : MonoBehaviour
{

    [Header("Snake Manager")]
    SnakeMovement SM;

    [Header("Food Variables")]
    public GameObject FoodPrefab;
    public int appearanceFrequency;

    [Header("Time to spawn Management")]
    public float timeBetweenFoodSpawn;
    private float thisTime;

    //
    public List<Vector3> SimpleFoodPositions = new List<Vector3>();
    //

    void Start()
    {


        SM = GameObject.FindGameObjectWithTag("SnakeManager").GetComponent<SnakeMovement>();


        SpawnFood();
    }


    void Update()
    {

        if (GameController.gameState == GameController.GameState.GAME)
        {
            if (thisTime < timeBetweenFoodSpawn)
            {
                thisTime += Time.deltaTime;
            }
            else
            {
                SpawnFood();
                thisTime = 0;
            }
        }
    }

    public void SpawnFood()
    {
        float screenWidthWorldPos = Camera.main.orthographicSize * Screen.width / Screen.height;
        float distBetweenBlocks = screenWidthWorldPos / 5;


        for (int i = -2; i < 3; i++)
        {


            float x = 2 * i * distBetweenBlocks;
            float y = 0;

            if (SM.transform.childCount > 0)
            {
                y = (int)SM.transform.GetChild(0).position.y + distBetweenBlocks * 2 + 10;
            }

            Vector3 SpawnPos = new Vector3(x, y, 0);


            int number;

            if (appearanceFrequency < 100)
                number = Random.Range(0, 100 - appearanceFrequency);
            else
                number = 1;


            GameObject boxInstance;

            if (number == 1)
            //
            {
                SimpleFoodPositions.Add(SpawnPos);
                boxInstance = Instantiate(FoodPrefab, SpawnPos, Quaternion.identity, transform);
                boxInstance.name = "Food";
                boxInstance.tag = "Food";
                boxInstance.layer = LayerMask.NameToLayer("Default");

                boxInstance.AddComponent<Rigidbody>();
                boxInstance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            }


            //  boxInstance = Instantiate(FoodPrefab, spawnPos, Quaternion.identity, transform);



            // Set this part as the parent of the Text Mesh
            //   FoodAmountTextMesh.transform.parent = boxInstance;

            //Place it correctly
            //  PartsAmountTextMesh.transform.position = newPart.position +
            //   new Vector3(0, 0.5f, 0);

        }
    }
}

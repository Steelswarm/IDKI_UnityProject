using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public float collectibleAmount;
    public float minX;
    public float maxX;
    public float minZ;
	public float maxZ;
	public float minY;
    
	private List<Vector3> randomPosList;
	
	public float minimumCollectibleDistance = 1f;


	public Terrain MyTerrain;
	private TerrainData MyTerrainData;
        
	// Start is called before the first frame update
	void Start()
	{
		
		
				
		MyTerrain = GameObject.Find("Terrain").GetComponent<Terrain>();
		MyTerrainData = MyTerrain.terrainData;
		
		randomPosList = new List<Vector3>();
		for (int i = 0; i < collectibleAmount; i++)
		{
			createNewCollectible();
		}
		

		
		for(int x = 0; x < randomPosList.Count; x++)
		{
			Instantiate(collectiblePrefab, randomPosList[x], collectiblePrefab.transform.rotation);
		}
		
	}
    
    
    

	private Vector3 GenerateSpawnPosition() //Generates random position Vector3
    {
        float spawnPosX = Random.Range(minX, maxX);
	    float spawnPosZ = Random.Range(minZ, maxZ);

		
	    //Debug.Log("TERRAIN HEIGHT IS: " + MyTerrainData.GetHeight((int)spawnPosX,(int)spawnPosZ));
	    float terrainHeight = MyTerrainData.GetHeight((int)spawnPosX,(int)spawnPosZ);
	    //Debug.Log(MyTerrainData.GetHeight(0,0));
	    Vector3 randomPos = new Vector3(spawnPosX, terrainHeight, spawnPosZ);
        //Debug.Log(randomPos);
        return randomPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    
    
    
	private void CheckifCoordsValid(List<Vector3> vectorList, Vector3 vector) //Checks if the given vector is far enough away from the others to avoid objects spawning too close
	{
		if(vectorList.Count.Equals(0)) //If the list is empty instantly adds the coordinate
		{
			vectorList.Add(vector);
		}
		else //If not, checks if the coordinate is not too close to existing ones before adding it
		{
			for(int i = 0; i < vectorList.Count; i++)
			{
				if((vectorList[i] - vector).magnitude < minimumCollectibleDistance) 
					//ATTENTION: This creates a stack overflow if the spawn area and minimum distance are not configured correctly. 
					//Further development to fix this would be required such as running this condition X times before it gives up and Logs in console 
					//"Unable to find suitable coordinate"
				{
					createNewCollectible();
					return;
				}
			}
			
			vectorList.Add(vector);
		}
		
		
	}
	
	
	private void createNewCollectible() //Checks if the random coordinates are valid 
	{
		
		Vector3 randomPos = GenerateSpawnPosition();
		CheckifCoordsValid(randomPosList, randomPos);
		//Instantiate(collectiblePrefab, GenerateSpawnPosition(), collectiblePrefab.transform.rotation);
		
	}
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

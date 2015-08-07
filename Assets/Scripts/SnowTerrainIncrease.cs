using UnityEngine;
using System.Collections;

public class SnowTerrainIncrease : MonoBehaviour {

	public int increaseRate = 2;
	public int increaseCount = 28;
	public float increaseValue = 0.001f;

	private Terrain snowTerrain;
	private TerrainData snowTerrainData;
	private int rateTimer = 0;

	void Start () {

		snowTerrain = transform.GetChild (0).GetComponent<Terrain> ();
		snowTerrainData = snowTerrain.terrainData;

	}

	void FixedUpdate () {
	
		rateTimer++;
		if(rateTimer > increaseRate) {
			rateTimer = 0;

			for(int i=0; i<increaseCount; i++) {

				int x = Random.Range(0, snowTerrainData.heightmapWidth);
				int z = Random.Range(0, snowTerrainData.heightmapWidth);

				float[,] heights = snowTerrainData.GetHeights(x, z, 1, 1);
				
				heights[0, 0] = heights[0, 0] + increaseValue;

				snowTerrainData.SetHeights(x, z, heights);
			}

			snowTerrain.Flush();
		}
	}
}

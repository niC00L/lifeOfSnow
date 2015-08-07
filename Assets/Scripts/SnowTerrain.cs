using UnityEngine;
using System.Collections;

public class SnowTerrain : MonoBehaviour {
	
	public float baseHeightAdjustment = 1.0f;

	public Texture2D snowTexture;
	
	private TerrainData baseTerrainData;

	private Terrain snowTerrain;
	private TerrainData snowTerrainData;

	void Awake() {

		Terrain baseTerrain = this.GetComponent<Terrain> ();

		baseTerrainData = baseTerrain.terrainData;

		///create new snow terrain data
		snowTerrainData = new TerrainData();
		snowTerrainData.heightmapResolution = baseTerrainData.heightmapResolution;
		snowTerrainData.baseMapResolution = baseTerrainData.baseMapResolution;
		snowTerrainData.size = new Vector3 (baseTerrainData.size.x, baseTerrainData.size.y, baseTerrainData.size.z);
		snowTerrainData.SetDetailResolution (1024, 8);

		//copy heights from base terrain

		float[,] heights = baseTerrainData.GetHeights (0, 0, baseTerrainData.heightmapWidth, baseTerrainData.heightmapHeight);

		snowTerrainData.SetHeights (0, 0, heights);

		//add snow terrain to game
		GameObject snowTerrainObject = new GameObject ();

		snowTerrainObject.name = "SnowTerrainLayer";

		snowTerrainObject.AddComponent<Terrain>();
		snowTerrain = snowTerrainObject.GetComponent<Terrain> ();
		snowTerrainObject.AddComponent<TerrainCollider>();
		TerrainCollider snowTerrainCollider = snowTerrainObject.GetComponent<TerrainCollider> ();
		snowTerrain.terrainData = snowTerrainData;
		snowTerrainCollider.terrainData = snowTerrainData;
		
		snowTerrainObject.transform.position = baseTerrain.transform.position + new Vector3 (0, baseHeightAdjustment, 0);
		snowTerrainObject.transform.parent = baseTerrain.transform;
		//$$$$$$$$$$$$$$$$$$$$$$$$$$$$snowTerrain.materialTemplate = baseTerrain.materialTemplate;

		//setup snow terrain texture


		SplatPrototype[] spt = new SplatPrototype[1];

		spt[0] = new SplatPrototype ();
		spt[0].texture = snowTexture;
		spt[0].tileSize = new Vector2(1, 1);

		snowTerrainData.splatPrototypes = spt;

	}

	public bool snowballIntersects(Vector3 pos, float size) {

		int dsize = Mathf.CeilToInt(size);

		Vector2 gp = globalPositionToTerrain(pos);

		if(gp.x + dsize < 0 || gp.y + dsize < 0)
			return false;
		if(gp.x - dsize >= snowTerrainData.heightmapWidth || gp.y - dsize >= snowTerrainData.heightmapHeight)
			return false;
		
		return true;
	}

	public Vector2 globalPositionToTerrain(Vector3 pos) {
		Vector3 tpos = transform.position;

		int x = Mathf.RoundToInt ((pos.x - tpos.x) / snowTerrainData.size.x * snowTerrainData.heightmapWidth);
		int z = Mathf.RoundToInt ((pos.z - tpos.z) / snowTerrainData.size.z * snowTerrainData.heightmapHeight);

		return new Vector2 (x, z);
	}

	private TerrainModificationPosition trimPositonToTerrainSize (Vector2 terrainPos, int size) {
		return new TerrainModificationPosition(terrainPos, baseTerrainData, size);
	}

	public float decreaseSnow(Vector3 position, float size, float snowTerrainDecreasment) {

		float volume = 0;
		
		Vector2 terrainPos = globalPositionToTerrain (position);

		int sizeRad = Mathf.CeilToInt(size);

		TerrainModificationPosition tpos = trimPositonToTerrainSize (terrainPos, sizeRad);
		
		float[,] heights = snowTerrainData.GetHeights(tpos.x, tpos.y, tpos.w, tpos.h);
		bool change = false;
		
		for(int i=0; i<tpos.w; i++) {
			for(int j=0; j<tpos.h; j++) {
				
				int fx = i - tpos.cx;
				int fz = j - tpos.cy;
				float realDistance = Mathf.Sqrt(fx*fx+fz*fz);
				
				if(realDistance <= size) { //if there even is snowball (in corners of square there is not)

					float relativeDistance = realDistance/size;

					/*

					float realTerrainHeight = heights[i, j] * snowTerrainData.size.y + snowTerrain.transform.position.y;

					float ballHeightAt = position.y - ( size * Mathf.Sqrt(1-(relativeDistance*relativeDistance)) );

					float heightDiff = realTerrainHeight - ballHeightAt + snowTerrainDecreasment;


					Vector3 debugStart = position;
					debugStart.y = ballHeightAt;
					Vector3 debugEnd = position;
					debugEnd.y = realTerrainHeight;

					debugStart.x += fx;
					debugStart.z += fz;
					debugEnd.x += fx;
					debugEnd.z += fz;

					Debug.DrawLine(debugStart, debugEnd);


					if(heightDiff > 0) {

						heights[i, j] = heights[i, j] - heightDiff/50;
						volume += heightDiff;

						change = true;

					}

					*/

					float heightDiff = snowTerrainDecreasment * ( Mathf.Sqrt(1-(relativeDistance*relativeDistance)) ) * Mathf.Pow(size, 1/3);

					heights[i, j] = heights[i, j] - heightDiff;

					volume += heightDiff;
					change = true;
				}
			}
		}

		if(change) {
		
			snowTerrainData.SetHeights (tpos.x, tpos.y, heights);
		
			snowTerrain.Flush();
		}

		return volume;
	}
}

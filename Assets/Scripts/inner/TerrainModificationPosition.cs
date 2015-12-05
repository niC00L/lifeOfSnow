using UnityEngine;

public class TerrainModificationPosition {

	public int cx;
	public int cy;
	public int x;
	public int y;
	public int w;
	public int h;

	public TerrainModificationPosition(Vector2 terrainPos, TerrainData terrainData, int size) {
		
		this.cx = size;
		this.cy = size;
		this.x = (int)terrainPos.x - size;
		this.y = (int)terrainPos.y - size;
		this.w = size * 2;
		this.h = size * 2;

		if(x < 0) {
			int d = 0-x;
			x += d;
			cx -= d;
			w -= d;
		}

		if(y < 0) {
			int d = 0-y;
			y += d;
			cy -= d;
			h -= d;
		}

		if(x+w >= terrainData.heightmapWidth) {
			int d = (x+w)-terrainData.heightmapWidth;
            
			w -= d;
		}

		if(y+h >= terrainData.heightmapHeight) {
			int d = (y+h)-terrainData.heightmapHeight;
			
			h -= d;
		}
	}
}
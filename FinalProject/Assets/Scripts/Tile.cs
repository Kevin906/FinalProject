using UnityEngine;

public class Tile
{
	public Vector2Int gridPos;       // tọa độ trong grid
	public Vector3 worldPos;         // vị trí trong scene
	public Edge[] edges = new Edge[4];  // 4 cạnh (trái, phải, trên, dưới)
	public Vertex[] vertices = new Vertex[4]; // 4 đỉnh (góc)

	public bool buildable = true;
}

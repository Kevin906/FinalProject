using UnityEngine;

public class Edge
{
	public Tile tileA, tileB;   // hai tile chia sẻ cạnh này
	public Vertex[] vertices = new Vertex[2]; // 2 đỉnh của cạnh
	public bool hasWall = false; // có tường không

	public Vector3 GetWorldPos()
	{
		if (tileA != null && tileB != null)
		{
			return (tileA.worldPos + tileB.worldPos) / 2f;
		}
		return Vector3.zero;
	}
}

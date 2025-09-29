using UnityEngine;

public class Grid : MonoBehaviour
{
	[Header("Grid Settings")]
	public int width = 5;
	public int height = 5;
	public float cellSize = 2f;

	[Header("Prefabs")]
	public GameObject tilePrefab;
	public GameObject wallPrefab;

	private Tile[,] tiles;
	private Edge[,] verticalEdges;   // cạnh dọc
	private Edge[,] horizontalEdges; // cạnh ngang
	private Tile startTile = null;
	private Tile endTile = null;

	void Start()
	{
		GenerateGrid();
	}

	void GenerateGrid()
	{
		tiles = new Tile[width, height];
		verticalEdges = new Edge[width + 1, height];
		horizontalEdges = new Edge[width, height + 1];

		// --- Tạo tiles ---
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Vector3 pos = new Vector3(x * cellSize, 0, y * cellSize);
				GameObject tileGO = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
				tileGO.name = $"Tile_{x}_{y}";

				Tile t = new Tile
				{
					gridPos = new Vector2Int(x, y),
					worldPos = pos,
				};
				tiles[x, y] = t;
			}
		}

		// --- Tạo edges ---
		for (int x = 0; x < width + 1; x++)
		{
			for (int y = 0; y < height; y++)
			{
				verticalEdges[x, y] = new Edge();
			}
		}
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height + 1; y++)
			{
				horizontalEdges[x, y] = new Edge();
			}
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			BuildWallAtMouse();
		}
	}

	void BuildWallAtMouse()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			Tile clickedTile = FindNearestTile(hit.point);
			if (clickedTile != null)
			{
				// Ví dụ: xây tường ở cạnh phía trên tile
				int x = clickedTile.gridPos.x;
				int y = clickedTile.gridPos.y;

				if (y + 1 < height + 1)
				{
					Edge edge = horizontalEdges[x, y + 1];
					if (!edge.hasWall)
					{
						Vector3 wallPos = clickedTile.worldPos + new Vector3(0, 0, cellSize / 2f);
						Instantiate(wallPrefab, wallPos, Quaternion.identity, transform);
						edge.hasWall = true;
					}
				}
			}
		}
	}

	Tile FindNearestTile(Vector3 point)
	{
		int x = Mathf.RoundToInt(point.x / cellSize);
		int y = Mathf.RoundToInt(point.z / cellSize);

		if (x >= 0 && x < width && y >= 0 && y < height)
		{
			return tiles[x, y];
		}
		return null;
	}
}

using UnityEngine;

public class CameraMoving : MonoBehaviour
{
	[Header("Pan Settings")]
	public float panSpeed = 20f;       // tốc độ kéo
	public float panBorderThickness = 10f;
	public float dragSpeed = 2f;

	[Header("Zoom Settings")]
	public float scrollSpeed = 5f;     // tốc độ zoom
	public float minY = 10f;           // zoom gần nhất
	public float maxY = 60f;           // zoom xa nhất

	private Vector3 dragOrigin;

	void Update()
	{
		HandleMovement();
		HandleZoom();
	}

	void HandleMovement()
	{
		// Di chuyển bằng phím WASD
		Vector3 pos = transform.position;

		if (Input.GetKey("w")) pos.z += panSpeed * Time.deltaTime;
		if (Input.GetKey("s")) pos.z -= panSpeed * Time.deltaTime;
		if (Input.GetKey("d")) pos.x += panSpeed * Time.deltaTime;
		if (Input.GetKey("a")) pos.x -= panSpeed * Time.deltaTime;

		// Kéo bằng chuột giữa (Middle Mouse Button)
		if (Input.GetMouseButtonDown(2))  // bấm nút giữa
		{
			dragOrigin = Input.mousePosition;
			return;
		}

		if (Input.GetMouseButton(2))  // giữ nút giữa
		{
			Vector3 difference = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3(-difference.x * dragSpeed, 0, -difference.y * dragSpeed);

			transform.Translate(move, Space.World);
			dragOrigin = Input.mousePosition;
		}

		transform.position = pos;
	}

	void HandleZoom()
	{
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		Vector3 pos = transform.position;

		pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime; // zoom theo trục Y
		pos.y = Mathf.Clamp(pos.y, minY, maxY);

		transform.position = pos;
	}
}

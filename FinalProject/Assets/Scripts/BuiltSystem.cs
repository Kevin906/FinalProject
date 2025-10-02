using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public GameObject ghostBeamPrefab;
    public GameObject woodBeamPrefab;

    private Node startNode;
    private GameObject ghostBeam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Node node = GetNodeUnderMouse();
            if (node != null)
            {
                startNode = node;
                ghostBeam = Instantiate(ghostBeamPrefab);
            }
        }

        if (Input.GetMouseButton(0) && startNode != null)
        {
            Vector3 mousePos = GetMouseWorldPosition();
            UpdateGhostBeam(ghostBeam.transform, startNode.Position, mousePos);
        }

        if (Input.GetMouseButtonUp(0) && startNode != null)
        {
            Node endNode = GetNodeUnderMouse();
            if (endNode != null && endNode != startNode)
            {
                GameObject beamObj = Instantiate(woodBeamPrefab);
                beamObj.GetComponent<Beam>().Init(startNode, endNode);
            }
            Destroy(ghostBeam);
            ghostBeam = null;
            startNode = null;
        }
    }

    void UpdateGhostBeam(Transform beam, Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        beam.position = start + dir / 2f;
        beam.rotation = Quaternion.LookRotation(dir);
        beam.localScale = new Vector3(0.2f, 0.2f, dir.magnitude);
    }

    Node GetNodeUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.collider.GetComponent<Node>();
        }
        return null;
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }
}

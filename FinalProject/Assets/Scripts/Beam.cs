using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public Node startNode;
    public Node endNode;

    public void Init(Node start, Node end)
    {
        startNode = start;
        endNode = end;

        // Cập nhật vị trí beam
        Vector3 dir = end.Position - start.Position;
        transform.position = start.Position + dir / 2f;
        transform.rotation = Quaternion.LookRotation(dir);
        transform.localScale = new Vector3(0.2f, 0.2f, dir.magnitude); // scale theo chiều dài
    }
}

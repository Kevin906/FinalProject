using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FortsLike/BlockType")]
public class BlockType : ScriptableObject
{
    public string blockName;
    public GameObject prefab;
    public float costMetal;
    public float costEnergy;
    public float buildTime;
    public float health;
    public BlockPhysicsType physicsType;
}

public enum BlockPhysicsType
{
    Node,
    Beam,
    Bracing,
    Device
}

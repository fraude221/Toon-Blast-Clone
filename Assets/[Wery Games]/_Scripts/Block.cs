using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector]
    public int colorID;
    [HideInInspector]
    public Vector2 index;
    [HideInInspector]
    public Material material;

    private void OnMouseDown()
    {
        GameManager.Instance.blockHandler.PopRelatedBlocks(this);
    }
}

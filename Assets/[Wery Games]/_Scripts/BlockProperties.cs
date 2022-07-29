using UnityEngine;

[System.Serializable]
public class BlockProperties
{
    public BlockType _blockType;
    public Color[] _blockColors;
    public GameObject _cubePrefab, _hexagonPrefab;
    public float _blockHeight, _blockWidth;

    public Vector3 GetBlockScale()
    {
        return new Vector3(_blockWidth, _blockHeight, _blockHeight);
    }
}
public enum BlockType
{
    Cube,
    Hexagon
}
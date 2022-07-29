using UnityEngine;

[System.Serializable]
public class ContainerProperties
{
    public int columnNumber;
    public int rowNumber;
    public float columnGap;
    public float rowGap;

    public bool IsIndexExist(int x, int y) => (x >= 0 && x < columnNumber) && (y >= 0 && y < rowNumber);
}

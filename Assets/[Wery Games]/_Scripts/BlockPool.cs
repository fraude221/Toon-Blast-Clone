using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    private Block[][] _pooledObjects; // [color number][amount to pool]

    public void GeneratePool(GameObject objectToPool, int poolSize, BlockProperties blockProperties)
    {
        _pooledObjects = new Block[blockProperties._blockColors.Length][];
        for (int i = 0; i < _pooledObjects.Length; i++)
        {
            _pooledObjects[i] = new Block[poolSize];

            Material blockMaterial = new Material(Shader.Find("Standard"));
            blockMaterial.color = blockProperties._blockColors[i];
            blockMaterial.enableInstancing = true;

            GameObject tmp;
            for (int j = 0; j < poolSize; j++)
            {
                tmp = Instantiate(objectToPool, transform);
                tmp.transform.localScale = blockProperties.GetBlockScale();
                tmp.GetComponent<Renderer>().material = blockMaterial;
                Block block = tmp.GetComponent<Block>();
                block.material = blockMaterial;
                block.colorID = i;
                tmp.SetActive(false);
                _pooledObjects[i][j] = block;
            }
        }
    }

    public Block GetBlock()
    {
        int randomColorIndex = Random.Range(0, _pooledObjects.Length);

        foreach (Block block in _pooledObjects[randomColorIndex])
        {
            if (!block.gameObject.activeInHierarchy)
            {
                block.gameObject.SetActive(true);
                return block;
            }
        }
        return null;
    }
}
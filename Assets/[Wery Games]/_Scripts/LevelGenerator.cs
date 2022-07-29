using UnityEngine;

public static class LevelGenerator
{
    public static void GenerateContainer(ContainerProperties containerProperties)
    {
        GameManager.Instance.blockContainer = new Block[containerProperties.columnNumber][];
        for (int i = 0; i < GameManager.Instance.blockContainer.Length; i++)
        {
            GameManager.Instance.blockContainer[i] = new Block[containerProperties.rowNumber];
        }
    }
    public static void FillContainer(BlockProperties blockProperties, ContainerProperties containerProperties)
    {
        if (blockProperties._blockType == BlockType.Cube)
        {
            FillContainerWithCubes(blockProperties, containerProperties);
        }
        else if (blockProperties._blockType == BlockType.Hexagon)
        {
            FillContainerWithHexagons(blockProperties, containerProperties);
        }
    }

    private static void FillContainerWithCubes(BlockProperties blockProperties, ContainerProperties containerProperties)
    {
        GeneratePool(blockProperties._cubePrefab, blockProperties, containerProperties);

        for (var i = 0; i < containerProperties.columnNumber; i++)
        {
            for (var j = 0; j < containerProperties.rowNumber; j++)
            {
                Block newBlock = GameManager.Instance.blockPool.GetBlock();

                float blockSizeX = newBlock.GetComponent<Renderer>().bounds.size.x;
                float blockSizeY = newBlock.GetComponent<Renderer>().bounds.size.y;
                float positionX = (i * blockSizeX) + (i * containerProperties.columnGap);
                float positionY = (j * blockSizeY) + (j * containerProperties.rowGap);
                newBlock.transform.position = new Vector3(positionX, positionY, 0);

                newBlock.index = new Vector2(i, j);

                GameManager.Instance.blockContainer[i][j] = newBlock;
            }
        }
    }
    private static void FillContainerWithHexagons(BlockProperties blockProperties, ContainerProperties containerProperties)
    {
        GeneratePool(blockProperties._hexagonPrefab, blockProperties, containerProperties);

        for (var i = 0; i < containerProperties.columnNumber; i++)
        {
            for (var j = 0; j < containerProperties.rowNumber; j++)
            {
                Block newBlock = GameManager.Instance.blockPool.GetBlock();

                float blockSizeX = newBlock.GetComponent<Renderer>().bounds.size.x;
                float blockSizeY = newBlock.GetComponent<Renderer>().bounds.size.y;
                float positionX = (i * blockSizeX) - ((j % 2) * (blockSizeX / 2)) + (i * containerProperties.columnGap);
                float positionY = (j * blockSizeY * 3 / 4) + (j * containerProperties.rowGap);
                newBlock.transform.position = new Vector3(positionX, positionY, 0);

                newBlock.index = new Vector2(i, j);

                GameManager.Instance.blockContainer[i][j] = newBlock;
            }
        }
    }

    private static void GeneratePool(GameObject objectToPool, BlockProperties blockProperties, ContainerProperties containerProperties)
    {
        GameManager.Instance.blockPool.GeneratePool(objectToPool, containerProperties.columnNumber * containerProperties.rowNumber, blockProperties);
    }
}

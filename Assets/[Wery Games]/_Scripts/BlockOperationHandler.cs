using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOperationHandler : MonoBehaviour
{
    [SerializeField] private int _minBlockNumberToPop;

    [Header("Pop Animation")]
    [SerializeField] private float _scaleFactor;
    [SerializeField] private float _animationDuration;

    private bool isAnimationContinues;

    private void Start()
    {
        isAnimationContinues = false;
    }

    public void PopRelatedBlocks(Block clickedBlock)
    {
        if (!isAnimationContinues)
        {
            List<Block> relatedBlocks = GetListOfMatchingSurroundingBlocks(clickedBlock);

            if (relatedBlocks.Count >= _minBlockNumberToPop)
            {
                UIManager.Instance.UpdateScore(clickedBlock.colorID, relatedBlocks.Count);

                foreach (Block block in relatedBlocks)
                {
                    ApplyPopAnimation(block);
                }
            }
        }
    }
    private void ApplyPopAnimation(Block block)
    {
        isAnimationContinues = true;

        Vector3 firstScale = block.transform.localScale;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(block.transform.DOScale(block.transform.localScale * _scaleFactor, _animationDuration / 4));
        sequence.Join(block.GetComponent<Renderer>().material.DOColor(Color.white, _animationDuration / 4));

        sequence.Append(block.transform.DOScale(firstScale, _animationDuration / 4));
        sequence.Join(block.GetComponent<Renderer>().material.DOColor(block.material.color, _animationDuration / 4));

        sequence.OnComplete(() =>
        {
            ReplaceBlockWithNewOne(block);
        });
    }
    private void ApplyFillAnimation(Block block)
    {
        block.transform.localScale = Vector3.zero;
        block.transform.DOScale(GameManager.Instance.blockProperties.GetBlockScale(), _animationDuration / 2).OnComplete(() => { isAnimationContinues = false; });
    }
    private void ReplaceBlockWithNewOne(Block block)
    {
        FillBlock(block);
        RemoveOldBlock(block);
    }
    private void FillBlock(Block previousBlock)
    {
        Block newBlock = GameManager.Instance.blockPool.GetBlock();
        newBlock.transform.position = previousBlock.transform.position;
        newBlock.index = new Vector2(previousBlock.index.x, previousBlock.index.y);
        GameManager.Instance.blockContainer[(int)previousBlock.index.x][(int)previousBlock.index.y] = newBlock;

        ApplyFillAnimation(newBlock);
    }
    private void RemoveOldBlock(Block oldBlock)
    {
        oldBlock.transform.localScale = GameManager.Instance.blockProperties.GetBlockScale();
        oldBlock.GetComponent<Renderer>().material = oldBlock.material;
        oldBlock.gameObject.SetActive(false);
        oldBlock.transform.position = Vector3.zero;
    }

    private List<Block> matchingBlocks;
    private List<Block> GetListOfMatchingSurroundingBlocks(Block clickedBlock)
    {
        matchingBlocks = new List<Block>();

        AddMatchingSurroundingBlocksToList(clickedBlock);

        return matchingBlocks;
    }
    private void AddMatchingSurroundingBlocksToList(Block blockToCheck)
    {
        matchingBlocks.Add(blockToCheck);

        int x = (int)blockToCheck.index.x;
        int y = (int)blockToCheck.index.y;

        CheckBlock(x + 1, y); // UP
        CheckBlock(x - 1, y); // DOWN
        CheckBlock(x, y + 1); // RIGHT
        CheckBlock(x, y - 1); // LEFT

        if (GameManager.Instance.blockProperties._blockType == BlockType.Hexagon)
        {
            if (y%2 == 0) // If left row
            {
                CheckBlock(x + 1, y + 1); // UP RIGHT
                CheckBlock(x + 1, y - 1); // DOWN RIGHT
            }
            else // If right row
            {
                CheckBlock(x - 1, y + 1); // UP LEFT
                CheckBlock(x - 1, y - 1); // DOWN LEFT
            }
        }

        void CheckBlock(int x, int y)
        {
            if (IsIndexExist(x, y))
            {
                Block selectedBlock = GameManager.Instance.blockContainer[x][y];
                if (AreBlocksMatch(selectedBlock, blockToCheck))
                {
                    AddMatchingSurroundingBlocksToList(selectedBlock);
                }
            }
        }
    }

    private bool IsIndexExist(int x, int y) => GameManager.Instance.containerProperties.IsIndexExist(x, y);
    private bool AreBlocksMatch(Block blockToCheck, Block mainBlock) => blockToCheck != null && !matchingBlocks.Contains(blockToCheck) && blockToCheck.colorID.Equals(mainBlock.colorID);
}

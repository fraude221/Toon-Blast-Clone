using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _scoreHolderHexagonPrefab;
    [SerializeField] private GameObject _scoreHolderCubePrefab;
    [SerializeField] private Transform _scorePanel;

    private TextMeshProUGUI[] _scoreTexts;
    private int[] _scores;

    private void Start()
    {
        Color[] colorList = GameManager.Instance.blockProperties._blockColors;
        _scoreTexts = new TextMeshProUGUI[colorList.Length];
        _scores = new int[colorList.Length];

        GenerateScoreHolders(colorList);
    }

    private void GenerateScoreHolders(Color[] colorList)
    {
        for (int i = 0; i < colorList.Length; i++)
        {
            GameObject newScoreHolder;
            if (GameManager.Instance.blockProperties._blockType == BlockType.Cube)
            {
                newScoreHolder = Instantiate(_scoreHolderCubePrefab, _scorePanel);
            }
            else
            {
                newScoreHolder = Instantiate(_scoreHolderHexagonPrefab, _scorePanel);
            }

            newScoreHolder.GetComponent<Image>().color = colorList[i];
            _scoreTexts[i] = newScoreHolder.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void UpdateScore(int colorID, int amount)
    {
        _scores[colorID] += amount;
        _scoreTexts[colorID].text = _scores[colorID].ToString();
    }
}

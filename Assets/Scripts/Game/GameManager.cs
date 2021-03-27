using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void BlockFallDelegate();
    public BlockFallDelegate blockFallen;
    public delegate void LineCompleteDelegate(float posY);
    public LineCompleteDelegate LineComplete;
    public delegate void GameOverDelegate(int score);
    public GameOverDelegate gameOver;
    public delegate void ScoreUpdatedDelegate(int score);
    public ScoreUpdatedDelegate scoreUpdated;

    [SerializeField]
    private int _arenaWidth = 9;
    [SerializeField]
    private float _maxHeight = 5.5f;
    [SerializeField]
    private int _scoreForLine = 1000;


    private Vector2 _arenaPosition = Vector2.zero;
    private float _linecastLength = 0;

    // Array to hold results of linecast used to determine number of blocks in line.
    private RaycastHit2D[] _blocksInLineContainer = null;

    private BlockController _activeBlock = null;

    private int _score = 0;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("BlockSpawner").GetComponent<BlockSpawner>().blockSpawn += OnBlockSpawned;
        
        _blocksInLineContainer = new RaycastHit2D[_arenaWidth];
        _arenaPosition = GameObject.FindGameObjectWithTag("Arena").transform.position;

        _linecastLength = (float)(_arenaWidth - 1) / 2;
    }

    //End game.
    private void GameOver()
    {
        gameOver?.Invoke(_score);
        if (!PlayerPrefs.HasKey("highScore") || _score > PlayerPrefs.GetInt("highScore"))
            PlayerPrefs.SetInt("highScore", _score);
    }

    // Check if line on height is completed.
    private void CheckLine(float height)
    {
        int blockCount = Physics2D.LinecastNonAlloc(new Vector2(-_linecastLength + _arenaPosition.x, height), new Vector2(_linecastLength + _arenaPosition.x, height), _blocksInLineContainer);
        if (blockCount >= _arenaWidth)
        {
            LineComplete?.Invoke(height);
            _score += _scoreForLine;
            scoreUpdated?.Invoke(_score);
        }
    }

    private void OnBlockSpawned(BlockController block)
    {
        _activeBlock = block;
        _activeBlock.blockFallen += OnBlockFallen;
    }


    private void OnBlockFallen(BlockController block)
    {
        if (block == null)
            return;

        List<float> heights = new List<float>();
        for (var i = 0; i < block.children.Count; i++)
            heights.Add(block.children[i].transform.position.y);
        // Lines are removed from top to bottom.
        heights.Sort((h1, h2) => h2.CompareTo(h1));

        if (heights[0] >= _maxHeight)
        {
            GameOver();
            return;
        }

        foreach (var height in heights)
            CheckLine(height);

        if (block == _activeBlock)
            blockFallen?.Invoke();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public delegate void BlockSpawnDelegate(BlockController block);
    public BlockSpawnDelegate blockSpawn;

    public delegate void NextBlockSetDelegate(GameObject block);
    public NextBlockSetDelegate nextBlockSet;

    //List of possible blocks
    [SerializeField]
    private GameObject[] _blocks = null;

    [Header("Difficulty Settings")]
    [SerializeField]
    private float[] _spawnDelayPerDifficulty = null;
    [SerializeField]
    private float[] _fallSpeedPerDifficulty = null;

    private GameObject _nextBlock = null;

    private float _spawnDelay = 0.5f;
    private float _blockFallSpeed = 0.5f;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().blockFallen += OnBlockFallen;
    }

    private void Start()
    {
        _spawnDelay = _spawnDelayPerDifficulty[PlayerPrefs.GetInt("difficulty", 1)];
        _blockFallSpeed = _fallSpeedPerDifficulty[PlayerPrefs.GetInt("difficulty", 1)];

        _nextBlock = Instantiate(_blocks[Random.Range(0, _blocks.Length)], transform.position, transform.rotation);
        SpawnBlock();
    }

    private void SpawnBlock()
    {
        _nextBlock.transform.position = transform.position;
        _nextBlock.GetComponent<BlockController>().SetActive();
        _nextBlock.GetComponent<BlockController>().fallTime = _blockFallSpeed;

        blockSpawn?.Invoke(_nextBlock.GetComponent<BlockController>());
        ChoseNextBlock();
    }

    private void ChoseNextBlock()
    {
        _nextBlock = Instantiate(_blocks[Random.Range(0, _blocks.Length)], transform.position, transform.rotation);
        nextBlockSet?.Invoke(_nextBlock);
    }

    private void OnBlockFallen()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(_spawnDelay);
        SpawnBlock();
    }


}

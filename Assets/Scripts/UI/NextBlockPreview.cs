using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBlockPreview : MonoBehaviour
{
    [SerializeField]
    private Transform _blockPreviewPosition = null;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("BlockSpawner").GetComponent<BlockSpawner>().nextBlockSet += OnNextBlockSet;
    }


    private void OnNextBlockSet(GameObject block)
    {
        block.transform.position = Camera.main.ScreenToWorldPoint(_blockPreviewPosition.position) + Vector3.forward * 20;
    }


}

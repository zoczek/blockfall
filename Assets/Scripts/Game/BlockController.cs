using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public delegate void BlockFallDelegate(BlockController blockController);
    public BlockFallDelegate blockFallen;

    public List<GameObject> children = new List<GameObject>();
    public float fallTime = 0.5f;

    private Controls _controls = null;
    private Transform _pivot = null;

    Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow };


    private float _currentFallTime = 0.0f;
    private bool _active = false;

    private void Start()
    {
        _controls = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controls>();

        Color color = colors[Random.Range(0, colors.Length)];

        for (var i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.CompareTag("BlockPart"))
            {
                children.Add(transform.GetChild(i).gameObject);
                children[i].GetComponent<SpriteRenderer>().color = color;
            }
            else
                _pivot = child.transform;
        }
    }


    void Update()
    {
        if (_active)
        {
            Fall();
            Move();
            Rotate();
        }
    }


    public void SetActive()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().LineComplete += OnLineComplete;

        _active = true;
    }


    // Move one unit down checking for collsions. 
    private void Fall()
    {
        _currentFallTime += Time.deltaTime;

        if (_currentFallTime >= fallTime)
        {
            _currentFallTime = 0;

            Vector2 oldPosition = transform.position;
            transform.position += Vector3.down;
            if (IsColliding())
            {
                transform.position = oldPosition;
                blockFallen?.Invoke(this);

                _active = false;
                foreach (var child in children)
                    child.layer = 0;
            }
        }
    }


    private void Move()
    {
        if (_controls.actionStates["left"] == Controls.KeyState.just_pressed)
            TryToMove(Vector2.left);
        else if (_controls.actionStates["right"] == Controls.KeyState.just_pressed)

            TryToMove(Vector2.right);
    }

    private void Rotate()
    {
        if (_controls.actionStates["rotate"] == Controls.KeyState.just_pressed)
            TryToRotate();
    }

    // Try to move block checking for collisions.
    private void TryToMove(Vector3 direction)
    {
        Vector2 oldPosition = transform.position;
        transform.position += direction; 
        
        if (IsColliding())
            transform.position = oldPosition;
    }

    // Try to rotate block checking for collisons.
    private void TryToRotate()
    {
        Quaternion oldRotation = transform.rotation;
        Vector3 oldPosition = transform.position;

        transform.RotateAround(_pivot.position, Vector3.forward, 90);

        if (IsColliding())
        {
            transform.rotation = oldRotation;
            transform.position = oldPosition;
        }
    }

    // Return true if collision detected.
    bool IsColliding()
    {
        foreach (var child in children)
            if (Physics2D.OverlapPointNonAlloc(child.transform.position, new Collider2D[1], ~LayerMask.GetMask("MovingBlock")) > 0)
                return true;
        
        return false;
    }

    // Destroy all parts of block present on completed line.
    // height is pos Y of the line.
    // If no parts are left destroy this block.
    void OnLineComplete(float height)
    {
        List<GameObject> destroyed = new List<GameObject>();

        foreach (var child in children)
        {
            if (Mathf.Approximately(child.transform.position.y, height))
            {
                destroyed.Add(child);
                Destroy(child);
            }
            else if (child.transform.position.y > height)
                child.transform.position += Vector3.down;
        }

        foreach (var dst in destroyed)
        {
            children.Remove(dst);
        }

        if (children.Count == 0)
            Destroy(this);
    }
}
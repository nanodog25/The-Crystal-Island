using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines the borders of ‘Player’s’ movement. Depending on the chosen handling type, it moves the ‘Player’ together with the pointer.
/// </summary>

[System.Serializable]
public class Borders
{
    [Tooltip("offset from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour {

    [Tooltip("offset from viewport borders for player's movement")]
    public Borders borders;
    Camera mainCamera;
    public bool IsControlActive = true;
    private Rigidbody2D _rb;
    private bool _lessControl = false;
    private bool _noControl = false;
    private bool _ahhh = false;

    public static PlayerMoving instance; //unique instance of the script for easy access to the script

    private void Awake()
    {
        if (instance == null)
            instance = this;

        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ResizeBorders();                //setting 'Player's' moving borders deending on Viewport's size
    }

    private void Update()
    {
        if (IsControlActive)
        {
            Vector3 move;
            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");
            move.z = 0;

            if (!_lessControl)
            {
                transform.position += move * Time.deltaTime * 20;
            }
            else if (!_noControl)
            {
                transform.position += move * Time.deltaTime * 20;
                _rb.AddForce(new Vector2(move.x * 2, move.y * 2));
            }
            else if (!_ahhh)
            {
                transform.position += move * Time.deltaTime * 15;
                _rb.AddForce(new Vector2(move.x * 2, move.y * 2));
            }
            else
            {
                float xPower = _rb.velocity.normalized.x == move.x ? 2 : 10;
                float yPower = _rb.velocity.normalized.y == move.y ? 2 : 10;
                _rb.AddForce(new Vector2(move.x * xPower, move.y * yPower));
            }
        }
        transform.position = new Vector3    //if 'Player' crossed the movement borders, returning him back 
        (
            Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
            Mathf.Clamp(transform.position.y, borders.minY, borders.maxY),
            0
        );
    }

    private void ResizeBorders() 
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset;
    }

    public void ReduceControl()
    {
        if (!_lessControl)
            _lessControl = true;
        else if (!_noControl)
            _noControl = true;
        else
            _ahhh = true;
    }
}

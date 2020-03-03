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
#if UNITY_STANDALONE || UNITY_EDITOR    //if the current platform is not mobile, setting mouse handling 

            //if (Input.GetMouseButton(0)) //if mouse button was pressed       
            //{
            //    Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); //calculating mouse position in the worldspace
            //    mousePosition.z = transform.position.z;
            //    transform.position = Vector3.MoveTowards(transform.position, mousePosition, 30 * Time.deltaTime);
            //}
            //else
            {
                Vector2 move;
                move.x = Input.GetAxis("Horizontal");
                move.y = Input.GetAxis("Vertical");

                float xPower = _rb.velocity.normalized.x == move.x ? 2 : 10;
                float yPower = _rb.velocity.normalized.y == move.y ? 2 : 10;
                _rb.AddForce(new Vector2(move.x * xPower, move.y * yPower));
#endif
            }
        }
        transform.position = new Vector3    //if 'Player' crossed the movement borders, returning him back 
        (
            Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
            Mathf.Clamp(transform.position.y, borders.minY, borders.maxY),
            0
        );
    }

    void ResizeBorders() 
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset;
    }
}

﻿using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool IsTravelStraight { get; set; }
    public bool IsFromLeft { get; set; }
    public float Speed { get; set; }

    private void Update()
    {
        if (IsTravelStraight)
        {
            transform.position += (new Vector3(IsFromLeft ? 2 : -2, -2f) * Speed * Time.deltaTime);
        }

        if (transform.position.y > Screen.height + 10)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.Instance.OnHit(transform.position);
        }
    }
}

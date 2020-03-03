using UnityEngine;

public class ScanningObj : MonoBehaviour
{
    private bool _isTakingReading = false;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") 
        {
            _isTakingReading = true;
        }
    }

    private void Update()
    {
        transform.position -= new Vector3(0, 1.2f) * Time.deltaTime;

        if (_isTakingReading)
        {
            transform.localScale += new Vector3(100f, 100f) * Time.deltaTime;
            if (transform.localScale.x > 300)
                Destroy(gameObject);
        }
    }
}

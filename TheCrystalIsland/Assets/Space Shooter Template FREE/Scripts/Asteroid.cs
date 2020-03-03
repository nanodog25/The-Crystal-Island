using UnityEngine;

namespace Assets.Space_Shooter_Template_FREE.Scripts
{
    public class Asteroid : MonoBehaviour
    {
        private float _speed;

        private void Awake()
        {
            _speed = Random.Range(1.5f, 3);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Projectile")
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            transform.position -= new Vector3(0, _speed) * Time.deltaTime;
        }
    }
}

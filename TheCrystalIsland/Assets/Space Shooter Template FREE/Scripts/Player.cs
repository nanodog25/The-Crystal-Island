using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject HitFX;

    public static Player Instance;
    private Rigidbody2D _rb;
    private float _hitCoolDown = 1f;
    private float _time;
    private int _hits;
    private ParticleSystem[] _hitEffects = new ParticleSystem[3];

    private void Awake()
    {
        if (Instance == null) 
            Instance = this;

        _rb = GetComponent<Rigidbody2D>();
        var damageFX = GetComponentsInChildren<ParticleSystem>();
        _hitEffects[0] = damageFX[0];
        _hitEffects[1] = damageFX[3];
        _hitEffects[2] = damageFX[4];
    }

    private void Update()
    {
        if (_time != 0)
        {
            _time -= Time.deltaTime;
            if (_time < 0)
            {
                _time = 0;
                GetComponent<PlayerMoving>().IsControlActive = true;
            }
        }
    }

    public void OnHit(Vector3 position)
    {
        if (_time == 0)
        {
            _time = _hitCoolDown;
            GetComponent<PlayerMoving>().IsControlActive = false;
            Vector3 direction = transform.position - position;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = 0;
            _rb.AddForce(direction * 50);
            Instantiate(HitFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
            _hits++;

            if (_hits == 3)
            {
                _hitEffects[1].Play();
            }
            else if (_hits == 5)
            {
                _hitEffects[2].Play();
            }
            else if (_hits == 8)
            {
                _hitEffects[0].Play();
            }
        }
        //change sprite
    }    
}

















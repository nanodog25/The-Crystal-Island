using Assets.Space_Shooter_Template_FREE.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class LevelController : MonoBehaviour {

    //Serializable classes implements
    Camera mainCamera;
    public GameObject ScanningObj;
    public GameObject Asteroid;
    public GameObject CrystalBarrage;
    public PlayableDirector EndCutscene;
    private float _time;
    private float _totalTime = 200;

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(AsteroidCreation());
        StartCoroutine(CrystalCreation());
        StartCoroutine(PowerupBonusCreation());
        StartCoroutine(PlayEndcutscene());
    }

    private void Update()
    {
        _time += Time.deltaTime;
    }

    IEnumerator AsteroidCreation()
    {
        while (_time < _totalTime)
        {
            float nextTime;
            if (_time < 50)
                nextTime = Random.Range(2f, 4f);
            else if (_time < 100)
                nextTime = Random.Range(1f, 2f);
            else if (_time < 150)
                nextTime = Random.Range(0.2f, 0.5f);
            else
                nextTime = _totalTime;

            yield return new WaitForSeconds(nextTime);
            var a = Instantiate(
                Asteroid,
                new Vector2(
                    Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
                    mainCamera.ViewportToWorldPoint(Vector2.up).y + ScanningObj.GetComponent<Renderer>().bounds.size.y / 2),
                Quaternion.identity
                );
            float scale = Random.Range(0.3f, 1f);
            a.transform.localScale = new Vector3(scale, scale, 1);
        }
    }

    IEnumerator CrystalCreation()
    {
        while (_time < _totalTime)
        {
            float nextTime;
            int count;
            float speed;
            float SpaceBetween;
            if (_time < 50)
            {
                nextTime = 50;
                count = Random.Range(3, 5);
                speed = Random.Range(3, 5);
                SpaceBetween = Random.Range(5f, 10f);
            }
            else if (_time < 100)
            {
                nextTime = Random.Range(5f, 10f);
                count = Random.Range(3, 5);
                speed = Random.Range(3, 5);
                SpaceBetween = Random.Range(5f, 10f);
            }
            else if (_time < 150)
            {
                nextTime = Random.Range(10f, 15f);
                count = 1;
                speed = Random.Range(3, 5);
                SpaceBetween = 0;
            }
            else
            {
                nextTime = Random.Range(1f, 3f);
                count = Random.Range(12, 20);
                speed = Random.Range(10, 15);
                SpaceBetween = Random.Range(1f, 2f);
            }

            yield return new WaitForSeconds(nextTime);
            var g = Instantiate(CrystalBarrage);
            var cb = g.GetComponent<CrystalBarrage>();
            cb.count = count;
            cb.speed = speed;
            cb.SpaceBetween = SpaceBetween;
            cb.IsFromLeft = Random.Range(0, 2) == 0;

        }
    }

    IEnumerator PowerupBonusCreation()
    {
        while (_time < _totalTime)
        {
            if (_time > 100)
                yield return new WaitForSeconds(_totalTime);

            yield return new WaitForSeconds(25);
            Instantiate(
                ScanningObj,
                //Set the position for the new bonus: for X-axis - random position between the borders of 'Player's' movement; for Y-axis - right above the upper screen border 
                new Vector2(
                    Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
                    mainCamera.ViewportToWorldPoint(Vector2.up).y + ScanningObj.GetComponent<Renderer>().bounds.size.y / 2),
                Quaternion.identity
                );
        }
    }

    IEnumerator PlayEndcutscene()
    {
        yield return new WaitForSeconds(_totalTime);
        EndCutscene.Play();
    }
}

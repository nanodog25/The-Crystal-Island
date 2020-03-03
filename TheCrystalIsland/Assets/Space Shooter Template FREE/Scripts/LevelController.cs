using System.Collections;
using UnityEngine;

#region Serializable classes
[System.Serializable]
public class EnemyWaves 
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

#endregion

public class LevelController : MonoBehaviour {

    //Serializable classes implements
    public EnemyWaves[] enemyWaves; 
    Camera mainCamera;
    public GameObject ScanningObj;
    public float TimeForScanningObject;

    private void Start()
    {
        mainCamera = Camera.main;
        //for each element in 'enemyWaves' array creating coroutine which generates the wave
        for (int i = 0; i<enemyWaves.Length; i++) 
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave));
        }
        StartCoroutine(PowerupBonusCreation());
    }

    //Create a new wave after a delay
    IEnumerator CreateEnemyWave(float delay, GameObject Wave) 
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.Instance != null)
            Instantiate(Wave);
    }

    IEnumerator PowerupBonusCreation()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeForScanningObject);
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
}

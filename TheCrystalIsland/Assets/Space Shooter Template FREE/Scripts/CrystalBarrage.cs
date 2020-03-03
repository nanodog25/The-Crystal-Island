using System;
using System.Collections;
using UnityEngine;

namespace Assets.Space_Shooter_Template_FREE.Scripts
{
    [Serializable]
    public class CrystalBarrage : MonoBehaviour
    {
        [Tooltip("Enemy's prefab")]
        public GameObject enemy;

        [Tooltip("a number of enemies in the wave")]
        public int count;

        [Tooltip("path passage speed")]
        public float speed;

        [Tooltip("time between emerging of the enemies in the wave")]
        public float timeBetween;

        public float SpaceBetween;

        public bool IsFromLeft;

        private void Start()
        {
            StartCoroutine(CreateEnemyWave());
        }

        IEnumerator CreateEnemyWave() //depending on chosed parameters generating enemies and defining their parameters
        {
            for (int i = 0; i < count; i++)
            {
                GameObject newEnemy;
                newEnemy = Instantiate(enemy, new Vector3(IsFromLeft ? -15 : 15, 10 + i * SpaceBetween), Quaternion.Euler(0, 0, IsFromLeft ? 45 : -45));
                float scale = UnityEngine.Random.Range(0.3f, 1f);
                newEnemy.transform.localScale = new Vector3(0.3f, scale, 1);
                Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
                enemyComponent.IsTravelStraight = true;
                enemyComponent.IsFromLeft = IsFromLeft;
                enemyComponent.Speed = speed;
                newEnemy.SetActive(true);
                yield return new WaitForSeconds(timeBetween);
            }
            Destroy(gameObject);
        }
    }
}

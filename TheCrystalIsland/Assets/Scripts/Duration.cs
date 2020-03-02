using UnityEngine;

namespace Assets.Scripts
{
    public class Duration : MonoBehaviour
    {
        private float _time;
        private float _duration;

        public void Set(float duration)
        {
            _duration = duration;
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time > _duration)
                Destroy(gameObject);
        }
    }
}

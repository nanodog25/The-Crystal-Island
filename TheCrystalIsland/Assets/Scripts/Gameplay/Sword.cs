using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Sword : MonoBehaviour
    {
        public SwordState State;
        public float RotationSpeed;
        public float MovementSpeed;
        private Vector3 _direction;

        private void Start()
        {
            State = SwordState.Windup;
        }

        public void Throw()
        {
            State = SwordState.ThrownElsewhere;

            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            _direction = (mousePos - objectPos).normalized;
            _direction.z = 0;
        }

        private void Update()
        {
            if (State == SwordState.Windup)
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
                mousePos.x = mousePos.x - objectPos.x;
                mousePos.y = mousePos.y - objectPos.y;

                float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else if (State == SwordState.ThrownElsewhere)
            {
                transform.position += _direction * Time.deltaTime * MovementSpeed;
                transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
            }
        }
    }
}

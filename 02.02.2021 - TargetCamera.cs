using UnityEngine;

namespace BGD.ThirdPersonMovement_2
{
    public class TargetCamera : MonoBehaviour
    {
        [SerializeField] [Range(0f, 5f)] private float _angularSpeed = 0.2f;

        [SerializeField] private Transform _target;
        [SerializeField] private TouchInput _touchInput; //считывание значения поворота камеры.

        private float _angleY;

        private void Start()
        {
            _angleY = transform.rotation.y;
        }

        private void Update()
        {
            _angleY += _touchInput.Horizontal * _angularSpeed; //изменение вычисления поворота камеры, взяв скорость поворота и умножив на перемещение поворота камеры.

            transform.position = _target.transform.position;
            transform.rotation = Quaternion.Euler(0, _angleY, 0);
        }
    }
}

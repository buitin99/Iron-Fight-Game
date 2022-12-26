using UnityEngine;

    public class Target : MonoBehaviour, IExplode {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _size = 0;
        [SerializeField] private float _speed = 0;
        public Rigidbody Rb => _rb;

        void Update() {
            var dir = new Vector3(Mathf.Cos(Time.time * _speed) * _size, Mathf.Sin(Time.time * _speed) * _size);

            _rb.velocity = dir;

        }

        public void Explode() => Destroy(gameObject);
    
}
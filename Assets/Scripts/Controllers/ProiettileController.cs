using UnityEngine;
namespace Assets.Scripts {
    [RequireComponent(typeof(BoxCollider2D))]
    class ProiettileController :MonoBehaviour{
        private float start;
        private float adesso;
        void Start() {
            start = Time.time;
        }
        void Update() {
            adesso = Time.time;
            if(adesso-start > 10.0f) {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collider) {
            //Debug.Log("Collisione");
            Destroy(gameObject);
        }
    }
}

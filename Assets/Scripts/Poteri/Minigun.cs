using System.Collections;
using UnityEngine;
namespace Assets.Scripts.Poteri {
    [System.Serializable]
    public class Minigun : Potere{

        private GameObject proiettile;

        [SerializeField]
        private int colpiBase;
        [SerializeField]
        private float tempoRicarica;
        [SerializeField]
        private float velocitaProiettile = 2.5f;
        private float lastColpo = 0.0f;

        private float attesa = 0.15f;
        private int colpiRimasti;

        public bool staSparando = false;

        private bool staRicaricando = false;

        private Transform transform;
        public Minigun(int colpiBase, float tempoRicarica, Transform transform, GameObject proiettile) {
            this.colpiBase = colpiBase;
            this.tempoRicarica = tempoRicarica;
            this.transform = transform;
            this.proiettile = proiettile;
            UIManager.istanza.StartCoroutine(Ricarica());
        }

        public void Spara(int direzione) {
            float adesso = Time.time;
            if(adesso - lastColpo >= attesa && staSparando) {
                if (colpiRimasti > 0) {
                    GameObject corrente = GameObject.Instantiate(proiettile, transform, false);
                    corrente.transform.position = transform.position + new Vector3(direzione * 0.20f, 0, 0);
                    corrente.transform.localScale = new Vector3(direzione, 1, 1);
                    Rigidbody2D body = corrente.GetComponent<Rigidbody2D>();
                    body.velocity = new Vector2(direzione * velocitaProiettile, body.velocity.y);
                    colpiRimasti--;
                    UIManager.istanza.AggiornaContatoreProiettili(colpiRimasti);
                    lastColpo = adesso;
                    attesa = 0.15f;
                } else if(!staRicaricando){

                    UIManager.istanza.StartCoroutine(Ricarica());
                }
            }
            
        }
        public IEnumerator Ricarica() {
            staRicaricando = true;
            yield return new WaitForSeconds(3.0f);
            colpiRimasti = colpiBase * livello;
            staRicaricando = false;
            UIManager.istanza.AggiornaContatoreProiettili(colpiRimasti);
        }
    }
}

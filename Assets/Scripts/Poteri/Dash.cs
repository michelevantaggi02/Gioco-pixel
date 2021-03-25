using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Poteri {
    [System.Serializable]
    public class Dash : Potere{

        [SerializeField]
        private float distanzaBase = 5.0f;
        private float lastDash;
        private float attesaDash;

        Transform transform;

        public Dash(Transform transform, float distanzaBase, float attesaDash) {
            this.transform = transform;
            this.distanzaBase = distanzaBase;
            this.attesaDash = attesaDash;
        }
        /**
         * Esegue lo scatto se passa una determinata durata di tempo dall'ultimo
         */
        public void Slancia(int dir) {
            float adesso = Time.time;
            /*if(adesso-lastDash >= attesaDash) {
                Vector3 newPos = new Vector3(transform.position.x + (dir * (distanzaBase * livello)), transform.position.y+0.1f, transform.position.z);

                if (Physics2D.OverlapCircle(newPos, 0.08f) == null) {
                    transform.position = newPos;
                    lastDash = adesso;
                } else {
                    Debug.Log("Posizione occupata: " + newPos);
                }

            } else {
                Debug.Log("Attendi altri "+ (attesaDash-(adesso - lastDash)));
            }*/
            if (Controlla(dir)) {
                Vector3 newPos = new Vector3(transform.position.x + (dir * (distanzaBase * livello)), transform.position.y + 0.1f, transform.position.z);
                transform.position = newPos;
                lastDash = adesso;
            }
            
        }

        public bool Controlla(int dir) {
            Vector3 newPos = new Vector3(transform.position.x + (dir * (distanzaBase * livello)), transform.position.y + 0.1f, transform.position.z);
            bool ritorno = (Physics2D.OverlapCircle(newPos, 0.08f) == null && Time.time - lastDash >= attesaDash);
            UIManager.istanza.OffuscaFrameDash(!ritorno);
            UIManager.istanza.AggiornaContatoreDash(Mathf.RoundToInt(attesaDash - (Time.time - lastDash)));
            
            return ritorno;

        }
    }
}

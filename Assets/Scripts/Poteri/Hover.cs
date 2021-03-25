using System;
using UnityEngine;

namespace Assets.Scripts.Poteri
{
    /**
     * Classe del potere di levitazione
     */
    [System.Serializable]
    public class Hover : Potere
    {
        [SerializeField]
        private float hoverSpeed = 0.4f;
        private float durataCorrente { get; set; }
        [SerializeField]
        private bool fluttua = false;
        private Rigidbody2D rg { get; set; }
        private float durataBase;

        private bool esaurito = true;
        public Hover(float durataBase, Rigidbody2D rg){
            this.durataBase = durataBase;
            this.rg = rg;
            RicaricaFluttuata();
        }
        
        /**
         * Inizializza il volo
         */
        public void IniziaFluttuata()
        {

            if (esaurito)
            {
                RicaricaFluttuata();
            }
            fluttua = true;

        }

        /**
         * Funzione chiamata in Update(), gestisce il fluttuare
         */
        public void Fluttua() {

            UIManager.istanza.OffuscaFrameHover(!(durataCorrente > 0.0001f));
            UIManager.istanza.AggiornaContatoreHover(Mathf.RoundToInt(durataCorrente));
            if (fluttua && durataCorrente> 0.0001f)
            {
                if (rg.velocity.y < 0.000001f)
                {
                    rg.gravityScale = 0.000001f;
                    rg.velocity = new Vector2(rg.velocity.x, -hoverSpeed / livello);
                    durataCorrente = durataCorrente - Time.deltaTime;
                    //Debug.Log("Durata corrente: " + durataCorrente);
                }
                
            }
            else
            {
                FermaFluttuata();
            }
        }

        public void RicaricaFluttuata() {
                durataCorrente = durataBase * livello;
                esaurito = false;
            
        }

        public bool StaFluttuando()
        {
            return fluttua;
        }
        /**
         * Ferma la fluttuata dicendo di ricaricare la durata,
         * chiamato quando tocca il terreno
         */
        public void FineFluttuata()
        {
            esaurito = true;
            FermaFluttuata();

        }
        /**
         * Ferma la fluttuata senza ricaricare la durata,
         * chiamato dal giocatore
         */
        public void PausaFluttuata()
        {
            esaurito = false;
            FermaFluttuata();
        }
        /**
         * Chiamato da fine e pausa, ferma la fluttuata
         */
        private void FermaFluttuata()
        {
            rg.gravityScale = 1;
            fluttua = false;
        }
        
    }
}

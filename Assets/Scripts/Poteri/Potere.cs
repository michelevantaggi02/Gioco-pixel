
using System;
using UnityEngine;

namespace Assets.Scripts.Poteri
{
    [System.Serializable]
    public class Potere
    {
        [SerializeField]
        protected int livello = 1;
        protected int esperienza { get; set; }

        /**
         * Controlla il livellamento dell'abilita'
         */
        public void CheckLvlUp()
        {
            if(esperienza/(100*livello) > 1 && livello < 10)
            {
                livello++;
                esperienza = 0;
            }
        }

        /**
         * Aggiunge esperienza alla singola abilita'
         * e controlla se sale di livello
         */
        public void DaiEsperienza(int quantita)
        {
            if (esperienza + quantita < Int32.MaxValue) {
                esperienza += quantita;
                CheckLvlUp();
            }
            
        }
        
    }
}

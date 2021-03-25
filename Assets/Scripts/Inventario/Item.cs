using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventario {
    public class Item {
        private Sprite immagine;
        private int valore;
        private int max;
        private bool distruggibile;

        public Item(Sprite immagine, int max, bool distruggibile, int valore) {
            this.immagine = immagine;
            this.max = max;
            this.distruggibile = distruggibile;
            this.valore = valore;
        }

         public void Usa() {
            if(valore > 0) {
                valore--;
            }
        }

        public void Aggiungi(int quantita) {
            if(valore+quantita > max) {
                valore = max;
            } else {
                valore += quantita;
            }
        }
        public void Rimuovi(int quantita) {
            if(valore-quantita < 0) {
                valore = 0;
            } else {
                valore -= quantita;
            }
        }
    }
}

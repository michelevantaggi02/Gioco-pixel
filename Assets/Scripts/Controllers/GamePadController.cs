
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

namespace Assets.Scripts.Controllers {
    class GamePadController :MonoBehaviour{

        public static Color coloreBase = Color.white;
        /**
     * Fa vibrare il gamepad per n tempo, se non esiste nessun gamepad segnala con un warning
     */
        public static void Vibra(float tempo, float potenzaSx, float potenzaDx) {
            try {
                Gamepad.current.SetMotorSpeeds(potenzaSx, potenzaDx);
                UIManager.istanza.Invoke(nameof(GamePadController.FermaVibrazione), tempo);
                UIManager.istanza.StartCoroutine(SetColorePs4(Color.red));
            } catch (Exception e) {
                Debug.LogWarning(e.Message);
            }
        }
        /**
         * Ferma la vibrazione del gamepad
         */
        private static void FermaVibrazione() {
            Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
            UIManager.istanza.StartCoroutine(SetColorePs4(coloreBase));

        }
        /**
         * Cambia il colore del gamepad, deve essere eseguito in una @<see cref="StartCoroutine"/>
         */
        public static IEnumerator SetColorePs4(Color color) {
            yield return new WaitForSeconds(0.01f);
            try {
                DualShock4GamepadHID gamepad = (DualShock4GamepadHID)Gamepad.current;
                gamepad.SetLightBarColor(color);
            } catch (Exception e) {
                Debug.LogWarning(e.Message);
            }

        }
    }
}

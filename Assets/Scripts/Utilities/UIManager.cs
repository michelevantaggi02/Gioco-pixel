using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager istanza;
    public Image[] cuori;
    public Sprite cuoreVuoto, cuorePieno;
    public Image contenitoreCuori;
    private int vita;
    public int vitaMax = 5;

    public GameObject UI;
    public Text contatoreProiettile;
    public Image frameDash;
    public Image frameHover;
    public Image frameCura;
    public int cure = 2;

    void Awake()
    {
        DontDestroyOnLoad(UI);
        istanza = this;
        vita = vitaMax;
        if (cuori.Length > vitaMax) {

            RectTransform rect = contenitoreCuori.GetComponent<RectTransform>();
            Debug.Log(rect.sizeDelta);
            for (int i = cuori.Length - 1; i >= vitaMax; i--) {
                Destroy(cuori[i]);
                rect.sizeDelta -= new Vector2(42, 0);
            }
        }
        AggiornaCure(0);
    }


    /**
     * Modifica la vita del personaggio
     */
    public void AggiornaVita(int valore) {
        if (vita + valore > vitaMax) {
            vita = vitaMax;
        } else if (vita + valore < 0) {
            vita = 0;
        } else {
            vita += valore;
        }
        int i;
        for (i = 0; i < cuori.Length; i++) {
            if (cuori[i] != null) {
                if (i < vita) {
                    cuori[i].sprite = cuorePieno;
                } else {
                    cuori[i].sprite = cuoreVuoto;
                }
            }
        }
        offuscaFrame(frameCura, cure == 0 || vita == vitaMax);
    }

    public void Danneggia() {
        AggiornaVita(-1);
        GamePadController.Vibra(0.5f, 0.75f, 0.75f);
    }
    public void Cura() {
        if(vita <= vitaMax && cure > 0) {
            AggiornaVita(1);
            AggiornaCure(-1);
        }
    }

    private void AggiornaCure(int valore) {
            cure += valore;
            Text testo = frameCura.GetComponentInChildren<Text>();
            testo.text = "" + cure;
        offuscaFrame(frameCura, cure == 0 || vita == vitaMax);
    }

    public void AggiornaContatoreProiettili(int valore) {
        contatoreProiettile.text = "" + valore;
    }

    public void OffuscaFrameHover(bool offusca) {
        offuscaFrame(frameHover, offusca);
    }

    public void AggiornaContatoreHover(int valore) {
        frameHover.GetComponentInChildren<Text>().text = "" + valore;
    }


    public void OffuscaFrameDash(bool offusca) {
        offuscaFrame(frameDash, offusca);
    }

    public void AggiornaContatoreDash(int valore) {
        Text testo = frameDash.GetComponentInChildren<Text>();
        if (valore > 0) {
            testo.text = "" + valore;
        } else {
            testo.text = "";
        }
    }

    private void offuscaFrame(Image frame, bool offusca) {
        if (offusca) {
            frame.color = new Color(1, 1, 1, 0.5f);
            frame.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
        } else {
            frame.color = new Color(1, 1, 1, 1);
            frame.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        }
    }
}

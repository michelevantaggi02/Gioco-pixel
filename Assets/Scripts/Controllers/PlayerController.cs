using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Poteri;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private int velocitaMassima = 1;
    private int velocitaMassimaCorsa = 3;
    private int velocitaMassimaCamminata = 1;
    //public int incremento = 2;
    [SerializeField]
    private float altezzaSalto = 0.1f;
    private bool isJumping = false;
    private float velocitaVibrazione = -7.0f;
    private float velocitaDiscesa = -8.0f;
    [SerializeField]
    private Hover hover;
    [SerializeField]
    private Dash dash;
    [SerializeField]
    private Minigun minigun;
    
    
    public GameObject proiettile;
    public SpriteRenderer renderSpada;

    public Color coloreBase;
    private Rigidbody2D rg;
    private Vector2 dir;
    private int prevVel;


    void Start()
    {

        DontDestroyOnLoad(gameObject);
        rg = GetComponent<Rigidbody2D>();

        hover = new Hover(1f, rg);
        dash = new Dash(transform, 0.35f, 3.0f);
        minigun = new Minigun(20, 1.5f, transform, proiettile);

        StartCoroutine(GamePadController.SetColorePs4(coloreBase));
    }
    void Update()
    {
        //regola la velocita' massima di movimento
        if (Mathf.Abs(rg.velocity.x) > velocitaMassima){
            rg.velocity = new Vector2(velocitaMassima, rg.velocity.y);
        }else{
            rg.velocity = new Vector2(dir.x, rg.velocity.y);
        }
        if (rg.velocity.y < velocitaDiscesa){
            rg.velocity = new Vector2(rg.velocity.x, velocitaDiscesa);
        }

        //controlla se il personaggio ha incontrato il terreno durante la discesa
        if(Mathf.Abs(rg.velocity.y) < 0.000001f)
        {
            isJumping = false;
            hover.FineFluttuata();
            hover.RicaricaFluttuata();
            if (prevVel < velocitaVibrazione)
            {
                prevVel = 0;
                UIManager.istanza.Danneggia();
            }
        }
        else
        {
            isJumping = true;
        }
        //controlla il potere di fluttuata
        hover.Fluttua();
        //controlla il potere di sparo
        minigun.Spara(GetDirection());

        dash.Controlla(GetDirection());
        //si salva la velocita con cui sta cadendo da usare alla prossima iterazione
        prevVel = Mathf.FloorToInt(rg.velocity.y);
    }

    

    /**
     * Controlla il movimento del personaggio
     */
    public void OnMove(InputValue value)
    {
        dir = value.Get<Vector2>();
        if (dir.x != 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = dir.x < 0;

        }
    }

    /**
     * Controlla il salto del personaggio
     */
    public void OnJump(InputValue value)
    {
        if (!isJumping && value.Get<float>()>0.9999f)
        {
            //ForceMode2D.Impulse indica che la forza non e' costante ma viene applicata solo all'inizio
            rg.AddForce(Vector2.up *altezzaSalto, ForceMode2D.Impulse);
            isJumping = true;
        }
    }
    /**
     * Gestisce la pressione del tasto per fluttuare
     */
    public void OnHover(InputValue value)
    {
        if (isJumping)
        {
            if (hover.StaFluttuando())
            {
                hover.PausaFluttuata();
            }
            else
            {
                hover.IniziaFluttuata();
            }
            //potrebbe tornare utile al livello massimo
            //rg.velocity = Vector2.zero;
        }
    }

    /**
     * Gestisce la pressione del tasto per lo scatto
     */
    public void OnDash(InputValue value) {
        dash.Slancia(GetDirection());
        
    }
    /**
     * Gestisce la corsa ancora indeciso se implementare
     */
    public void OnRun(InputValue value) {
        //Debug.Log("Inizio a correre: "+value.Get<float>());
        if (value.Get<float>()>0.01f) {
            velocitaMassima = velocitaMassimaCorsa;
        } else {
            velocitaMassima = velocitaMassimaCamminata;
        }
    }

    /**
     * Gestisce il fuoco
     */
     public void OnFire(InputValue value) {
        if(value.Get<float>() > 0.01f) {
            minigun.staSparando = true;
        } else {
            minigun.staSparando = false;
        }
    }

    /**
     * Controlla l'attacco con la spada
     */
     public void OnSword(InputValue value) {
        
        renderSpada.flipX = GetDirection() == -1;
        GetComponentInChildren<Animator>().Play("Attacco");
        
    }

    /**
     * Gestisce la cura del personaggio
     */
     public void OnCure(InputValue value) {
        UIManager.istanza.Cura();
    }

    /**
     * Ottiene la direzione verso cui e' girato il personaggio
     */
     private int GetDirection() {
        if(rg.velocity.x == 0) {
            return GetComponent<SpriteRenderer>().flipX ? -1 : 1;
        } else {
            return Mathf.RoundToInt(rg.velocity.x);
        }
    }

    
}

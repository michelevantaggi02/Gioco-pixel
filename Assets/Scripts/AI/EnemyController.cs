using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rg;
    public float speed = 0.3f;
    private bool run = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rg = GetComponent<Rigidbody2D>();
    }

    
    // Update is called once per frame
    void Update()
    {
        Vector3 direzione = player.transform.position - transform.position;
        if (player != null && run) {
            rg.AddForce(direzione);
        }
    }
    private void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(col.gameObject);
        if (col.gameObject.Equals(player)) {
            UIManager.istanza.AggiornaVita(-1);
            run = false;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadBodyController : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Collisione con" + collider.gameObject);
        if (collider.gameObject.Equals(player)) {
            SceneManager.LoadSceneAsync("Test");
        }

    }

 
}

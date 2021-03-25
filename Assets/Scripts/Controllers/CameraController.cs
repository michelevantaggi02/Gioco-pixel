using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform omino;
    public float velocitaCamera = 1.0f;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        omino = GameObject.FindWithTag("Player").transform;
        Debug.Log("Camera position: " + transform.position);
        DontDestroyOnLoad(gameObject);
    }

    /**
     * Muove la telecamera verso il personaggio, con un lieve ritardo
     */
    void FixedUpdate()
    {
        // Debug.Log("Camera bottom: " + Camera.main.ScreenToWorldPoint(Vector3.zero).y);
        // Debug.Log("Camera top: " + Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)));
        //Debug.Log(omino.position.y > Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y);
        /*if(omino.position.y> Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height-(Screen.height/4), 0)).y || omino.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height / 4), 0)).y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + ((omino.position.y-transform.position.y)), transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, velocitaCamera * Time.deltaTime);
        }
        if (omino.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - (Screen.width / 4), 0, 0)).x || omino.position.x < Camera.main.ScreenToWorldPoint(new Vector3((Screen.width / 4), 0, 0)).x)
        {
            Vector3 newPosition = new Vector3(omino.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, velocitaCamera *Time.deltaTime);
        }*/
        //Vector3 newPosition = new Vector3(transform.position.x + ((omino.position.x - transform.position.x) * velocitaCamera), transform.position.y + ((omino.position.y - transform.position.y) *velocitaCamera), transform.position.z);
        Vector3 newPosition = new Vector3(omino.position.x, omino.position.y, transform.position.z) +offset;
        Vector3 smooth = Vector3.Lerp(transform.position, newPosition, velocitaCamera);
        transform.position = smooth;
    }
}

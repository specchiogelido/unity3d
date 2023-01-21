using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemiController : MonoBehaviour{
    Rigidbody rb;
    public Vector3 target;
    public float maxSpeed = 150;
    private float speed;
    public GameObject player;
    public float distanzaRallentamento = 3;
    // Start is called before the first frame update
    void Start()    {
        rb= this.GetComponent<Rigidbody>();
        target = Vector3.zero;
        player = GameObject.Find("Player");
        speed = maxSpeed;
    }

    public void setTarget(Vector3 t) {
        target = t;
    }
    // Update is called once per frame
    void Update() {
        //cerca di andare verso il centro della arena
        Vector3 velDir =  (this.transform.position- target);
        if(velDir.magnitude <= distanzaRallentamento) {
            speed = Mathf.Lerp(speed, 0.1f, distanzaRallentamento);
        } else {
            speed = maxSpeed;
        }
        Vector3 sterzo = rb.velocity - velDir;
        
        rb.AddForce(sterzo.normalized * speed * Time.deltaTime);
        
        //e di scappare dal player
        if (Vector3.Distance(this.transform.position, player.transform.position) < 5) { 
            velDir =  player.transform.position - this.transform.position;
            sterzo = rb.velocity - velDir;
            speed = maxSpeed*4;
            rb.AddForce(sterzo.normalized * speed * Time.deltaTime);
        }
        

    }

    
}

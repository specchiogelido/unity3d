using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour{
    protected GameObject player;
    protected Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()    {
        Vector3 direzione = player.transform.position - this.transform.position;
        direzione.Normalize();
        rb.AddForce(direzione * speed * Time.deltaTime);
    }

    /*
    void Update() {
        Vector3 desiredVel = player.transform.position - this.transform.position;
        desiredVel.Normalize();
        desiredVel *= speed;
        Vector3 sterzo = desiredVel - rb.velocity;
        rb.AddForce(sterzo * Time.deltaTime);
        //this.transform.up = rb.velocity;

    }*/
}

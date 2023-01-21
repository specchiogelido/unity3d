using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileInseguitore : MonoBehaviour{
    public GameObject target;
    public float speed;
    public float forzaSterzo;
    protected Rigidbody rb;
    public bool inVolo = false;
    public GameObject esplosione;
    //usata per comunicare agg punti
    public SpawnManager managerGioco;
    // Start is called before the first frame update
    void Start()    {
        rb = GetComponent<Rigidbody>();
        managerGioco = ((GameObject)GameObject.Find("SpawnManager")).GetComponent<SpawnManager>();
    }

    public void posizionaPrimaDiSparare(Vector3 pos) {
        this.transform.position = pos;
        
    }
    public void fire() {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * speed  * Time.deltaTime, ForceMode.Impulse);
        this.inVolo = true;
    }
    // Update is called once per frame
    void Update()    {

        if (inVolo) {
            if (this.target == null) {
                target=(GameObject.FindGameObjectWithTag("Enemy"));
                if (target == null) return;
            }
            Vector3 desiredVel = target.transform.position - this.transform.position;
            desiredVel.Normalize();
            desiredVel *= speed;
            Vector3 sterzo = desiredVel - rb.velocity;
            rb.AddForce(sterzo * forzaSterzo * Time.deltaTime);
            this.transform.up = rb.velocity;
        }
    }

    private void esplodi() {
        GameObject esplosione_clone = GameObject.Instantiate(this.esplosione, this.transform.position, this.transform.rotation);
        esplosione_clone.GetComponent<ParticleSystem>().Play();
        GameObject.Destroy(esplosione_clone, 1.0f);

    }

    private void OnCollisionEnter(Collision collision) {
        

        if (collision.gameObject.CompareTag("Enemy")) {
            this.managerGioco.aggiornaPunti(5);

            Rigidbody rb=collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(this.rb.velocity * speed);
            this.esplodi();
            GameObject.Destroy(this.gameObject);
            GameObject.Destroy(collision.gameObject,2.0f);
        } else if(collision.gameObject.CompareTag("Missile")==false && collision.gameObject.CompareTag("Player")==false) {
            GameObject.Destroy(this.gameObject);
            this.esplodi();
        }
        
        
    }
}

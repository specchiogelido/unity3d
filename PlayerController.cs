using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public float speed=100.0f;
    protected GameObject focalPoint;
    public bool isPowerUp = false;
    public float durataPowerUp = 7;
    public GameObject indicatorPowerUp;
    Rigidbody rb;
    protected IPowerUpBehavior powerUpBehavior;
    private float countDownPowerUp;
    public GameObject LanciatoreMissili;
    //usata per aggiornare i punti
    protected SpawnManager managerGioco;
    // Start is called before the first frame update
    void Start()    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        managerGioco = ((GameObject)GameObject.Find("SpawnManager")).GetComponent<SpawnManager>();
    }

    public void setIsPowerUp(bool state) {
        isPowerUp = state;
        indicatorPowerUp.SetActive(isPowerUp);
    }
    // Update is called once per frame
    void Update()    {
        //spinge in avanti il plyer ma non nel suo avanti locale ma nell'avanti della
        //scena, quindi utile se il player è una sfera che può essere spinta in avanti
        //rispetto all'osservatore
        rb.AddForce(focalPoint.transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime );
        if (isPowerUp) {
            this.indicatorPowerUp.transform.position = this.transform.position + new Vector3(0, -0.5f, 0);
            this.indicatorPowerUp.transform.Rotate(0, 10.0f, 0);
            
        }
        if (this.transform.position.y <= -10) {

            ((SpawnManager)(GameObject.Find("SpawnManager")).GetComponent<SpawnManager>()).mangeGameOver();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PowerUp")) {
            this.managerGioco.aggiornaPunti(10);
            /*tenta di prendere il comportamento dal powerup*/
            this.powerUpBehavior=other.gameObject.GetComponent<IPowerUpBehavior>();
            if (this.powerUpBehavior != null) {
                this.durataPowerUp = this.powerUpBehavior.durataPowerUp;
                if (powerUpBehavior.nomeComponent == "PowerUpMissile") {
                    this.gameObject.AddComponent<PowerUpMissile>();
                } else if (powerUpBehavior.nomeComponent == "PowerUpMiniEnemy") {
                    this.gameObject.AddComponent<PowerUpMiniEnemy>();

                } else if (powerUpBehavior.nomeComponent== "PowerUpFulmine") {
                    this.gameObject.AddComponent<PowerUpFulmine>();
                    
                }
            }
            this.setIsPowerUp(true);
            GameObject.Destroy(other.gameObject);
            StartCoroutine(contoAllaRovesciaPowerUp());
        }
    }

    IEnumerator contoAllaRovesciaPowerUp() {
        this.countDownPowerUp = this.durataPowerUp;
        while (countDownPowerUp > 0) { 
            yield return new WaitForSeconds(1);
            countDownPowerUp--;
        }
        this.powerUpBehavior=this.gameObject.GetComponent<IPowerUpBehavior>();
        if (powerUpBehavior != null) {
            GameObject.Destroy(this.GetComponent<IPowerUpBehavior>());
            
        }
        this.setIsPowerUp(false);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy") && this.isPowerUp) {
            //Debug.Log("Collisione Enter to: " + collision.gameObject.name + " Player powerUp=" + this.isPowerUp);
            Rigidbody rb=collision.gameObject.GetComponent<Rigidbody>();
            Vector3 direzioneSpinta = collision.GetContact(0).normal;
            rb.AddForce(-direzioneSpinta * 200, ForceMode.Impulse);
            this.managerGioco.aggiornaPunti(1);
            //rb.AddExplosionForce(100, collision.GetContact(0).point, 20);
        }
    }

    private void OnGUI() {
        if (this.isPowerUp) { 
            Rect pos = new Rect(new Vector2(10, 200), new Vector2(100, 80));
            string text = "Power Up Remain: "+ this.countDownPowerUp;

            GUI.TextArea(pos, text);
        }
    }
}

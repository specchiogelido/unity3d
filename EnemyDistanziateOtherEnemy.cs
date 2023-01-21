using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanziateOtherEnemy : MonoBehaviour{
    // Start is called before the first frame update
    Vector3 forzaRepulsivaCentroStormo;
    Rigidbody rb;
    public float intervalloScansione;
    public float ampiezzaPercezione = 2.0f;
    public float velocitaAllontanamento = 150;
    public GameObject centerStornIndicatorDebug=null;
    void Start(){
        rb = GetComponent<Rigidbody>();
        StartCoroutine("rilevaVicini");
        
        //GameObject istanza = (GameObject) Resources.Load("CenterStormIndicator", typeof(GameObject));
        //Debug.Log("Istanza load: "+istanza.name);
        //centerStornIndicatorDebug = GameObject.Instantiate(istanza, this.transform.position, this.transform.rotation);
    }

    // Update is called once per frame
    void Update(){
        rb.AddForce(this.forzaRepulsivaCentroStormo * Mathf.Lerp(rb.velocity.magnitude,this.velocitaAllontanamento,0.6f));
        this.forzaRepulsivaCentroStormo = Vector3.zero;
    }

    IEnumerator rilevaVicini() {
        while (true) {
            yield return new WaitForSeconds(intervalloScansione);
            //Debug.Log("Enemy: " + this.gameObject.name + ":> Rilevo Vicini:");
            RaycastHit[] hitInfo;
            if ( (hitInfo=Physics.SphereCastAll(this.transform.position, ampiezzaPercezione, this.transform.forward, 0.05f))!=null ) {
                Vector3 direction = Vector3.zero;
                GameObject other;
                int numEnemy = 1;
                for (int i = 0; i < hitInfo.Length; i++) {
                    other = hitInfo[i].collider.gameObject;
                    if (other != this.gameObject && other.CompareTag("Enemy")) {
                        //centerPos += other.transform.position;
                        direction += other.transform.position - this.transform.position;
                        numEnemy++;
                    }
                }
                //centerPos /= numEnemy;
                Vector3 forza =  direction.normalized;
                
                forzaRepulsivaCentroStormo = (new Vector3(forza.x,forza.y,forza.z))*-1;
                //Debug.Log("      : " + this.gameObject.name + ":> Rilevati "+hitInfo.Length+" Vicini");
                if (this.centerStornIndicatorDebug != null) {
                    this.centerStornIndicatorDebug.transform.position =  this.transform.position + (forza * -2);
                }
            }
        }
    }
}

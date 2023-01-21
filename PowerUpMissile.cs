using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMissile : IPowerUpBehavior {
    // Start is called before the first frame update
    
    public GameObject lanciatoreMissili;
    void Start(){
        
            this.durataPowerUp = 10;
            this.nomeComponent = "PowerUpMissile";
        if (this.gameObject.CompareTag("Player")) {
            
            GameObject istanza = (GameObject)Resources.Load("LanciaMissili", typeof(GameObject));
            this.lanciatoreMissili = GameObject.Instantiate(istanza, this.transform.position, istanza.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update(){
        if (this.gameObject.CompareTag("Player")) { 
            lanciatoreMissili.transform.position = this.transform.position + Vector3.up;
            lanciatoreMissili.transform.Rotate(new Vector3(0,1,0));

        }

    }

    public void OnDestroy() {
        GameObject.Destroy(lanciatoreMissili);
    }


}

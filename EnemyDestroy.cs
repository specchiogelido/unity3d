using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour{
    // Start is called before the first frame update
    public SpawnManager managerGioco;
    void Start(){
        managerGioco = ((GameObject)GameObject.Find("SpawnManager")).GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update(){
        if (this.transform.position.y < -10 || this.transform.position.y > 10) {
            managerGioco.aggiornaPunti(10);
            Destroy(this.gameObject);

        }
    }
}

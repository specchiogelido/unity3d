using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFulmine : IPowerUpBehavior{
    // Start is called before the first frame update
    protected GameObject generatoreFulmini;
    [SerializeField] public float ampiezzaFulmini;

    void Start()    {
        this.nomeComponent = "PowerUpFulmine";
        this.durataPowerUp = 30;
        if (this.gameObject.CompareTag("Player")) {

            GameObject istanza = (GameObject)Resources.Load("GeneratoreFulmini", typeof(GameObject));
            this.generatoreFulmini = GameObject.Instantiate(istanza, this.transform.position+Vector3.up, istanza.transform.rotation);
            
        }
        //generatoreFulmini = GameObject.Instantiate(generatoreFulmini, this.transform.position, this.transform.rotation);
    }

    // Update is called once per frame
    void Update()    {
        //gestita solo dal player
        if (this.gameObject.CompareTag("Player")) {
            generatoreFulmini.transform.position = this.transform.position +Vector3.up;
            //generatoreFulmini.transform.rotation = this.transform.rotation;
        }

    }

    public void OnDestroy() {
        GameObject.Destroy(generatoreFulmini);
    }
}

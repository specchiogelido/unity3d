using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMiniEnemy : IPowerUpBehavior{
    // Start is called before the first frame update
    GameObject player;
    GameObject[] enemy;
    public int particellePerEnemy = 40;
    public float intervalloTraCreazione = 0.5f;

    void Start(){
        this.nomeComponent = "PowerUpMiniEnemy";
        this.durataPowerUp = 25;

        player = GameObject.Find("Player");
        //codice eseguito dal player solo quanto il powerup di aggiunge compo componente in esso
        if (this.gameObject == player) {
            StartCoroutine(this.contoAllaRovescia());
        }
    }

    // Update is called once per frame
    void Update(){
        //viene eseguito dal player
        if (this.transform.name == "Player" && false) {
            if (enemy == null) {
                //la prima volta emeny è null perché sebbene sia stato riempito dall'oggetto powerup
                //poi quando questo componente viene aggiunto al player dinamicamente, l'array enemy
                //è vuoto nel contesto del player che lo riempie dei soli oggetti enemy e non minienemy
                EnemyFollowPlayer[] enemyGrandi;
                enemyGrandi=GameObject.FindObjectsOfType<EnemyFollowPlayer>();
                enemy = new GameObject[enemyGrandi.Length];
                for(int i = 0; i < enemy.Length; i++) {
                    enemy[i] = enemyGrandi[i].gameObject;
                    GameObject.Destroy(enemy[i].GetComponent<Rigidbody>());
                    GameObject.Destroy(enemy[i].GetComponent<Collider>());
                    GameObject.Destroy(enemy[i].GetComponent<EnemyFollowPlayer>());
                }
            }
            if (enemy.Length > 0) {
                float y;
                for(int i = 0; i < enemy.Length; i++) {
                    if (enemy[i]) { 
                        y = Mathf.Lerp(enemy[i].transform.position.y, 1.0f, 0.3f);
                        enemy[i].transform.position += new Vector3(0, y, 0);
                    }
                }
            }
        }
    }
    public IEnumerator contoAllaRovescia() {
        //codice eseguito dal player
        Debug.Log("coroutine di powerUp eseguita da " + this.transform.name);
        yield return new WaitForSeconds(this.durataPowerUp);
        GameObject[] miniEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        int size = miniEnemy.Length;
        GameObject e;
        Debug.Log("Trovati " + size + " Enemy da trasformare");
        Vector3 moveMiniEnemy = new Vector3(0.0f, 0.5f, 0.0f);
        for (int i = 0; i < size; i++) {
            e = miniEnemy[i];
            if (e != null) {
                e.transform.position = moveMiniEnemy;
                GameObject.Destroy(e);
                //yield return new WaitForSeconds(this.intervalloTraCreazione);

            }
        }
        //size = size / particellePerEnemy;
        if (size/particellePerEnemy > 0) {
            for (int i = 0; i < size/particellePerEnemy; i++) {
                //e = (GameObject) Resources.Load("Enemy", sizeof(GameObject));
                e = (GameObject)Resources.Load("Enemy", typeof(GameObject));
                float x = 0;// Random.RandomRange(-8, 8);
                float z = 0;// Random.RandomRange(-8, 8);
                Vector3 pos = new Vector3(x, 1.0f, z);
                GameObject.Instantiate(e, pos, this.transform.rotation);
            }
        }

        
        
        
    }
    

    private void OnTriggerEnter(Collider other) {
        //codice eseguito dal powerUp appena prima di essere aggiunto come component al player
        //come fosse una fork dove nello stesso listato c'è codice figlio e padre
        if (other.gameObject.CompareTag("Player")) {

            enemy = GameObject.FindGameObjectsWithTag("Enemy");
            int size = enemy.Length;
           // if (size > 3) size = 3;
            GameObject e;
            Vector3 pos;
            for (int i = 0; i < size; i++) {
                e = enemy[i];
                pos = e.transform.position;
                //attenzione, al posto della destroy lo sposto sotto il terreno
                //per poi farlo riemergere gradualmente durante la durata del powerUp
                //e.transform.position += new Vector3(0, 4.0f, 0);

                GameObject istanza = (GameObject)Resources.Load("MiniEnemy", typeof(GameObject)) as GameObject;
                GameObject miniEnemy;
                for (int n = 0; n < particellePerEnemy; n++) {
                    pos.x = pos.x+Mathf.Cos(n)*2;
                    pos.z = pos.z+Mathf.Sin(n)*2;
                    pos.y = Random.Range(0.5f, 2.0f);
                    miniEnemy=(GameObject)GameObject.Instantiate(istanza, pos, e.transform.rotation);
                    miniEnemy.GetComponent<MiniEnemiController>().setTarget(pos);
                    //yield return new WaitForSeconds(this.intervalloTraCreazione);

                }
                //attenzione: da ripristinare se non va il riposizionamento sotto il terreno riga 88
                GameObject.Destroy(e);
                
                
            }
            
        }
    }
}

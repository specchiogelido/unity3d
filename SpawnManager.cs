using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour{
    public GameObject enemyPrefab;
    int enemyCount = 1;
    public GameObject scoreText;
    public GameObject gameOverText;
    public int score=0;
    protected bool gameOver = false;
    // Start is called before the first frame update
    void Start()    {
        
        Debug.Log("scoreText= " + scoreText);
        this.aggiornaPunti(0);
        spawEnemyWave(enemyCount);
    }

    public void mangeGameOver() {
        //se già sto in gameOver lo sto gia gestendo quindi esco. Questo garantisce che anche se
        //arriva un'altra chiamata dal player verrà gestina una sola coroutine di riavvio.
        if (gameOver == true) return;
        this.gameOver = true;
        this.gameOverText.SetActive(gameOver);
        this.distruggiTuttiNemici();
        StartCoroutine(countDownResetGame(5));
    }

    public IEnumerator countDownResetGame(float n) {
        TextMeshProUGUI message = this.gameOverText.GetComponent<TextMeshProUGUI>();
        while (n > 0) {
            yield return new WaitForSeconds(1);
            message.text = "Game Over\n The game reset in " + n + " seconds. \n Your score is: " + this.score;
            n--;
        }
        this.resetGame();
        
    }

    public void distruggiTuttiNemici() {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemy!=null) {
            
            foreach( GameObject e in enemy){
                GameObject.Destroy(e);
            }
        }
    }
   public void resetGame() {
        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(0, 0.5f, 0);
        player.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        

        score = 0;
        enemyCount = 1;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Punteggio: 0";
        gameOverText.SetActive(false);
        this.gameOver = false;

    }

    private void spawEnemyWave(int n) {

        GameObject[] powerUp = GameObject.FindGameObjectsWithTag("PowerUp");
        if (powerUp.Length > 0) {
            for(int i=0; i < powerUp.Length; i++) {
                GameObject.Destroy(powerUp[i]);
            }
        }
        powerUp=new GameObject[4];
        powerUp[0]=(GameObject) Resources.Load("PowerUp", typeof(GameObject));
        powerUp[1] = (GameObject)Resources.Load("PowerUp_Missile", typeof(GameObject));
        powerUp[2] = (GameObject)Resources.Load("PowerUp_MiniEnemy", typeof(GameObject));
        powerUp[3] = (GameObject)Resources.Load("PowerUp_fulmine", typeof(GameObject));
        int id = Random.RandomRange(0, 4);
        GameObject.Instantiate(powerUp[id], randomPosition(), powerUp[id].transform.rotation);
        for(int i = 0; i < n; i++) {
            Instantiate(enemyPrefab, randomPosition(), enemyPrefab.transform.rotation);
        }
    }
    protected Vector3 randomPosition() {
        float x = Random.RandomRange(-8, 8);
        float z = Random.RandomRange(-8, 8);
        Vector3 pos = new Vector3(x, 1.0f, z);
        return pos;
    }

    public void aggiornaPunti(int n) {
        score += n;
        (scoreText.GetComponent<TextMeshProUGUI>()).text = "Punteggio: " + score;

        //scoreText.text = "Punteggio: " + n;

    }
    // Update is called once per frame
    void Update()    {
        if (gameOver == true) return;

        if (FindObjectsOfType<EnemyFollowPlayer>().Length == 0) {
            this.enemyCount++;
            this.spawEnemyWave(enemyCount);
        }
        
    }
}

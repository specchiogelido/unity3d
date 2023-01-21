using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanciatoreMissili : MonoBehaviour{
    // Start is called before the first frame update
    public GameObject missilePrefab;
    void Start()    {
        
    }

    public void lanciaMissili() {
        EnemyFollowPlayer[] enemy = GameObject.FindObjectsOfType<EnemyFollowPlayer>();
        if (enemy != null) {
            Vector3 pos;
            for(int i = 0; i < enemy.Length; i++) {
                GameObject m=(GameObject)GameObject.Instantiate(missilePrefab, this.transform.position, this.transform.rotation);
                (m.GetComponent<MissileInseguitore>()).target = enemy[i].gameObject;
                pos.x = Random.Range(-0.5f, 0.5f);
                pos.z = Random.Range(-0.5f, 0.5f);
                pos.y = 1.0f;
                pos += this.transform.position;

                (m.GetComponent<MissileInseguitore>()).posizionaPrimaDiSparare(pos);
                (m.GetComponent<MissileInseguitore>()).fire();
            }
        }
    }
    // Update is called once per frame
    void Update()    {
        if (Input.GetKeyUp(KeyCode.Space)) this.lanciaMissili();
    }
}

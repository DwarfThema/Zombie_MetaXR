using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_AttackFlameDeal : MonoBehaviour
{
    [SerializeField] 
        GameObject[] burningFxPreArray = new GameObject[3];

    JH_AttackFlameEnemy flameEnemyP;
    JH_AttackFlameEnemy flameEnemyC;
    JH_AttackFlameEnemy flameEnemyMe;

    GameObject burningFxPre;
    GameObject burningFx;
    int random;

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0,2);
        burningFxPre = burningFxPreArray[random];        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
            flameEnemyP = other.GetComponentInParent<JH_AttackFlameEnemy>();
            flameEnemyC = other.GetComponentInChildren<JH_AttackFlameEnemy>();
        Debug.Log(flameEnemyP);
        Debug.Log(flameEnemyC);

            if(!flameEnemyP && !flameEnemyC){
                burningFx = Instantiate(burningFxPre, other.transform.position, other.transform.rotation);
                burningFx.transform.SetParent(other.transform, false);
            }
    }
}

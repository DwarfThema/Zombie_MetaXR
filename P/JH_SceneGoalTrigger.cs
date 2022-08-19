using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JH_SceneGoalTrigger : MonoBehaviour
{
    public GameObject goalUi;
    public GameObject semiUi;
    public GameObject preUi;
    private void Start() {
        if(preUi){
            preUi.SetActive(true);
            
            if(preUi.activeInHierarchy){
                semiUi.SetActive(false);
            }else{
                 semiUi.SetActive(true);
            }
        }

        goalUi.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
        semiUi.SetActive(false);
        goalUi.SetActive(true);
        }
    }
}

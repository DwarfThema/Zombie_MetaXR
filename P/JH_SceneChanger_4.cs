using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JH_SceneChanger_4 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Car")){
            SceneManager.LoadScene(5);
        }
    }
}

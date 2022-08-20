using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JH_SceneChanger : MonoBehaviour
{
    [SerializeField] int toScene;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            SceneManager.LoadScene(toScene - 1);
        }
    }
}

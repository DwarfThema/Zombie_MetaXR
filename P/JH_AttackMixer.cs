using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_AttackMixer : MonoBehaviour
{

    JH_isHeadHit head;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 8){
            head = other.GetComponentInChildren<JH_isHeadHit>();
            head.OnRaycastHeadHit(Vector3.up);
        }
    }
}

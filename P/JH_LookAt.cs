using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_LookAt : MonoBehaviour
{
    GameObject target;
    GameObject isMe;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per framesssss
    void Update()
    {
       
        transform.LookAt(target.transform.position);
    }
}

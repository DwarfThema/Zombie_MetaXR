using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_SceneChanger_3 : MonoBehaviour
{   
    [SerializeField] GameObject uiFindKey;
    [SerializeField] GameObject uiOpenDoor;
    [SerializeField] GameObject uiWayOut;

    [SerializeField] GameObject key;
    [SerializeField] GameObject door;
    [SerializeField] GameObject uiKeyDir;
    [SerializeField] GameObject uiDoorDir;
    [SerializeField] GameObject wayOutTirgger;

    JH_DoorActive doorActive;

    Animator anim;
    
    void Start()
    {
        uiFindKey.SetActive(true);
        uiOpenDoor.SetActive(false);
        uiWayOut.SetActive(false);

        uiKeyDir.SetActive(true);
        uiDoorDir.SetActive(false);
        wayOutTirgger.SetActive(false);

        doorActive = door.GetComponent<JH_DoorActive>();
    }


    void Update()
    {
        if(key.activeInHierarchy == false && uiFindKey.activeInHierarchy == true){
            uiOpenDoor.SetActive(true);
            uiKeyDir.SetActive(false);
            uiDoorDir.SetActive(true);
            anim = uiFindKey.GetComponent<Animator>();
            anim.SetTrigger("clear");
            StartCoroutine(uiClear());
        }

        if(doorActive.isOpen && uiOpenDoor.activeInHierarchy == true){
            uiWayOut.SetActive(true);
            uiDoorDir.SetActive(false);
            wayOutTirgger.SetActive(true);
            anim = uiOpenDoor.GetComponent<Animator>();
            anim.SetTrigger("clear");
            StartCoroutine(uiClear());
        }
    }

    IEnumerator uiClear(){
        yield return new WaitForSeconds(1);
        uiFindKey.SetActive(false);
    }
}

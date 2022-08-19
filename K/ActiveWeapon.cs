using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;


public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }

    public Transform[] weaponSlots;
    public bool isChangingWeapon;

    RayCastWeapon1[] equipped_weapon = new RayCastWeapon1[2];
    public CharacterAiming characterAiming;
    AmmoWidget ammoWidget;
    public Transform crossHairTarget;
    //ReloadWeapon reload;

    //int activeWeaponIndex = -1;
    int activeWeaponIndex;
    //����� ������Ʈ, ���Ⱑ ���� ���̾Ű ����
    bool isHolstered = false;

    
    public Transform weaponParent;
    public Transform weaponLeftGrip;
    public Transform weaponRightGrip;
    public Animator rigController;
    

    
   



    //���⽽�� �ѹ���


    //�������� ���� �迭 ����(2��)

    //Ȱ���� ���� �ε���


    private void Awake()
    {
        //crossHairTarget = Camera.main.transform.Find("CrossHairTarget");
        //ammoWidget = FindObjectOfType<AmmoWidget>;
        characterAiming = GetComponent<CharacterAiming>();
        //reload = GetComponent<ReloadWeapon>();
    }
    void Start()
    {
        rigController.updateMode = AnimatorUpdateMode.AnimatePhysics;
        rigController.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
        rigController.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        rigController.updateMode = AnimatorUpdateMode.Normal;
        //�¾�� �������� ���Ⱑ �ִ��� Ž��, 
        RayCastWeapon1 existingWeapon = GetComponentInChildren<RayCastWeapon1>();
        //�������� ���Ⱑ �������
        if (existingWeapon)
        {   //�������ι��� ����
            Equip(existingWeapon);
        }
    }

    private void OnEnable()
    {
        rigController.updateMode = AnimatorUpdateMode.AnimatePhysics;
        rigController.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
        rigController.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        rigController.updateMode = AnimatorUpdateMode.Normal;


    }
    public bool IsFiring()
    {
        RayCastWeapon1 currentWeapon = GetActiveWeapon();
        if (!currentWeapon) return false;
        return currentWeapon.isFiring;
    }


    public RayCastWeapon1 GetActiveWeapon()
    {
        return GetWeaPon(activeWeaponIndex);
    }

    //���⸦ ��� �Լ�
    RayCastWeapon1 GetWeaPon(int index)
    {   //���� �ε����� 0���� �۰ų� �迭�� ũ�⺸�� Ŭ��� ��ȯ�� ����
        if (index < 0 || index >= equipped_weapon.Length)
            return null;
        //�׷��� ������� �ش� �ε����� �迭 ��ġ�� ���� ���� ����. 
        return equipped_weapon[index];
    }


    void Update()
    {
        var weapon = GetWeaPon(activeWeaponIndex);
        //bool notSprinting = rigController.GetCurrentAnimatorStateInfo(2).shortNameHash == Animator.StringToHash("not_sprint");
        bool canFire = !isHolstered; //&& notSprinting; //&& !reload.isReloading;
        if (weapon&&!isHolstered)
        {
            if (Input.GetButton("Fire1") && !weapon.isFiring/*&&canFire*/)
            {
                weapon.StartFiring();
            }
            if (Input.GetButtonUp("Fire1")/*||!canFire*/)
            {
                weapon.StopFiring();
            }
           

            weapon.UpdateWeapon(Time.deltaTime, crossHairTarget.position);

        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            bool isHolstered = rigController.GetBool("holster_weapon");
            rigController.SetBool("holster_weapon", !isHolstered);

        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WeaponSlot.Primary);
            //weaponSlots[0].gameObject.SetActive(true);
            //weaponSlots[1].gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveWeapon(WeaponSlot.Secondary);
            //weaponSlots[0].gameObject.SetActive(false);
            //weaponSlots[1].gameObject.SetActive(true);
        }



    }
        //���� �����ϴ� �Լ�,�� �Լ��� ������ ��ũ��Ʈ�� ���ڰ����� �޴´�.
        public void Equip(RayCastWeapon1 newWeapon)
        {
            //���� ������ ���� ���������� ������ �����Ѵ�.
            int weaponSlotIndex = (int)newWeapon.weaponSlot;
            //�ش� �������� �迭 ��ġ�� ���⸦ �����Ѵ�.
            var weapon = GetWeaPon(weaponSlotIndex);
            //���Ⱑ �̹� �ش� �迭�� ���� ���
            if (weapon)
            {
                //�ش� ���⸦ �����Ѵ�.
                Destroy(weapon.gameObject);

            }
            weapon = newWeapon;
        
        
            weapon.recoil.characterAiming = characterAiming;
            weapon.recoil.rigController = rigController;
            weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
            equipped_weapon[weaponSlotIndex] = weapon;
            SetActiveWeapon(newWeapon.weaponSlot);
            


            //if(ammoWidget)
            //{
            //    ammoWidget.Refresh(weapon.ammoCount, weapon.clipCount);
            //}


        }





    void ToggleActiveWeapon()
    {
        bool isHolstered = rigController.GetBool("holster_weapon");
        if (isHolstered) StartCoroutine(ActivateWeapon(activeWeaponIndex));
        else StartCoroutine(HolsterWeapon(activeWeaponIndex));
        
    }
    


        void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;
        if (holsterIndex == activateIndex || isChangingWeapon)
        {
            //return;
            holsterIndex = -1;
        }
        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        //rigController.SetInteger("weapon_index", activateIndex);
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;
    }

    IEnumerator HolsterWeapon(int index)
    {
        isChangingWeapon = true;
        isHolstered = true;
        var weapon = GetWeaPon(index);
        if (weapon)
        {

            rigController.SetBool("holster_weapon", true);
            do
            {
                yield return new WaitForSeconds(0.05f);
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            
            isChangingWeapon = false;

        }
    }

    IEnumerator ActivateWeapon(int index)
    {
        isChangingWeapon = true;
        var weapon = GetWeaPon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            do
            {
                yield return new WaitForSeconds(0.05f);
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            
            isHolstered = false;
        }
        isChangingWeapon = false;
    }

    //public void DropWeapon()
    //{
    //    var currentWeapon = GetActiveWeapon();
    //    if (currentWeapon)
    //    {
    //        currentWeapon.transform.SetParent(null);
    //        currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
    //        currentWeapon.gameObject.AddComponent<Rigidbody>();
    //        equipped_weapon[activeWeaponIndex] = null;
    //    }
    //}

    //public void RefillAmmo(int clipCount)
    //{
    //    var weapon = GetActiveWeapon();
    //    if (weapon)
    //    {
    //        weapon.clipCount += clipCount;
    //        ammoWidget.Refresh(weapon.ammoCount, weapon.clipCount);
    //    }
    //}


}

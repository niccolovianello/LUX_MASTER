using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class ObjectsManagement : MonoBehaviour
    {
        public Equipment[] loadout;
        public Equipment[] pickLoadout;
        public Transform objectParent;
        private GameObject currentObject;
        public PlayerCombatScript weaponProperties;
        private Animator JhonnyAnimator;
        public int[] ammo; // 0 fiammiferi 1 olio 2 batterie 5 oggetti curativi
        

        private int currentIndex = 0;
        public UIScript UI;
        
        
        private void Start()
        {
            pickLoadout = new Equipment[3];
            ammo = new int[5];
            ammo[0] = 5;
            UI.UpdateResources("Matches", 5);
            JhonnyAnimator = FindObjectOfType<Animator>();

            // JhonnyAnimator.SetBool("HaveTorch", true);
            //JhonnyAnimator.SetBool("HaveTorch", false);

        }

        void Update()

        {
            //if (Input.GetKeyDown(KeyCode.Tab))
            //{ if (currentIndex == 2)
            //    {
            //        Equip(0);
            //        currentIndex = 0;

            //    }
            //    else {
            //        Equip(currentIndex + 1);
            //        currentIndex++;
            //    }

            //}
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                MatchLightMethod();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Equip(0);
                currentIndex = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Equip(1);
                currentIndex = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Equip(2);
                currentIndex = 2;
            }

            if (currentObject != null)
            {
                Aim(Input.GetMouseButton(1));
            }

            



        }


        void MatchLightMethod()
        {



            if (ammo[0] > 0)
            {
                if (currentObject != null)
                {
                    Destroy(currentObject);

                    if (pickLoadout[currentIndex] != null)

                        pickLoadout[currentIndex].isSelected = false;
                }
                GameObject match = Instantiate(loadout[2].prefab, objectParent.position, objectParent.rotation, objectParent) as GameObject;
                JhonnyAnimator.SetBool("HaveTorch", false);

                match.transform.localPosition = Vector3.zero;
                match.transform.localEulerAngles = Vector3.zero;
                weaponProperties.attackRange = loadout[2].attackRange;
                weaponProperties.attackRate = loadout[2].attackRate;
                weaponProperties.attackDamage = loadout[2].damage;
                //weaponProperties.animationObj = loadout[2].animatorObject;
                currentObject = match;

                UI.ActiveWeapon(-1);

            }
            else
                Debug.Log("fiammiferi finiti");

           

        }
        void Equip(int eq_index)

        {
            //if (eq_index == currentIndex)
            //    return;

            if (pickLoadout[eq_index] == null)
            {
                Debug.Log("There's not any object in the selected slot");
                return;
            }

            if (pickLoadout[eq_index].prefab == currentObject)
                return;

            if (currentObject != null)
            {
                Destroy(currentObject);
                pickLoadout[currentIndex].isSelected = false;
            }


            GameObject t_newEquipment = Instantiate(pickLoadout[eq_index].prefab, objectParent.position, objectParent.rotation, objectParent) as GameObject;
            
            t_newEquipment.transform.localPosition = Vector3.zero;
            t_newEquipment.transform.localEulerAngles = Vector3.zero;

            weaponProperties.attackRange = pickLoadout[eq_index].attackRange;
            weaponProperties.attackRate = pickLoadout[eq_index].attackRate;
            weaponProperties.attackDamage = pickLoadout[eq_index].damage;
            //weaponProperties.animationObj = pickLoadout[eq_index].animatorObject;


            currentObject = t_newEquipment;
            currentIndex = eq_index;
            pickLoadout[eq_index].isSelected = true;

            UI.ActiveWeapon(currentIndex);

            if (pickLoadout[currentIndex].name == "Torch")
                JhonnyAnimator.SetBool("HaveTorch", true);
            else
                JhonnyAnimator.SetBool("HaveTorch", false);
        }

        void Aim(bool isAiming)
        {
            if (currentObject == null || pickLoadout[currentIndex] == null)
                return;

            Transform t_anchor = currentObject.transform.Find("Anchor");
            Transform t_state_ads = currentObject.transform.Find("States/ADS");
            Transform t_state_hip = currentObject.transform.Find("States/Hip");

            if (isAiming)
            {
                t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * pickLoadout[currentIndex].aimSpeed);
            }
            else
            {

                t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * pickLoadout[currentIndex].aimSpeed);
            }

        }

        public void PickEquipment(string equipmentPick)
        { 
           
            int index=0;
            int indexCounting = 0;
            int ind_pick= 3000;
            bool replacement = true;
            bool isPlaced = false;

            foreach (Equipment e in loadout)
            {
                if (e.name == equipmentPick)
                {
                    index = indexCounting;
                    Debug.Log("E' entrato");
                }
                else
                    indexCounting++;
            }

            for (int i = 0; i < 3; i++)
            {
                if (pickLoadout[i] == null && isPlaced == false)
                {
                    pickLoadout[i] = loadout[index];
                    ind_pick = i;
                    replacement = false;
                    isPlaced = true;
                }
               
            }
            if (replacement == true)
            {
                pickLoadout[currentIndex].isSelected = false;
                pickLoadout[currentIndex] = loadout[index];
                ind_pick = currentIndex;

            }

            Debug.Log(ind_pick);

            Equip(ind_pick);

            UI.UpdateWeapons(equipmentPick, ind_pick);
            UI.ActiveWeapon(ind_pick);
        }

        public int getCurrentIndex()
        {
            return currentIndex;
        }

        // return current object
        public GameObject getCurrentObj()
        {
            return currentObject;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Runtime.CompilerServices;

namespace Com.Kawaiisun.SimpleHostile
{
    public class UIScript : MonoBehaviour
    {
        Transform Weapons;
        Transform Resources;
        Transform InfoPanel;
        Transform DeathPanel;

        float alpha;
        float alpha1;
        //Text t = child.GetComponent<Text>();

        // Start is called before the first frame update
        void Awake()
        {
            Resources = transform.Find("Resources");
            Weapons = transform.Find("WeaponsInventory");
            InfoPanel = transform.Find("InfoPanel");
            DeathPanel = transform.Find("DeathPanel");
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UpdateWeapons(string weaponname, int weaponpos)
        {
            int pos = weaponpos + 1;
            Transform weapSlot = Weapons.Find("Panel/WeaponSlot (" + pos + ")");
            weapSlot.GetComponentInChildren<Text>().text = weaponname;
        }

        public void ActiveWeapon(int newwpos)
        {
            Image Matches = Resources.Find("Panel/Matches").GetComponent<Image>();
            if (newwpos != 3)
            {
                for (int i = -1; i < 2; i++) // POI METTERE < 3 QUANDO AGGIUNGO ULTIMO SLOT + CAMBIA IN DESTROY MATCH E TORCH (3 -> 4)
                {
                    if (newwpos == i)
                    {
                        if (newwpos == -1)
                        {
                            Matches.color = new Color32(100, 100, 100, 255);
                        }
                        else
                        {
                            Weapons.Find("Panel/WeaponSlot (" + (newwpos + 1) + ")").GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                        }
                    }
                    else
                    {
                        if (i == -1)
                        {
                            Matches.color = new Color32(100, 100, 100, 100);
                        }
                        else
                        {
                            Weapons.Find("Panel/WeaponSlot (" + (i + 1) + ")").GetComponent<Image>().color = new Color32(100, 100, 100, 100);
                        }
                    }
                }
            }
            else
            {
                for (int i = -1; i < 2; i++)
                {
                    if(i == -1)
                    {
                        Matches.color = new Color32(100, 100, 100, 100);
                    }
                    else
                    {
                        Weapons.Find("Panel/WeaponSlot (" + (i + 1) + ")").GetComponent<Image>().color = new Color32(100, 100, 100, 100);
                    }
                }
            }
        }

        public void UpdateResources(string resourcename, int resourcenumber)
        {
            Transform resSlot = Resources.Find("Panel/" + resourcename);
            Text numRes = resSlot.Find("ResourceNumberCircle").GetComponentInChildren<Text>();
            int oldRes = int.Parse(numRes.text);
            int tot = oldRes + resourcenumber;
            numRes.text = tot.ToString();

            Text addrem = resSlot.Find("AddRemove").GetComponentInChildren<Text>();
            if (resourcenumber > 0)
            {
                addrem.color = new Color32(0, 255, 0, 255);
                addrem.text = "+" + resourcenumber;
            }
            else
            {
                addrem.color = new Color32(255, 0, 0, 255);
                addrem.text = "-" + resourcenumber;
            }

            StartCoroutine(ExecuteAfterTime(0.5f, addrem));
        }

        public void UpdateInfo(string info)
        {
            Text infotext = InfoPanel.GetComponentInChildren<Text>();
            infotext.text = info;

            //StartCoroutine(InfoAfterTime(2f, infotext));
        }

        public void HurtUI(float damage)
        {
            if (damage >= 255)
            {
                alpha = damage;
                DeathPanel.GetComponentInChildren<Text>().DOColor(new Color32(255, 255, 255, 255), 0.7f);
            }
            else if (damage == 0)
            {
                alpha = damage;
            }
                
            else
            {
                alpha += (damage * 0.5f);
            }
            DeathPanel.GetComponent<Image>().DOColor(new Color32(138, 3, 3, (byte)alpha), 0.5f);
        }

        public void TimerDarkUI(float timer)
        {
            if (timer >= 255)
            {
                alpha1 = timer;
                DeathPanel.GetComponentInChildren<Text>().DOColor(new Color32(255, 255, 255, 255), 0.7f);
            }
            else
            {
                alpha1 = (255 / timer);
            }
            DeathPanel.GetComponent<Image>().DOColor(new Color32(0, 0, 0, (byte)alpha1), 0.5f);
        }

        /*public void UnSeenPlayerUI(bool seen)
        {
            if(seen == true)
            {
                transform.GetComponentInChildren<>
            }
        }*/

        IEnumerator ExecuteAfterTime(float time, Text t)
        {
            yield return new WaitForSeconds(time);

            t.color = new Color32(0, 0, 0, 0);
        }

        /*IEnumerator InfoAfterTime(float time, Text t)
        {
            yield return new WaitForSeconds(time);

            t.text = "";
        }*/
    }
}

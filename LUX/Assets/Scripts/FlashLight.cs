using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Kawaiisun.SimpleHostile
{
    public class FlashLight : MonoBehaviour
    {

        private Light flashlight;
        private bool isOn = false;
        public Equipment selectionFlashlight;
        public ObjectsManagement obj;
        public float currentBatteryEnergy;
        public float dischargeBatteryVelocity = 0.5f;
        private SupportScriptResources ssr;

        float startIntensity;

        public UIScript UI;

        private void Awake()
        {
            UI = GameObject.Find("CanvasUI").GetComponent<UIScript>();
            if (UI == null)
                Debug.Log("not found UI from flashlight");
            //Debug.Log(UI.name);
        }

        private void Start()
        {
            ssr = FindObjectOfType<SupportScriptResources>();
            obj = FindObjectOfType<ObjectsManagement>();
            flashlight = this.GetComponent<Light>();
            currentBatteryEnergy = 10f;

            startIntensity = flashlight.intensity;
            isOn = true;
        }
        void Update()
        {
            if (obj.getCurrentObj() == null)
                return;

            if (Input.GetKeyDown(KeyCode.R) && selectionFlashlight.isSelected == true)
            {

                if (obj.ammo[2] > 0)
                {
                    Debug.Log("Ricarica!");
                    currentBatteryEnergy = selectionFlashlight.charge;
                    obj.ammo[2]--;

                    UI.UpdateResources("Battery", -1);
                }
                else
                {
                    Debug.Log("No Batteries left!");
                }
                
            }
            if (selectionFlashlight.isSelected == true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    isOn = !isOn;

                    if (isOn)
                    {
                        currentBatteryEnergy = ssr.GetRemainEnergy();

                    }
                    else
                        ssr.SetRemainEnergy(currentBatteryEnergy);
                }
                if (isOn)
                {

                    if (currentBatteryEnergy >= 0)
                    {
                        flashlight.enabled = true;
                        currentBatteryEnergy -= dischargeBatteryVelocity * Time.deltaTime;

                        //flicker

                        //Debug.Log(currentBatteryEnergy + " + " + flashlight.intensity + " + " + currentBatteryEnergy/20f);
                        if ((currentBatteryEnergy / 20f) < /*1.5f*/ startIntensity)
                        {
                            flashlight.intensity = Random.Range(Random.Range((currentBatteryEnergy / 20f), /*1.5f*/ startIntensity), /*1.5f*/ startIntensity);
                        }

                        //end flicker
                    }
                    else 
                    {
                        flashlight.enabled = false;
                        
                    }
                        

                }
                else
                {
                    flashlight.enabled = false;
                    
                } 
            }
            else
            {
                flashlight.enabled = false;
                ssr.SetRemainEnergy(currentBatteryEnergy);
            }

            
           
        }
    }
}
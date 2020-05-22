using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class PickObjects : MonoBehaviour
    {

        public ObjectsManagement obj;

        public UIScript UI;
      

        void Update()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 4, Color.yellow);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4))
            {
                if (hit.collider.gameObject.tag == "Equipment")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Hi raccolto un oggetto!");
                        obj.PickEquipment(hit.collider.gameObject.name);
                        Destroy(hit.collider.gameObject);
                    }

                    UI.UpdateInfo("E");
                }
            }
            else
            {
                UI.UpdateInfo("");
            }
        }


    }

}

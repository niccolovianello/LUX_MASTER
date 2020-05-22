using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    
    public class PlayerPickUpResources : MonoBehaviour
    {
        public int Oil = 1;
        public int Matches = 5;
        public int Batteries = 1;
        public int Arrows = 1;
        public ObjectsManagement obj;

        public UIScript UI;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.tag == "Arrows")
            {
                obj.ammo[3] = obj.ammo[3] + Arrows;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);

                UI.UpdateResources(hit.gameObject.name, Arrows);
            }
            if (hit.gameObject.tag == "Battery")
            {
                Debug.Log(hit.gameObject.name);
                obj.ammo[2] = obj.ammo[2] + Batteries;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);

                UI.UpdateResources(hit.gameObject.name, Batteries);
            }

            if (hit.gameObject.tag == "Oil")
            {
                obj.ammo[1] = obj.ammo[1] + Oil;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);

                UI.UpdateResources(hit.gameObject.name, Oil);
            }
            if (hit.gameObject.tag == "Matches")
            {
                obj.ammo[0] = obj.ammo[0] + Matches;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);

                UI.UpdateResources(hit.gameObject.name, Matches);
            }
            if (hit.gameObject.tag == "HealthObject")
            {
                obj.ammo[4] = obj.ammo[4] + 1;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);

                UI.UpdateResources(hit.gameObject.name, 1);
            }


        }

    }

}


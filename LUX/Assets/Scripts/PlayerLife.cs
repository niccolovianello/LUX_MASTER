using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Com.Kawaiisun.SimpleHostile
{

    public class PlayerLife : MonoBehaviour
    {
        float timeLeft = 30f;
        float plCurrentTime;

        public ObjectsManagement obj;

        public UIScript UI;

        // Start is called before the first frame update
        void Start()
        {
            plCurrentTime = timeLeft;
            obj = FindObjectOfType<ObjectsManagement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (obj.getCurrentObj() != null)
            {
                Light objLight = obj.getCurrentObj().GetComponentInChildren<Light>();

                if (objLight.enabled == true)
                {
                    if (plCurrentTime < 30f && plCurrentTime > 0f)
                    {
                        plCurrentTime += Time.deltaTime;

                        UI.TimerDarkUI(plCurrentTime);
                    }
                }
                else if(objLight.enabled == false)
                {
                    //if(plCurrentTime > 0f)
                    //{
                        plCurrentTime -= Time.deltaTime;

                        UI.TimerDarkUI(plCurrentTime);
                    //}
                }
            }

            //Debug.Log(Mathf.Round(plCurrentTime));
            if(plCurrentTime < 0f)
            {
                Die();

            }

        }

        void Die()
        {
            //Debug.Log("death_darkness");
            UI.TimerDarkUI(255);

            StartCoroutine(ExecuteAfterTime(1f)); //delete and destroy gameobject?
        }

        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            Debug.Log("i'm_dead");
            Destroy(gameObject);
            SceneManager.LoadScene("SampleScene");
        }
    }
}

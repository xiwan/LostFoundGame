using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostAndFound
{
    public class LightTrigger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void TurnOnTorch()
        {
            string objname = this.transform.parent.parent.name;
            int i = int.Parse(objname.Split('-')[0]);
            int j = int.Parse(objname.Split('-')[1]);

            string path = "Prefab/TorchBlue";
            
            //Debug.Log("xxxxxxxxxxxxxx" + GameManager.heart[i,j]);
            if (GameManager.heart[i,j] == 1)
            {
                path = "Prefab/TorchBright";
                AudioManager.Instance.PlaySound("Sounds/Player/Junkomory Click 22");
                GameObject.Find("Maze").SendMessage("DislayTorchStat", true);  
            }
            else
            {
                AudioManager.Instance.PlaySound("Sounds/Player/Junkomory Click 20");
                GameObject.Find("Maze").SendMessage("DislayTorchStat", false);  
            }

            GameObject torchPrefab = Resources.Load<GameObject>(path);
            GameObject torch = Instantiate(torchPrefab);
            torch.transform.parent = this.transform.parent.transform;

            torch.transform.localPosition = new Vector3(0, 0, 0);
            torch.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);    
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log(this.transform.parent.childCount);
                if (this.transform.parent.childCount == 1)
                {
                    TextHelper.Instance.FadeIn();
                    GameObject.FindGameObjectWithTag("Player").SendMessage("SetPressEOk", true);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().TriggerLighter = this.transform.gameObject;
                    //PlayerMove.Instance.SetPressEOk(true);
                    //PlayerMove.Instance.TriggerLighter = this.transform.gameObject;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Player").SendMessage("SetPressEOk", false);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().TriggerLighter = null;
                    //PlayerMove.Instance.SetPressEOk(false);
                    //PlayerMove.Instance.TriggerLighter = null;
                }
            } 
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TextHelper.Instance.FadeOut();
                GameObject.FindGameObjectWithTag("Player").SendMessage("SetPressEOk", false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().TriggerLighter = null;
            } 
        }
    }


}
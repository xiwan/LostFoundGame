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

        public void TurnOnTorch(Color32 color)
        {
            Debug.Log("xxxxxxxxxxxxxx" + color);

            string objname = this.transform.parent.parent.name;
            int i = int.Parse(objname.Split('-')[0]);
            int j = int.Parse(objname.Split('-')[1]);

            string path = "Prefab/TorchBlue";
            
            if(this.transform.parent.childCount == 2)
            {
                if (GameManager.heart[i,j] == 0)
                {
                    AudioManager.Instance.PlaySound("Sounds/Player/Junkomory Click 20");  
                    this.transform.parent.Find("Torch").GetComponentInChildren<Light>().color = color;                
                }
            }
            else if(this.transform.parent.childCount == 1)
            {
                if (GameManager.heart[i,j] == 1)
                {
                    path = "Prefab/TorchBright";
                    AudioManager.Instance.PlaySound("Sounds/Player/Junkomory Click 22");                 
                }
                else
                {
                    AudioManager.Instance.PlaySound("Sounds/Player/Junkomory Click 20");  
                }

                GameObject torchPrefab = Resources.Load<GameObject>(path);
                GameObject torch = Instantiate(torchPrefab);
                torch.transform.parent = this.transform.parent.transform;
                if (GameManager.heart[i,j] == 0)
                {
                    torch.GetComponentInChildren<Light>().color = color;                
                }
                torch.transform.localPosition = new Vector3(0, 0, 0);
                torch.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);   
            }
            
            GameObject.Find("Maze").SendMessage("DislayTorchStat", true);  
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log(this.transform.parent.childCount);
                GameObject.FindGameObjectWithTag("Player").SendMessage("SetPressEOk", true);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().TriggerLighter = this.transform.gameObject;
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
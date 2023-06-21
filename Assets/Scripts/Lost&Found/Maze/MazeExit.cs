using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostAndFound
{
    public class MazeExit : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnTriggerEnter(Collider other) 
        {
            if (other.CompareTag("Player"))
            {
                GameObject.Find("Maze").SendMessage("End");  
            } 
        }
    }

}


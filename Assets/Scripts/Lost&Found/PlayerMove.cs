using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace LostAndFound
{
    public class PlayerMove : MonoBehaviour
    {

        public float speed = 1.0F;
        public GameObject trail;
        public float smoothing = 1f;
        public GameObject TorchPre;

        private bool pressEisOK = false;

        public GameObject TriggerLighter = null;
        public GameObject TriggerBox = null;
        private GameObject playerView;
        private Rigidbody rigidbody;
        private LineRenderer line;
        private int i;
        Vector3 RunStart;
	    Vector3 RunNext;
        Damageable damageable;

		private void Awake()
		{
            damageable = GetComponent<Damageable> ();
		}

		// Start is called before the first frame update
		void Start()
        {
            playerView = GameObject.Find("PlayerView");
            rigidbody = transform.GetComponent<Rigidbody>();
            // GameObject trailClone = (GameObject)Instantiate(trail, this.transform.position, this.transform.rotation);
            // line = trailClone.GetComponent<LineRenderer>();
        }

        private void MoveToPos(Vector3 pos)
        {
            this.transform.position = pos;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // 防止回弹
            if (rigidbody != null)
            {
                rigidbody.velocity = new Vector3(0, 0, 0);
            }

            if (Input.GetKey("space"))
            {

                //Debug.Log("xxxxxxxx space " + pressEisOK);
                if (pressEisOK && TriggerLighter!= null && TriggerLighter.activeSelf)
                {
                    TriggerLighter.SendMessage("TurnOnTorch");
                }
                else if(pressEisOK && TriggerBox!= null && TriggerBox.activeSelf)
                {
                    TriggerBox.SendMessage("OpenBox");
                }
                else
                {
                   // Debug.Log("NotOK");
                }
            }
            else if (Input.GetKeyDown("m"))
            {
                GameObject.Find("Maze").SendMessage("Pause"); 
            }
            else if (Input.GetKey("up") || Input.GetKey("w") )
            {
                this.transform.Translate(0, 0, speed*Time.deltaTime);
                //AudioManager.Instance.PlaySound("Sounds/Player/14664");
                AudioManager.Instance.PlaySound("Sounds/Player/sneaker-shoe-on-concrete-floor-medium-pace-1");
                
                TextHelper.Instance.HideInit();
            }
            else if (Input.GetKey("down") || Input.GetKey("s"))
            {
                this.transform.Translate(0, 0, -speed * Time.deltaTime);
                //AudioManager.Instance.PlaySound("Sounds/Player/14664");
                AudioManager.Instance.PlaySound("Sounds/Player/sneaker-shoe-on-concrete-floor-medium-pace-1");
                
                TextHelper.Instance.HideInit();
            }
            else if (Input.GetKey("left") || Input.GetKey("a"))
            {
                //this.transform.Translate(-speed * Time.deltaTime, 0, 0);
                Rotate(this.transform, -5f, 0, smoothing * Time.deltaTime );
            }
            else if (Input.GetKey("right") || Input.GetKey("d"))
            {
                //this.transform.Translate(speed * Time.deltaTime, 0, 0);
                Rotate(this.transform, 5f, 0, smoothing * Time.deltaTime);
            }
            else 
            {
                AudioManager.Instance.StopSound();
            }

            // RunNext = this.transform.position;
    
            // if (RunStart != RunNext) {
            //     i++;
            //     line.SetVertexCount(i);//设置顶点数
            //     line.startColor = Color.black;
            //     line.endColor = Color.red; 
            //     line.SetPosition(i-1, this.transform.position);
    
            // }
    
            // RunStart = RunNext;

        }

        public void SetPressEOk(bool flag)
        {
            this.pressEisOK = flag;
        }

        public void Rotate(Transform transform, float horizontal , float vertical , float fRotateSpeed)
        {
            Vector3 targetDir = new Vector3(horizontal , 0 , vertical);
            if(targetDir != Vector3.zero)
            {
                // Quaternion q1 = Quaternion.identity;  
                //  q1.SetLookRotation(targetDir);  
                // transform.rotation = Quaternion.Lerp(transform.rotation, q1, fRotateSpeed);
                //transform.LookAt(targetDir, Vector3.up);
                //Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.y + horizontal, 0);
                //Quaternion targetRotation = Quaternion.AngleAxis(horizontal, Vector3.up); 
                //Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                //Quaternion targetRotation =   Quaternion.Euler(targetDir);
                //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, fRotateSpeed);

                transform.Rotate(Vector3.up, horizontal);
            }
        }

        public void onHit (int damage, Vector3 knockback) {
            Debug.Log ("damaged");
        }

		public void onDeath () {
			Debug.Log ("onDeath");
            SceneManager.LoadScene ("GameOver");
		}


		public void onHealthChanged (int heal) {
			Debug.Log ("onHeal");
		}
	}
}


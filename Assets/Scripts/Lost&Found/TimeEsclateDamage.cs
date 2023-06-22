using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEsclateDamage : MonoBehaviour
{
    float esclateTime = 0;
    float sinceDamagedTime = 0f;
    public float damageInterval = 5f;
    public Damageable damageable;
    int damage = 1;
	// Start is called before the first frame update
	void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        esclateTime += Time.deltaTime;
        if(esclateTime - sinceDamagedTime > damageInterval) {
            //Debug.Log ("Damaged "+damage +" point");
            damageable.Damage (damage);

            sinceDamagedTime = esclateTime;
		}

	}
}

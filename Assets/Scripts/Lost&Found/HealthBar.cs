using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace LostAndFound {
	public class HealthBar : MonoBehaviour {
		// Start is called before the first frame update
		public Slider healthSlider;
		//public TMP_Text healthBarText;
		Damageable playerDamageble;
		public TextMeshProUGUI textMeshPro;
		float esclateTime = 0f;


		private void Awake () {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			if (player == null) {
				Debug.Log ("No player found in the scence");
			}
			playerDamageble = player.GetComponent<Damageable> ();
		}

		void Start () {

			healthSlider.value = CalSliderPercentage ();
			//healthBarText.text = "HP " + playerDamageble.Health + " / " + playerDamageble.MaxHealth;

		}

		private float CalSliderPercentage () {
			return (float)playerDamageble.Health / (float)playerDamageble.MaxHealth;
		}

		private void OnEnable () {
			playerDamageble.healthChanged.AddListener (OnPlayerHeanthChanged);
		}

		private void OnDisable () {
			playerDamageble.healthChanged.RemoveListener (OnPlayerHeanthChanged);
		}

		// Update is called once per frame
		void Update () {
			esclateTime += Time.deltaTime;
			textMeshPro.text = "Time Esclate: " +
				String.Format ("{0:00}", Math.Floor (esclateTime / 3600)) + ":" +
				String.Format ("{0:00}", Math.Floor (esclateTime % 3600 / 60)) + ":" +
				String.Format ("{0:00}", Math.Floor (esclateTime % 60));
		}

		private void OnPlayerHeanthChanged (int newHealth, int maxHealth) {
			healthSlider.value = CalSliderPercentage ();
			//healthBarText.text = "HP " + playerDamageble.Health + " / " + playerDamageble.MaxHealth;

		}
	}
}
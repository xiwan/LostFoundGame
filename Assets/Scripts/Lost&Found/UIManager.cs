using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace LostAndFound {
	public class UIManager : MonoBehaviour {
		public GameObject damageTextPrefab;
		public GameObject HealthTextPrefab;

		public Canvas gameCanvas;

		private void Awake () {
			gameCanvas = FindObjectOfType<Canvas> ();
		}

		private void OnEnable () {
			CharacterEvents.charaterDamaged += CharacterTookDamage;
			CharacterEvents.charaterHealed += CharacterHealed;
		}

		private void OnDisable () {
			CharacterEvents.charaterDamaged -= CharacterTookDamage;
			CharacterEvents.charaterHealed -= CharacterHealed;
		}

		public void CharacterTookDamage (GameObject character, int damageReceived) {
			Vector3 spawnPosition = new Vector3(210, gameCanvas.pixelRect.height - 60, 0);
			TMP_Text tmpText = Instantiate (damageTextPrefab,
				spawnPosition,
				Quaternion.identity,
				gameCanvas.transform)
				.GetComponent<TMP_Text> ();
			tmpText.text = damageReceived.ToString();
		}

		public void CharacterHealed (GameObject character, int healthRestored) {
			Vector3 spawnPosition = new Vector3 (210, gameCanvas.pixelRect.height - 60, 0);
			TMP_Text tmpText = Instantiate (HealthTextPrefab,
				spawnPosition,
				Quaternion.identity,
				gameCanvas.transform)
				.GetComponent<TMP_Text> ();
			tmpText.text = healthRestored.ToString ();
		}
	}
}
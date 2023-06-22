using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LostAndFound {
	public class LoadScene : MonoBehaviour {
		// Start is called before the first frame update
		void Start () {

		}

		public static void onClick (string sceneName) {
			SceneManager.LoadScene (sceneName);
		}
	}
}

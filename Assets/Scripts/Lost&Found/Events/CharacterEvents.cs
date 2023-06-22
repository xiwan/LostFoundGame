using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LostAndFound {
	public class CharacterEvents {
		public static UnityAction<GameObject, int> charaterDamaged;
		public static UnityAction<GameObject, int> charaterHealed;
	}
}
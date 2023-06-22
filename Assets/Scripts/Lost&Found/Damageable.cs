using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour {
	public UnityEvent<int> damageableHit;
	public UnityEvent<int, int> healthChanged;
	public UnityEvent damageableDeath;
	[SerializeField]
	private int _maxHealth;
	[SerializeField]
	private int _health = 100;
	[SerializeField]
	private bool _isAlive = true;

	private float timeSinceDamaged = 0;
	public float invincibilityTimer = 1f;
	private bool isInvincible = false;


	public int Health {
		get => _health;
		set {
			_health = value;
			healthChanged?.Invoke (_health, MaxHealth);
			if (_health <= 0) {
				IsAlive = false;
			}
		}
	}

	public int MaxHealth { get => _maxHealth; set { _maxHealth = value; } }

	public bool IsAlive {
		get => _isAlive;
		private set {
			_isAlive = value;
			if (!value) {
				damageableDeath.Invoke ();
			}
		}
	}

	private void Awake () {
		
	}

	private void Start()
	{
		Health = MaxHealth;
	}

	private void Update () {
		if (isInvincible) {
			if (timeSinceDamaged > invincibilityTimer) {
				isInvincible = false;
				timeSinceDamaged = 0;
			}

			timeSinceDamaged += Time.deltaTime;
		}

		if (!IsAlive) {
			isInvincible = true;
		}
	}

	public bool Damage (int damage) {
		if (IsAlive) {
			if (!isInvincible) {
				Health -= damage;
				isInvincible = true;


				damageableHit?.Invoke (damage);


				return true;
			}
		}
		return false;
	}

	public bool Heal (int healthRestore) {
		if (IsAlive && Health < MaxHealth) {

			int maxHeal = Mathf.Max (MaxHealth - Health, 0);
			int actualHeal = Mathf.Min (maxHeal, healthRestore);

			Health += actualHeal;

			return true;
		}

		return false;
	}
}

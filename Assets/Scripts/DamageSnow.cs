using UnityEngine;
using System.Collections;

public class DamageSnow : MonoBehaviour {

	public float voidDamageHeight = -50.0f;
	public float voidDamage = 1.0f;
	public float touchDamage = 0.4f;

	private Spawn spawn;
	private SnowballController snowballController;

	void Start () {
		spawn = GameObject.Find("Spawn").GetComponent<Spawn>();
		snowballController = this.GetComponent<SnowballController>();
	}

	void Update () {
		if (snowballController.transform.position.y < this.voidDamage) {
			Damage(voidDamage);
		}
	}

	void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.tag == "Enemy") {
			Damage(touchDamage);
		}
		
	}

	void Damage(float damage) {
		snowballController.size -= damage;
		if (snowballController.size > 0) {
			snowballController.updateSize ();
		} else {
			spawn.spawnSnowball();
			Destroy(snowballController.gameObject);
		}
	}
}

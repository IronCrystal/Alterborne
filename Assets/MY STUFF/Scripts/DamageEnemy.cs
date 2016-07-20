using UnityEngine;
using System.Collections;

public class DamageEnemy : MonoBehaviour {
    private int countSinceDamageTaken = 0;
    public int timeBetweenDamage;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            GameObject thePlayer = GameObject.Find("player");
            Stats playerStats = thePlayer.GetComponent<Stats>();
            Stats enemyStats = other.gameObject.GetComponent<Stats>();
            playerStats.currentHP--;
            enemyStats.currentHP--;
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            if (countSinceDamageTaken >= timeBetweenDamage) {
                GameObject thePlayer = GameObject.Find("player");
                Stats playerStats = thePlayer.GetComponent<Stats>();
                Stats enemyStats = other.gameObject.GetComponent<Stats>();
                playerStats.currentHP--;
                enemyStats.currentHP--;
                countSinceDamageTaken = 0;
            } else {
                countSinceDamageTaken++;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        countSinceDamageTaken = 0;
    }
}

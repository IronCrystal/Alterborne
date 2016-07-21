using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DamageEnemy : MonoBehaviour {
    
    public float cooldown;
    float cd;
    RaycastHit2D hit;
	GameObject[] enemiesHit;
	int enemiesHitLength;
    int layerPlayer = 8;
    int raycastLayer;

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.forward);
    Quaternion stepAngle = Quaternion.AngleAxis(12, Vector3.forward);

	public Stats playerStats;
	Animator playerAnim;
	int playerDirection;
	Quaternion attackDirection;
	bool sameEnemy;
    // Use this for initialization
    void Start () {
        cd = cooldown;
        cooldown = 0;
		enemiesHit = new GameObject[10];
		enemiesHitLength = 0;
        layerPlayer = 1 << layerPlayer;
        raycastLayer = ~layerPlayer;
		sameEnemy = false;
		playerAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
        cooldown -= Time.deltaTime;
		attackDirection = new Quaternion ();
	}

    public void attackCone()
    {
        if (cooldown <= 0)
        {   
			playerDirection = playerAnim.GetInteger ("Direction");
			//Debug.Log (playerDirection);
			if (playerDirection == 1)
				startingAngle = Quaternion.AngleAxis(30, Vector3.forward);
			else if (playerDirection == 2)
				startingAngle = Quaternion.AngleAxis(120, Vector3.forward);
			else if (playerDirection == 3)
				startingAngle = Quaternion.AngleAxis(-150, Vector3.forward);
			else
				startingAngle = Quaternion.AngleAxis(-60, Vector3.forward);
			
			//Debug.Log (transform.rotation);
			//Quaternion angle = transform.rotation * startingAngle;
            Vector3 direction = startingAngle * Vector3.right;

            Vector3 pos = transform.position;
            for (int i = 0; i < 11; i++)
            {
				Debug.Log (direction);
                Debug.DrawRay(pos, direction, Color.red, 3.0f);
                hit = Physics2D.Raycast(pos, direction, 1.0f, raycastLayer);

                if (hit.collider != null){
                    //Debug.Log(hit.collider.name);
                    if (hit.collider.tag == "Enemy")
                    {
						sameEnemy = false;
						for (int e = 0; e < enemiesHitLength; e++) {
							//Debug.Log (enemiesHitLength);
							if (enemiesHit [e].Equals(hit.collider.gameObject)) {
								sameEnemy = true;
								//Debug.Log ("same");
							}
						}
						if (!sameEnemy){
							
							enemiesHit [enemiesHitLength] = hit.collider.gameObject;
							enemiesHitLength++;
	                        //GameObject thePlayer = GameObject.Find("player");
	                        Stats playerStats = GetComponent<Stats>();
	                        float damageDealt = playerStats.damageDealt;
	                        Stats enemyStats = hit.collider.gameObject.GetComponent<Stats>();
	                        float armor = enemyStats.armor;

							int damage = 1;
							//if (playerStats.entityClass == Stats.Class.Warrior)
	                        	//damage = Mathf.RoundToInt(damageDealt - armor);
							//else if (playerStats.entityClass == Stats.Class.Sorcerer)
								damage = Mathf.RoundToInt(damageDealt - armor);
							
	                        if (damage <= 0)
	                            damage = 1;
	                        enemyStats.currentHP -= damage;
	                        //Debug.Log(damage);
						}
                    }
                }
                direction = stepAngle * direction;

            }          
            cooldown = cd;
        }

    }

    /*void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            GameObject thePlayer = GameObject.Find("player");
            Stats playerStats = thePlayer.GetComponent<Stats>();
            Stats enemyStats = other.gameObject.GetComponent<Stats>();
            playerStats.currentHP--;
            enemyStats.currentHP--;
        }
    }*/

    /*void OnCollisionStay2D(Collision2D other) {
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
    }*/
}

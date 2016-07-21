using UnityEngine;
using System.Collections;

public class DamageEnemy : MonoBehaviour {
    
    public float cooldown;
    float cd;
    RaycastHit2D hit;

    int layerPlayer = 8;
    int raycastLayer;

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.forward);
    Quaternion stepAngle = Quaternion.AngleAxis(24, Vector3.forward);

    // Use this for initialization
    void Start () {
        cd = cooldown;
        cooldown = 0;
        layerPlayer = 1 << layerPlayer;
        raycastLayer = ~layerPlayer;
	}
	
	// Update is called once per frame
	void Update () {
        cooldown -= Time.deltaTime;
	}

    public void attackCone()
    {
        if (cooldown <= 0)
        {            
            Quaternion angle = transform.rotation * startingAngle;
            Vector3 direction = angle * Vector3.right;
            Vector3 pos = transform.position;
            for (int i = 0; i < 5; i++)
            {
                Debug.DrawRay(pos, direction, Color.red, 3.0f);
                hit = Physics2D.Raycast(pos, direction, 1.0f, raycastLayer);

                if (hit.collider != null){
                    //Debug.Log(hit.collider.name);
                    if (hit.collider.tag == "Enemy")
                    {
                        GameObject thePlayer = GameObject.Find("player");
                        Stats playerStats = thePlayer.GetComponent<Stats>();
                        float strength = playerStats.strength;
                        Stats enemyStats = hit.collider.gameObject.GetComponent<Stats>();
                        float armor = enemyStats.armor;

                        int damage = Mathf.RoundToInt(strength - armor);
                        if (damage <= 0)
                            damage = 1;
                        enemyStats.currentHP -= damage;
                        //Debug.Log(damage);
                        break;
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

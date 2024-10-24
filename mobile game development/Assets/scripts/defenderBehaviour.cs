using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defenderBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public int cost = 20;
    public int Damage = 2;
    public float coolDown = 0.5f;
    [SerializeField] private float wait = 0.0f; 

    private void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Enemy")
        {
            Debug.Log("attacking");
            if (wait <= 0.0f)
            {
                attack(coll.gameObject);
            }
        }
    }

    void attack(GameObject enemy)
    {
        enemy.GetComponent<enemyBehaviour>().takeDamage(Damage);
        wait = coolDown;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wait > 0.0f)
        {
            wait -= Time.deltaTime;
        }
    }
}

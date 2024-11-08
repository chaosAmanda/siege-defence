using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    public int health = 10;
    public float speed = 2;
    public int bounty = 10;

    public GameObject Manager;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(new Vector3(0, this.transform.position.y, 0));
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    void death(bool killed)
    {
        if (killed) { 
            Destroy(gameObject);
            Manager.GetComponent<GameManager>().GainMoney(bounty);
        }
        else if (!killed)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (speed * Time.deltaTime), this.transform.position.z);
        if (this.transform.position.y > 4.5)
        {
            death(false);
        }
        if (health <= 0)
        {
            death(true);
        }
    }
}

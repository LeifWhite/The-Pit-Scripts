using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using Unity.MLAgents;

using UnityEngine;
using UnityEngine.AI;

public class zombie : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform zombieTarget;

    public GameObject model;
    Animator anim;

    [SerializeField] private float health;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        anim = model.GetComponent<Animator>();

      float distance = Vector3.Distance(transform.position, zombieTarget.position);

        if (zombieTarget != null && distance > 2)
        {
            agent.SetDestination(zombieTarget.position);
            anim.SetBool("walking", true);

            Target t = zombieTarget.GetComponent<Target>();

            if (t != null)
            {
                t.TakeDamage(10);
            }
        }
        else
        {
            anim.SetBool("walking", false);
            agent.SetDestination(this.transform.position);
        }
    }

    public void takeDamage(float damage)
	{
        health -= damage;

        if(health < 0)
		{
            GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(go, 4);
		}
	}
}

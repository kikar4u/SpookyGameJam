using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 target;
    PlayerController player;
    private NavMeshAgent agent;
    private bool continueCoroutine = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getPlayerPosition());
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target);
        //Debug.Log("is hiding" + player.IsHiding);
    }
    public void GetPlayerPosition()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    IEnumerator getPlayerPosition()
    {
        while (continueCoroutine)
        { //variable that enables you to kill routine
            //Debug.Log("OnCoroutine: " + Time.time + target);
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsHiding)
            {
                target = RandomNavmeshLocation(Random.Range(8.0f, 20.0f));
            }
            else
            {
                target = RandomNavmeshLocation(Random.Range(8.0f, 20f));
            }

            yield return new WaitForSeconds(Random.Range(2.0f, 7.0f));




        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GameOver(true);
        }
    }



}

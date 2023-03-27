using UnityEngine;
using UnityEngine.AI;

public class TestAI : MonoBehaviour
{
    public float distance = 2.0f; // Düþmanlarýn diðer düþmanlara olan en yakýn mesafesi
    public float attackDistance = 5.0f; // Düþmanlarýn oyuncuya veya diðer hedeflere saldýrmaya baþlayacak mesafesi

    private NavMeshAgent agent;
    private Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = FindNearestTarget();
    }

    void Update()
    {
        // Hedefe doðru hareket et
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    Transform FindNearestTarget()
    {
        // Tüm düþmanlarý bul
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy_AI");

        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        // En yakýn hedefi bul
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < nearestDistance && distanceToEnemy > distance)
            {
                nearestDistance = distanceToEnemy;
                nearestTarget = enemy.transform;
            }
        }

        // En yakýn hedef oyuncu mu yoksa baþka bir düþman mý?
        if (nearestTarget == null || nearestDistance > attackDistance)
        {
            nearestTarget = FindPlayer();
        }

        return nearestTarget;
    }

    Transform FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            return player.transform;
        }

        return null;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Eðer çarpýþma diðer bir düþmanla olduysa, dur
        if (collision.gameObject.tag == "Enemy_AI")
        {
            agent.isStopped = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Eðer diðer düþmandan uzaklaþýyorsa, hareketine devam et
        if (collision.gameObject.tag == "Enemy_AI")
        {
            agent.isStopped = false;
        }
    }
}

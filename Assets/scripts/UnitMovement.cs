using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;


    public bool isComandedToMove;

    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)){
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                isComandedToMove = true;
                agent.SetDestination(hit.point);
            }
        }

        //Debug.Log("Distancia Unit: " + agent.remainingDistance);

        if(agent.hasPath == false || agent.remainingDistance <= agent.stoppingDistance)
        {
            isComandedToMove = false;
        }
    }
}

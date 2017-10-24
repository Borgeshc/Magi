using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public static bool canMove;
    public LayerMask movementMask;
    public LayerMask interactable;

    public static bool primaryAttack;
    public static bool secondaryAttack;

    float baseSpeed;

    Camera cam;
    Transform target;
    Interactable focus;
    NavMeshAgent agent;
    PlayerCombat combat;


    private void Start()
    {
        canMove = true;
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<PlayerCombat>();

        baseSpeed = agent.speed;
        Time.timeScale = 1;
    }

    private void Update()
    {
        primaryAttack = Input.GetKey(KeyCode.Mouse0);
        secondaryAttack = Input.GetKey(KeyCode.Mouse1);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            print("StopMoving");
            canMove = false;
        }
        else
        {
            print("StartMoving");
            canMove = true;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(!canMove && primaryAttack || !canMove && secondaryAttack)
            combat.Attack(primaryAttack, target);

        if (Physics.Raycast(ray, out hit, 100, interactable))
        {
            if (primaryAttack || secondaryAttack)
            {
                combat.Attack(primaryAttack, target);

                if (canMove)
                    MoveToPoint(hit.point);

                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
        else if (Physics.Raycast(ray, out hit, 100, movementMask))
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            {
                if (canMove)
                    MoveToPoint(hit.point);

                RemoveFocus();
            }
        }

        if (target != null && canMove)
        {
            agent.SetDestination(target.position);
            agent.speed = baseSpeed;
            FaceTarget();
        }
        //else if (!canMove)
        //{
        //    agent.speed = 0;
        //    agent.SetDestination(transform.position);
        //    agent.velocity = Vector3.zero;
        //}
    }

    void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        if (canMove)
        {
            agent.stoppingDistance = newTarget.radius * .8f;
            agent.updateRotation = false;

            target = newTarget.interactionTransform;
        }
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0;
        agent.updateRotation = true;
        target = null;
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        StopFollowingTarget();
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100f);
    }
}

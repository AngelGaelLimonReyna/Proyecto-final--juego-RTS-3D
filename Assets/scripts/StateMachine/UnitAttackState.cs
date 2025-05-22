using System;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackState : StateMachineBehaviour
{
    NavMeshAgent agent;
    AttackController attackController;

    public float attackTimer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        attackController = animator.GetComponent<AttackController>();
        attackController.SetAttackMaterial();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackController.targetToAttack != null &&
            !animator.transform.GetComponent<UnitMovement>().isComandedToMove)
        {
            LookAtTarget();

            float distanceFromTarget = Vector3.Distance(
                attackController.targetToAttack.position,
                animator.transform.position
            );

            if (distanceFromTarget > attackController.stopAttackingDistance)
            {
                animator.SetBool("isAttacking", false);
                return;
            }

            if (attackTimer <= 0)
            {
                switch (attackController.GetAttackType())
                {
                    case "CortaDistancia":
                        AttackSingleTarget();
                        break;
                    case "InAreaDistancia":
                        AttackAreaTargets();
                        break;
                    case "LargaDistancia":
                        AttackRangedTarget();
                        break;
                }

                attackTimer = 1f / attackController.attackRate;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void LookAtTarget()
    {
        Vector3 direction = attackController.targetToAttack.position - agent.transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            agent.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void AttackSingleTarget()
    {
        if (attackController.targetToAttack != null)
        {
            var damageToInflict = attackController.unitDamage;
            var unit = attackController.targetToAttack.GetComponent<Enemy>();
            if (unit != null)
            {
                unit.TakeDamage(damageToInflict);
            }
        }
    }

    private void AttackAreaTargets()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(
            attackController.transform.position,
            attackController.attackDistance
        );

        foreach (var enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                var unit = enemy.GetComponent<Enemy>();
                if (unit != null)
                {
                    unit.TakeDamage(attackController.unitDamage);
                }
            }
        }
    }

    private void AttackRangedTarget()
    {
        if (attackController.targetToAttack != null && attackController.projectilePrefab != null)
        {
            // Instanciar el proyectil desde la posición del objeto atacante
            GameObject projectile = GameObject.Instantiate(
            attackController.projectilePrefab,
            attackController.transform.position,
            attackController.transform.rotation //usa la rotación de la unidad
            );

            // Inicializar el proyectil con la posición del objetivo y el daño
            proyectilLogica logic = projectile.GetComponent<proyectilLogica>();
            if (logic != null)
            {
                logic.Initialize(attackController.targetToAttack.position, attackController.unitDamage);
            }
        }
    }
}



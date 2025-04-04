using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{

    private float idleTime = 0f; // Tiempo acumulado en el estado idle.

    private float timeToChange = 10f; // Tiempo mínimo para cambiar entre Idle 1 e Idle 2 (en segundos).

    private int currentIdleState = 1; // 1 para Idle 1, 2 para Idle 2.

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entrando en Idle State");
    }

    public override void HandleStateInput(PlayerStateManager player)
    {
        if (player.InputHandler.IsMoving && !player.InputHandler.IsSprinting)
        {
            player.SwitchState(player.walkState);
        }
    }

    public override void UpdateState(PlayerStateManager player) 
    {
        player.Animator.SetInteger("CurrentIdle", currentIdleState);

        // Incrementamos el tiempo que hemos estado en Idle.
        idleTime += Time.deltaTime;

        // Si el tiempo de inactividad supera el tiempo para cambiar de estado, cambiamos el Idle.
        if (idleTime >= timeToChange)
        {
            // Cambiamos aleatoriamente entre Idle 1 y Idle 2.
            currentIdleState = (Random.Range(0, 2) == 0) ? 1 : 2; // Expresión condicional también conocida como operador ternario

            // Reseteamos el temporizador.
            idleTime = 0f;
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Saliendo de Idle State");
        player.Animator.SetBool("IsWalking", true);
    }
}

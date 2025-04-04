using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entrando en Run State");
        player.Animator.SetBool("IsSprinting", true);
    }

    public override void HandleStateInput(PlayerStateManager player)
    {
        if (!player.InputHandler.IsSprinting)
        {
            player.SwitchState(player.walkState);
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {
        Vector3 moveDirection = new Vector3(player.InputHandler.MovementInput.x, 0, player.InputHandler.MovementInput.y);

        if (moveDirection.sqrMagnitude > 0.01f) // Solo rotar si hay movimiento
        {
            player.Controller.Rotate(player.InputHandler.MovementInput);
        }

        player.Controller.Move(moveDirection, true);
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Saliendo de Run State");
        player.Animator.SetBool("IsSprinting", false);
    }
}

using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entrando en Walk State");
        player.Animator.SetBool("IsWalking", true);
    }

    public override void HandleStateInput(PlayerStateManager player)
    {
        if (!player.InputHandler.IsMoving)
        {
            player.SwitchState(player.idleState);
        }
        else if (player.InputHandler.IsMoving && player.InputHandler.IsSprinting)
        {
            player.SwitchState(player.runState);
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {
        Vector3 moveDirection = new Vector3(player.InputHandler.MovementInput.x, 0, player.InputHandler.MovementInput.y);

        if (moveDirection.sqrMagnitude > 0.01f) // Solo rotar si hay movimiento
        {
            player.Controller.Rotate(player.InputHandler.MovementInput);
        }

        player.Controller.Move(moveDirection, false);
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Saliendo de Walk State");
        player.Animator.SetBool("IsWalking", false);
    }
}

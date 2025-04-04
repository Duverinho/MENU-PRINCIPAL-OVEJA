using UnityEngine;

public abstract class PlayerBaseState
{
    public virtual void EnterState(PlayerStateManager player) { }

    public abstract void HandleStateInput(PlayerStateManager player);

    public abstract void UpdateState(PlayerStateManager player);

    public virtual void ExitState(PlayerStateManager player) { }
}

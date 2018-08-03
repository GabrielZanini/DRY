using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitting : PlayerState
{
    public Hitting(PlayerStateMachine player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        player.isAttacking = true;
        player.animator.SetTrigger("Attack");
    }

    public override void Tick()
    {
        if (!player.isAttacking)
        {
            if (player.isGrounded)
            {
                player.Landing();
            }
            else
            {
                player.SetState(new Falling(player));
            }
        }
    }

    public override void FixedTick()
    {

    }

    public override void OnStateExit()
    {
        player.isAttacking = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooking : PlayerState
{
    public Hooking(PlayerStateMachine player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        player.isHooking = true;
        player.animator.SetTrigger("Grab");
    }

    public override void Tick()
    {
        if (!player.isHooking)
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
        else
        {
            if (player.isTouchingWallEdge)
            {
                player.SetState(new Grabbing(player));
            }
        }
    }

    public override void FixedTick()
    {

    }

    public override void OnStateExit()
    {
        player.isHooking = false;
    }

}

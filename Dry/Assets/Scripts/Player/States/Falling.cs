using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : PlayerState
{
    public Falling(PlayerStateMachine player) : base(player)
    {
    }

    public override void OnStateEnter()
    {

    }

    public override void Tick()
    {
        if (player.isGrounded)
        {
            player.Landing();
        }
        else
        {
            if (player.InputJump && player.isUmbrellaOpen)
            {
                player.SetState(new Gliding(player));
            }
            else
            {
                player.Attacking();
            }
        }        
    }

    public override void FixedTick()
    {
        player.Moving();
    }

    public override void OnStateExit()
    {
        if (player.isGrounded)
        {
            player.animator.SetBool("Jumping", false);
        }
    }

}

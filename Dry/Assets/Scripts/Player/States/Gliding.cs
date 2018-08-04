using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : PlayerState
{
    public Gliding(PlayerStateMachine player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        player.isGliding = true;
    }

    public override void Tick()
    {
        if (player.isGrounded)
        {
            player.Landing();
        }
        else if (!player.isUmbrellaOpen || !player.InputJump)
        {
            player.SetState(new Falling(player));
        }
        else
        {
            player.Attacking();
            player.Hooking();
        }       
    }

    public override void FixedTick()
    {
        player.Moving();
        player.rigidbody.AddForce(Vector3.up * player.glidingForce, ForceMode.Acceleration);
    }

    public override void OnStateExit()
    {
        if (player.isGrounded)
        {
            player.animator.SetBool("Jumping", false);
        }

        player.isGliding = false;
    }
}
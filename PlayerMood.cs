using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMood : MonoBehaviour
{
    public Animator animator;

    bool fightBtnDown = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.F))
        {
            animator.SetBool("Anim_Fight", true);
            Player.fight = true;
        }
        */

        if (fightBtnDown)
        {
            animator.SetBool("Anim_Fight", true);
            Player.fight = true;
            fightBtnDown = false;
        }



        if (Player.fight == false)
        {
            animator.SetBool("Anim_Fight", false);
        }
        if (Player.hitGoodStuff)
        {
            animator.SetBool("Anim_HitGoodStuff", true);
        }
        else
        {
            animator.SetBool("Anim_HitGoodStuff", false);
        }

        if (Player.hitBadStuff)
        {
            animator.SetBool("Anim_HitBadStuff", true);
        }
        else
        {
            animator.SetBool("Anim_HitBadStuff", false);
        }


    }

    public void FightBtnDown() //mobile
    {
        fightBtnDown = true;
    }

}

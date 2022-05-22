using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    // Start is called before the first frame update
    private bool comboPossible;
    public int comboStep;
    private bool inputSmash;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ComboPossible()
    {
        comboPossible = true;
    }

    public void NormalAttack()
    {
        if (comboStep == 0)
        {
            anim.Play("Hit1");
            comboStep = 1;
            return;
        }

        if (comboStep != 0)
        {
            if (comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }
    }

    public void NextAtk()
    {
        if (comboStep == 2)
        {
            anim.Play("Hit2");
        }

        if (comboStep == 3)
        {
            anim.Play("Hit3");
        }
    }

    public void ResetCombo()
    {
        comboPossible = false;
        comboStep = 0;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    CharacterContollerScript characterContollerScript;
    Animator anim;

    GameObject backSword;
    GameObject equippedSword;

    void Start()
    {
        characterContollerScript = this.GetComponent<CharacterContollerScript>();
        anim = this.GetComponent<Animator>();

        backSword = GameObject.Find("2Hand-Sword-Back");

        equippedSword = GameObject.Find("2Hand-Sword");
        equippedSword.SetActive(false);
    }

    void WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Y) && anim.GetBool("Armed") == false)
        {
            anim.SetBool("Armed", true);
            //StartCoroutine(DrawSword());
            StartCoroutine(SwordState(true));
        }
        else if (Input.GetKeyDown(KeyCode.Y) && anim.GetBool("Armed") == true)
        {
            anim.SetBool("Armed", false);
            //StartCoroutine(SheathSword());
            StartCoroutine(SwordState(false));
        }
    }

    IEnumerator SwordState(bool active)
    {
        yield return new WaitForSeconds(1.22f);

        equippedSword.SetActive(active);

        Debug.Log("sword drawn");
    }

    IEnumerator DrawSword()
    {
        yield return new WaitForSeconds(1.25f);

        backSword.SetActive(false);
        equippedSword.SetActive(true);

        Debug.Log("sword drawn");
    }
    IEnumerator SheathSword()
    {
        yield return new WaitForSeconds(1.25f);
        
        equippedSword.SetActive(false);
        backSword.SetActive(true);

        Debug.Log("sword sheathed");
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    void Update()
    {
        WeaponSwitch();

        if (Input.GetMouseButton(0) && anim.GetBool("Armed") == true)
        {
            Attack();
        }
    }
}

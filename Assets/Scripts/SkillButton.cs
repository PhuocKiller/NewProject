using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
  public void PlayerDoSkill ()
    {
        PlayerController.instance.PlayerSkill();
    }

   
}

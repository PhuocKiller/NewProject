using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AzillaStudio
{
    public class DemoScript : MonoBehaviour
    {
        public Animator[] anims;
        public Text textAnim;

        public void Idle()
        {
            textAnim.text = "IDLE";
            foreach (Animator anim in anims)
            {
                anim.SetBool("run", false);
                anim.SetBool("die", false);
            }
        }

        public void Run()
        {
            textAnim.text = "RUN";
            foreach (Animator anim in anims)
            {
                anim.SetBool("run", true);
                anim.SetBool("die", false);
            }
        }

        public void Hit()
        {
            textAnim.text = "INJURED";
            foreach (Animator anim in anims)
            {
                anim.SetBool("run", false);
                anim.SetBool("die", false);
                anim.SetTrigger("hit");
            }
        }

        public void Atk01()
        {
            textAnim.text = "ATTACK";
            foreach (Animator anim in anims)
            {
                anim.SetBool("run", false);
                anim.SetBool("die", false);
                anim.SetTrigger("atk_01");
            }
        }       

        public void Die()
        {
            textAnim.text = "DIE";
            foreach (Animator anim in anims)
            {
                anim.SetBool("die", true);
                anim.SetBool("run", false);
            }
        }

    }
}
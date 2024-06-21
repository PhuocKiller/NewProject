using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public CharacterType characterType;
    public SkeletonGraphic skeletonGraphic;
    private void Awake()
    {
        skeletonGraphic = GetComponent<SkeletonGraphic>();
    }
    public void SetupSkinInUI(int level)
    {
       if (level<5)
        {
            skeletonGraphic.Skeleton.SetSkin("Lv1");
        }
       else if (level<10)
        {
            skeletonGraphic.Skeleton.SetSkin("Lv5");
        }
       else { skeletonGraphic.Skeleton.SetSkin("Lv10"); }
    }
}

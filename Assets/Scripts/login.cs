using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    public void ChangeScene()
    {
        // Tải scene mới dựa vào tên scene
        SceneManager.LoadScene(1);
    }
}

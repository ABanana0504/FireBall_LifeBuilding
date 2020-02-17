using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainController : MonoBehaviour
{
   
    public void MainStart()
    {   //加载场景
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        }       
    }


}

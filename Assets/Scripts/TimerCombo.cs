using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCombo : MonoBehaviour
{
    private static TimerCombo _timerCombo;
    public static TimerCombo timerComboC
    {
        get
        {
            return _timerCombo;
        }
    }
    public bool isCombo = false;
   

    private Image imgCombo;             //Combo计时条
    private float duration = 6;         //计时持续时间
    public float timerCombo = 0;        //Combo计时初始时间
    
    

    void Awake()
    {
        _timerCombo = this;
    }

    void Start()
    {
        //Debug.Log(gameObject.tag.ToString());
       
            imgCombo = GetComponent<Image>();
       
    }

    void Update()
    {
       
        if (isCombo)
        {
            timerCombo += Time.deltaTime;
            imgCombo.fillAmount = Mathf.Lerp(1, 0, timerCombo / duration);
            if (timerCombo > duration)
            {
                timerCombo = 0;
                isCombo = false;
            }
        }
        // Debug.Log(timerCoReward);
        
    }

  
}

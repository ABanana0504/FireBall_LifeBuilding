using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCoReward : MonoBehaviour
{
    private static TimerCoReward _timerCoReward;
    public static TimerCoReward timerCoRewardC
    {
        get
        {
            return _timerCoReward;
        }
    }
    public bool isComboRe = false;

    private Image imgCoReward;             //ComboReward计时条
    private float duration = 6;             //计时持续时间
    private float timerCoReward = 0;                //ComboReward计时初始时间

    void Awake()
    {
        _timerCoReward = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        imgCoReward = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isComboRe)
        {
            timerCoReward += Time.deltaTime;
            imgCoReward.fillAmount = Mathf.Lerp(1, 0, timerCoReward / duration);
            if (timerCoReward > duration)
            {
                timerCoReward = 0;
                isComboRe = false;
            }
        }
    }
}

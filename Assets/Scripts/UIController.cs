using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject material;             //获取材料预设
    public GameObject materialHolder;       //存放材料
    public GameObject building;    
    public GameObject imgHospital;
    public Text txtScore;
    public Text txtCombo;
    public Text txtCoReward;
    public Text txtHadHospital;
    public List<Sprite> Hospitals = new List<Sprite>();

    private int score;
    private int combo;
    private int scoreGrade = 1;             //等价ComboReward
    private int i;
    private int j;
    private int x;
    private int y;
    private int preScore;
    private GameObject go;    

    // Start is called before the first frame update
    void Start()
    {
        MakeMaterial();
        //InvokeRepeating("CreateBuilding", 0, 0.5f);
        CreateBuilding();
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (Head.GetHead.isGot)
        {
            MakeMaterial();
            TimerCombo.timerComboC.isCombo = true;
            Head.GetHead.isGot = false;
            Score();
        }
        if (!TimerCombo.timerComboC.isCombo)
        {
            combo = 0;
            txtCombo.text = combo.ToString();
        }
        if (!TimerCoReward.timerCoRewardC.isComboRe)
        {
            scoreGrade = 1;
            txtCoReward.text = "X" + scoreGrade.ToString();
        }
        if (Head.GetHead.isOver)
        {
            Over();
        }
       
    }
    void MakeMaterial()
    {   //随机位置生成材料
        GameObject m = Instantiate(material);
        m.transform.SetParent(materialHolder.transform, false);
        m.transform.localPosition = CreatePosition();     //24*24=576比例        
    }

    Vector3 CreatePosition()
    {
        while (true)
        {
            int xRandom = Random.Range(0, 24);
            int yRandom = Random.Range(0, 24);
            Vector3 vector3Pos = new Vector3(xRandom * 24 + 12, yRandom * 24 + 12, 0);
            if (!IsHadPosition(vector3Pos))
            {
                return vector3Pos;
            }
        }      
    }

    bool IsHadPosition(Vector3 vector3) 
    {        
        int workerNum = Head.GetHead.workerList.Count;
        List<GameObject> kWorkerList = Head.GetHead.workerList;
        for (int k = 0; k < workerNum; k++)
        {
            if (vector3 == kWorkerList[k].transform.localPosition)
            {
                return true;
            }
        }
        return false;
    }

    void Score()
    {
        if (TimerCombo.timerComboC.timerCombo > 0 || combo == 0)
        {
            switch (++combo)
            {
                case 50:
                    scoreGrade = 5;
                    TimerCoReward.timerCoRewardC.isComboRe = true;
                    Head.GetHead.isSlow = true;
                    Head.GetHead.isSlowRepeat = true;
                    break;
                case 30:
                    scoreGrade = 4;
                    TimerCoReward.timerCoRewardC.isComboRe = true;
                    Head.GetHead.isDeleted = true;                     //删除十名工人
                    break;
                case 10:
                    scoreGrade = 3;
                    TimerCoReward.timerCoRewardC.isComboRe = true;                    
                    break;
                case 5:
                    scoreGrade = 2;
                    TimerCoReward.timerCoRewardC.isComboRe = true;                    
                    break;
                default:
                    break;
            }
        }
        else
        {
            combo = 0;
            TimerCombo.timerComboC.isCombo = false;
        }
        TimerCombo.timerComboC.timerCombo = 0;
        score += scoreGrade;
        txtScore.text = score.ToString();
        txtCombo.text = combo.ToString();
        txtCoReward.text = "X" + scoreGrade.ToString();
        if (score / 50 - 1 == preScore)
        {
            txtHadHospital.text = "你已建成医院个数\n" + (++preScore);
            go.GetComponent<Image>().sprite = Hospitals[1];
            CreateBuilding();
        }
    }

    void CreateBuilding()
    {
        x = i++ * 72 + 36;
        y = 72 * j - 36;
        go = Instantiate(building, new Vector3(x, y, 0), Quaternion.identity);
        go.GetComponent<Image>().sprite = Hospitals[0];
        go.transform.SetParent(imgHospital.transform,false);
        if (i == 4)
        {
            j--;
            if (j == -4)
            {
                Debug.Log("Win");
            }
        }        
        i = i % 4;        
    }

    void Over()
    {
        SceneManager.LoadScene(1);
    }

   
}

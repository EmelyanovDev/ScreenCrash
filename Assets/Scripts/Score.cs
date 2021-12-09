
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Animator _menuAnimator;

    [SerializeField] private Text _scoreText;
    public int scoreCount;

    [SerializeField] private Text _recordText;
    private int recordCount;

    public Text _moneyText;

    public Text _secondMoneyText;
    
    public int moneyCount;

    [SerializeField] private Text _complexityText;

    [SerializeField] private Animator _complexityAnim;

    [SerializeField] private ObstacleCreate ObstacleCreate;

    [SerializeField] private AudioSource synthwave;
    
    [SerializeField] private AudioSource coinSound;
    
    [SerializeField] private AudioSource bipSound;

    public GameObject _TouchPad;

    public static bool playMusic;

    [SerializeField] private GameObject dap;

    void Start()
    {
        if (PlayerPrefs.HasKey("Record"))
        {
            recordCount = PlayerPrefs.GetInt("Record");
            
            _recordText.text = ":" + recordCount.ToString();
        }

        if (PlayerPrefs.HasKey("Money"))
        {
            moneyCount = PlayerPrefs.GetInt("Money");

            ChangeMoneyCount(0);
        }
        if (!playMusic)
        {
            synthwave.Stop();
            dap.SetActive(true);
        }
        else
        {
            dap.SetActive(false);
        }
    }

    public void ObstacleAttack(bool isDie)
    {
        if (isDie)
        {
            synthwave.Stop();
            
            if (scoreCount > recordCount)
            {
                recordCount = scoreCount;
                
                _recordText.text = ":" + scoreCount.ToString();
            }
            PlayerPrefs.SetInt("Record", recordCount);

            _TouchPad.SetActive(false);
            
            bipSound.Play();
            
            _menuAnimator.Play("MenuCreate");
            
        }
        else
        {
            scoreCount++;
            
            _scoreText.text = ":" + scoreCount.ToString();

            if (ObstacleCreate.giveMoney)
            {
                ChangeMoneyCount(1);
            }
                

            if (scoreCount > recordCount)
            {
                recordCount = scoreCount;
                
                _recordText.text = ":" + scoreCount.ToString();
            }

            switch (scoreCount)
            {
                case 5:
                    ChangeComplexity("normal", 1);
                    break;
                case 15:
                    ChangeComplexity("hard", 2);
                    break;
                case 25:
                    ChangeComplexity("insane", 3);
                    break;
                case 35:
                    ChangeComplexity("die!!!", 4);
                    break;
            }
        }
    }

    private void ChangeComplexity(string complexity, int level)
    {
        _complexityText.text = complexity;
        
        ObstacleCreate.complexity = level;
        
        _complexityAnim.Play("ComplexityChange");
    }

    public void MusicButton()
    {
        playMusic = !playMusic;
        dap.SetActive(!dap.activeSelf);
    }

    public void ChangeMoneyCount(int count)
    {
        moneyCount += count;
        
        _moneyText.text = ":" + moneyCount.ToString();
        
        _secondMoneyText.text = ":" + moneyCount.ToString();
        
        PlayerPrefs.SetInt("Money", moneyCount);
        
        coinSound.Play();
    }
}

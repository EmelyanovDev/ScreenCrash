using UnityEngine;
using UnityEngine.UI;

public class SkinChange : MonoBehaviour
{

    [SerializeField] private SpriteRenderer[] _objectColors; //для изменения цвета объектов

    [SerializeField] private Text[] _textColors; //для изменения цвета текстов

    public Color[] _skinColors; //цвета скинов,на которые всё меняется

    [SerializeField] private Text[] _skinTexts; //для цены и галочки о покупке

    [SerializeField] private Menu Menu; 

    [SerializeField] private Score Score;

    private bool _nowIsShopOfObstacles;
    

    void Start()
    {
        PlayerPrefs.SetInt("0", 1); //для того, чтобы первый стандартный скин был всегда куплен
        
        for (int i = 0; i < _skinTexts.Length; i++) //проверка на то, какие скини куплены, а какие нет
        {
            if (PlayerPrefs.HasKey(i.ToString()))
            {
                _skinTexts[i].text = "☑";
            }
            else
            {
                _skinTexts[i].text = "10";
            }
        }

        if (PlayerPrefs.HasKey("Skin")) //какой скин был выбран в последний раз и вызывает нужную функцию
        {
            ChangeSkin(PlayerPrefs.GetInt("Skin"));
        }
        
    }

    public void ChangeSkin(int numberOfSkin) //при нажатии на кнопку скина
    {
        if (!_nowIsShopOfObstacles)
        {
            if (PlayerPrefs.HasKey(numberOfSkin.ToString())) //если куплен
            {

                for (int i = 0; i < _objectColors.Length; i++) //меняет цвет объектов
                {
                    _objectColors[i].color = _skinColors[numberOfSkin];
                }

                for (int i = 0; i < _textColors.Length; i++) //меняет цвет текстов
                {
                    _textColors[i].color = _skinColors[numberOfSkin];
                }
                PlayerPrefs.SetInt("Skin", numberOfSkin); //устанавливает выбранный скин
                
                Menu.click.Play(); 
            }
            else
            {
                if (Score.moneyCount >= 10) //если денег достаточно
                {
                    PlayerPrefs.SetInt(numberOfSkin.ToString(), 1); //устанавливает то, что скин куплен
                    
                    Score.ChangeMoneyCount(-10); //отнимает деньги и меняет колво монетов в текстах 
                    
                    _skinTexts[numberOfSkin].text = "☑"; //ставит галочку вместо цены типо куплено
                    
                    Menu.click.Play();
                }
                else
                {
                    Menu.bip.Play();
                }
                
            }
        }
        else
        {
            if (PlayerPrefs.HasKey($"ObstacleColor {numberOfSkin}"))
            {
                PlayerPrefs.SetInt("ObstacleColor", numberOfSkin);
            }
            else
            {
                if (Score.moneyCount >= 10)
                {
                    PlayerPrefs.SetInt($"ObstacleColor {numberOfSkin}", 1);
                    
                    PlayerPrefs.SetInt("ObstacleColor", numberOfSkin);
                    
                    Score.ChangeMoneyCount(-10);

                    _skinTexts[numberOfSkin].text = "☑";
                    
                    Menu.click.Play();
                }
                else
                {
                    Menu.bip.Play();
                }
            }
        }
    }

    public void ChangeShopTypeButton()
    {
        if (_nowIsShopOfObstacles)
        {
            for (int i = 0; i < _skinTexts.Length; i++) //проверка на то, какие скини куплены, а какие нет
            {
                if (PlayerPrefs.HasKey(i.ToString()))
                {
                    _skinTexts[i].text = "☑";
                }
                else
                {
                    _skinTexts[i].text = "10";
                }
            }
        }
        else
        {
            for (int i = 0; i < _skinTexts.Length; i++)
            {
                if (PlayerPrefs.HasKey($"ObstacleColor {i}"))
                {
                    _skinTexts[i].text = "☑";
                }
                else
                {
                    _skinTexts[i].text = "10";
                }
            }
        }

        _nowIsShopOfObstacles = !_nowIsShopOfObstacles;

    }


    
}

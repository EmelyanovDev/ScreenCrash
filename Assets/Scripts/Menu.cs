using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Menu : MonoBehaviour
{
    [SerializeField] private Animator _menuAnim;

    [SerializeField] private PlayerSprite PlayerSprite;

    [SerializeField] private ObstacleCreate ObstacleCreate;

    [SerializeField] private Animator shopAnim;

    [SerializeField] private Score Score;

    [SerializeField] private GameObject shop;

    [SerializeField] private GameObject menu;

    [SerializeField] private GameObject _adsButton;

    [SerializeField] private GameObject _pauseNo;
    [SerializeField] private GameObject _pauseYes;

    public AudioSource click, bip;

    private void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4216375");
        }
    }

    public void RestartButton()
    {
        click.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AdsButton()
    {
        if (Advertisement.IsReady())
        {
            click.Play();
            
            Advertisement.Show("Rewarded_Android");
            
            _menuAnim.Play("MenuIdle");
            
            ObstacleCreate.gameIsEnd = false;
            
            Time.timeScale = 0f;

            StartCoroutine(ObstacleCreate.CreateObstacle());

            StartCoroutine(CountDown());

            Score._TouchPad.SetActive(true);
            
            PlayerPrefs.SetInt("AdsViewed", 1);

            Destroy(_adsButton);
        }
        bip.Play();
    }

    public void ShopButton()
    {
        shop.SetActive(true);
        
        shopAnim.Play("ShopCreate");
        
        _menuAnim.Play("MenuIdle");
        
        menu.SetActive(false);
        
        Score._secondMoneyText.gameObject.SetActive(true);
        
        click.Play();
    }

    public void CloseShop()
    {
        shopAnim.Play("ShopIdle");
        
        shop.SetActive(false);
        
        menu.SetActive(true);
        
        _menuAnim.Play("MenuCreate");
        
        Score._secondMoneyText.gameObject.SetActive(false);
        
        click.Play();
    }

    public void PlusMoney()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("Rewarded_Android");
            Score.ChangeMoneyCount(5);
            click.Play();
        }
        else
        {
            bip.Play();
        }
        
    }

    private IEnumerator CountDown()
    {
        PlayerSprite.clickText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            PlayerSprite.clickText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        PlayerSprite.Click();
    }

    public void Pause()
    {
        switch (Time.timeScale)
        {
            case 1f:
                Time.timeScale = 0f;
                _pauseNo.SetActive(false);
                _pauseYes.SetActive(true);
                break;
            case 0f:
                Time.timeScale = 1f;
                _pauseNo.SetActive(true);
                _pauseYes.SetActive(false);
                break;
        }
    }
}

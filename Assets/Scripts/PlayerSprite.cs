using UnityEngine;
using UnityEngine.UI;

public class PlayerSprite : MonoBehaviour
{
    public Sprite[] _figures; //массив спрайтов персонажа

    public SpriteRenderer playerSprite; //спрайт игрока

    [SerializeField] private ObstacleCreate ObstacleCreate;

    public static int spriteNow; //указывает какой сейчас спрайт
    
    public Animator _cameraAnim;

    public Text clickText;

    [SerializeField] private AudioSource kick;

    [SerializeField] private ParticleSystem _changeEffect;

    public GameObject _pauseButton;
    

    void Start()
    {
        spriteNow = 0; 
        Time.timeScale = 0f; //делает паузу 
        _changeEffect.startColor = playerSprite.color;
    }

    public void Click() //при нажатии на экран
    {
        _pauseButton.SetActive(true);        
        
        _cameraAnim.Play("CameraIdle"); //сбросить предыдущую анимацию

        _changeEffect.Stop();
        
        _changeEffect.Play();
        kick.pitch = Random.Range(0.9f, 1.1f);
        kick.Play();
        Time.timeScale = 1f;        // Начало игры 
        clickText.gameObject.SetActive(false); // Убрать текст с надписью ClICK
        
        _cameraAnim.Play("CameraZoom"); //эффект камеры при нажатии
        
        
        if (spriteNow != _figures.Length - 1)       //
        {                                           // 
            spriteNow++;                            //
        }                                           //
        else                                        //
        {                                           //
            spriteNow = 0;                          //
        }                                           //
        playerSprite.sprite = _figures[spriteNow];  // Изменение спрайта игрока

        
        if (ObstacleCreate.complexity == 1 || ObstacleCreate.complexity == 2) //если нормал или хард
        {
            if (ObstacleCreate.rotatePlayer) //если перпятствие горизонтальное
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else //если перпятствие вертикальное
            {
                transform.rotation = Quaternion.Euler(0,0,0);
            }
        }


        if (ObstacleCreate.complexity == 3 || ObstacleCreate.complexity == 4) //при рандомном повороте поворачивать и аналогично поворачивать игровка
        {
            transform.rotation = Quaternion.Euler(0,0,ObstacleCreate.rotation);
        }
    }

    public void RotatePlayer(int zDegree) //для поворота игрока если препятствие сменило поворот
    {
        transform.rotation = Quaternion.Euler(0,0,zDegree);
    }
}

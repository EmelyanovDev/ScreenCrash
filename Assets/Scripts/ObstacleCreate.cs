
using System.Collections;
using UnityEngine;

public class ObstacleCreate : MonoBehaviour
{
    public GameObject[] _obstacles; //массив препятствий

    [SerializeField] private float _timeToCreate; //скорость появления
    
    [SerializeField] private PlayerSprite PlayerSprite; //для изменения спрайта игрока

    [SerializeField] private SkinChange _skinChange;

    public float _delayTime; //задержка перед атакой препятствия для другого скрипта

    public int complexity; // 0 - нормал 1 - хард 2 - инсэйн 3 - дай
    
    public int queue = 0; //чтобы поворачивать препятствие через раз

    public int rotation; //поворот при инсэйне для персонажа

    public bool rotatePlayer; //поворачивать ли персонажа когда препятствие горизонталньое
    
    public static bool gameIsEnd; //указывает на конец игры
    
    private int choosenObstacle; //выбирает препятствие из массива
    
    private GameObject obstacle; //для изменения при разной сложности

    private int numberOfObstacle; //какое препятствие по счёту  

    public bool giveMoney;

    [SerializeField] private AudioSource complexingSound;

    [SerializeField] private AudioSource create;

    [SerializeField] private ParticleSystem _createEffect;

    [SerializeField] private GameObject _echoEffect;

    [SerializeField] private ParticleSystem[] _echoEffects;
    void Start()
    {
        StartCoroutine(CreateObstacle());
        gameIsEnd = false;
    }
    

    public IEnumerator CreateObstacle()
    {
        yield return new WaitForSeconds(_timeToCreate);
        if (!gameIsEnd)
        {
            
            
            create.Play();
            
            PlayerSprite._cameraAnim.Play("CameraIdle");
            
            PlayerSprite._cameraAnim.Play("CameraStraggering");
            
            choosenObstacle = Random.Range(0, _obstacles.Length);
            
            obstacle = (GameObject)Instantiate(_obstacles[choosenObstacle], new Vector3(0, 0, 0), Quaternion.identity);

            if (PlayerPrefs.HasKey("ObstacleColor"))
            {
                for (int i = 0; i < obstacle.transform.childCount; i++)
                {
                    obstacle.transform.GetChild(i).GetComponent<SpriteRenderer>().color = _skinChange._skinColors[PlayerPrefs.GetInt("ObstacleColor")];
                }
            }
            
            _createEffect.Play();

            numberOfObstacle++;

            if (numberOfObstacle % 5 == 0)
            {
                obstacle.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                
                giveMoney = true;
            }
            else
            {
                giveMoney = false;
            }

            rotatePlayer = false; //для отсутствия поворота персонажа при нажатии на экран
            
            switch (complexity) //при разной сложности 
            {
                    
                case 1: //меняет поворот каждый раз на 90 градусов
                    if (queue == 1) //если горизонтальное
                    {
                        RotateObstacle();
                    }
                    else if(queue == 0) //если вертикальное
                    {
                        queue++; //чтобы в следующий раз было горизотнальное

                        PlayerSprite.RotatePlayer(0); //поворачивает игрока сразу при появлении
                    }
                    break;
                case 2:
                    queue = Random.Range(0, 2); //решает, будет препятствие горизантальное или вертикальное
                    
                    if (queue == 1) //будет
                    {
                        RotateObstacle();
                    }
                    else //не будет
                    {
                        PlayerSprite.RotatePlayer(0);
                    }
                    break;
                case 3:
                    rotation = Random.Range(1, 360); //рандомно выбирает поворот препятствия
                    obstacle.transform.rotation = Quaternion.Euler(0,0,rotation); //поворачивает препятсвтие
                    
                    PlayerSprite.RotatePlayer(rotation); //поворачивает персонажа при появлении
                    break;
                case 4:
                    rotation = Random.Range(1, 360); //рандомно выбирает поворот препятствия
                    obstacle.transform.rotation = Quaternion.Euler(0,0,rotation); //поворачивает препятсвтие
                    
                    PlayerSprite.spriteNow = Random.Range(0, PlayerSprite._figures.Length);             //
                    PlayerSprite.playerSprite.sprite = PlayerSprite._figures[PlayerSprite.spriteNow]; //при появлении препятствия делает рандомную фигуру
                    
                    PlayerSprite.RotatePlayer(rotation); //повернуть игрока аналогично препятствию
                    break;

            }
            
            
            
            _timeToCreate -= 0.015f; //уменшить скорость появления
            _delayTime -= 0.015f;    //уменшить задержку перед сжатием

            StartCoroutine(CreateObstacle()); //начать корутину заново
        }
    }

    private void RotateObstacle()
    {
        rotatePlayer = true;
        
        PlayerSprite.RotatePlayer(90);
        
        obstacle.transform.rotation = Quaternion.Euler(0, 0, 90);
        
        queue = 0;
    }

    public void ObstacleComplexion()
    {
        complexingSound.Play();
    }

    public void EchoEffectPlay()
    {
        _echoEffect.transform.rotation = obstacle.transform.rotation;
        foreach (var echoEffect in _echoEffects)
        {
            echoEffect.Play();
        }
    }
}

using System.Collections;
using UnityEngine;

public class ObstacleAttack : MonoBehaviour
{
    private Animator anim;

    private ObstacleCreate ObstacleCreate;
    
    private Score Score;

    [SerializeField] private int _obstacleNumber; //для указания, какой формы данное препятствие 
    

    void Start()
    {
        anim = GetComponent<Animator>();
        
        ObstacleCreate = Camera.main.GetComponent<ObstacleCreate>();

        Score = Camera.main.GetComponent<Score>();

        StartCoroutine(StartAnimation());
        
    }
    
    private IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(ObstacleCreate._delayTime);
        
        anim.Play("SquareObstacle");
        
        ObstacleCreate.ObstacleComplexion();
    }
    public void EndAnimation() 
    {
        ObstacleCreate.EchoEffectPlay();

        if (_obstacleNumber == PlayerSprite.spriteNow)
        {
            Score.ObstacleAttack(false);
        }
        else
        {
            Score.ObstacleAttack(true);
            
            ObstacleCreate.gameIsEnd = true;
        }
        
        Destroy(this.gameObject);
        
    }
}

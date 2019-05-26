using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : Unit
{
    //
    public bool win_game = false;
    //Текст
    public float points = 0;

    public Text ScoreText;
    public Text MAIN_TEXT;
    /// <summary>
    /// Методы доступные для просмотра при запуске игры
    //[SerializeField]///Вот это поле отвечает за то, что на них можно посмотреть
    private int lives = 5;

    private LivesBar livesBar;

    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            if (value < 5) lives = value;
            livesBar.Refresh();
        }
    }


    private float speed = 5.0f;
    
    private float jumpForce = 12.0f;
    /// </summary>

    private bool isGrounded = false;

    /// Огненный шар
    private Bullet bullet;

    public float nextShootTime = 0;
    public float nowTime = 0;
    public float oldTime = 0;

    ///Направление импульса
    public Vector3 direction;

    ///устанавливаем или получаем значение state(того какая сейчас анимация
    private Charstate State
    {
        get
        {
            return (Charstate)animator.GetInteger("State");
        }
        set
        {
            animator.SetInteger("State", (int)value);
        }
    }

    /// Отвечает за телесность нашего объекта
    new private Rigidbody2D rigidbody;
    ///Анимация нашего объека
    private Animator animator;
    ///Картинка и положение нашего объекта(а ещё размер)
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    ///Значения которые устанавливаются при из игры
    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("FaerBall");
    }

    ///Специальная функция которая работает каждый фиксированный интервал времени
    private void FixedUpdate()
    {
        CheckGround();///Каждый момент проверяем на землел ли мы
    }

    /// Все что тут, постоянно обновляется
    private void Update()
    {
        ScoreText.text = "Score : " + points;
        //Debug.Log(points);
        nowTime += Time.deltaTime;
        if (isGrounded)
        {
            State = Charstate.Idle;
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (nextShootTime <= nowTime)
            {
                Shoot();
                nextShootTime += nowTime + 5;
            }
        }
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }


        if (win_game)
        {
            MAIN_TEXT.text = "YOU WIN!!!";
            if (nowTime >= oldTime + 3)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else if(Lives <= 0 || transform.position.y < -30)
        {
            MAIN_TEXT.text = "Fail:(";
            if (nowTime >= oldTime + 3)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            oldTime = nowTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

    }

    private void Run()
    {
        direction = Vector3.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        ///Если GetAxis вернул -1 то вправо если 1 то влево
        spriteRenderer.flipX = direction.x < 0.0f;

        ///Если на земле и бежим то включается анимация бега
        if (isGrounded)
        {
            State = Charstate.Run;
        }
    }

    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }


    private void Shoot()
    {
        Vector3 position = transform.position;//ДОБАВИТЬ ПОЗИЦИЮ Y 

        ///Создаем пулю в сцене
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (spriteRenderer.flipX ? -1.0f : 1.0f);
        newBullet.spriteRenderer.flipX = (spriteRenderer.flipX == false);
    }

    public override void ReceiveDamage()
    {
        Lives --;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 4.0f, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);///Массив всеъ колайдеров в радиусе 1

        isGrounded = colliders.Length > 1; ///Если калайдеров > 1 то прыгать мы не можем

        ///Если прыгаем, то анимация прыжка
        if (!isGrounded)
        {
            State = Charstate.Jump;
        }
    }

}

/// Перечисление анимаций
public enum Charstate
{
    Idle,/// Стоять и моргать
    Run, /// Беги лес, беги
    Jump ///Прыгаем
}
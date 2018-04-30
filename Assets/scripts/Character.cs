﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Players Stats
    [Header("Stats")]
    public int FireColb;//количество огня в колбе
    public int lives;//количество жизней
    public float speed;
    private float TempSpeed;//временная скорость (изменяется)

    int MinFireColb = 0;//минимальное хп, при котором прекращаются выстрелы
    [Tooltip("Время перезарядки конвертации жизни")]
    public float TimeToPlusLives;
    [Tooltip("Сколько секунд после смерти нужно ждать чтобы воскреснуть")]
    float zaderzhka = 1;
    #endregion

    #region For checked time
    [HideInInspector]
    public float LastTimeToPlusLives;//последнее время конвертьации жизней
    float timeDie;//время смерти
    Vector3 napravlenie;//куда смотрит игрок
    float deltaColor;//для плавного изменения цвета игрока
    public int AttackType = 0;//1-near,2-shoot
    float OfssetY;//расстояниие появления атаки по у относительно перса
    #endregion

    #region System
    Rigidbody2D rb;
    Animator animator;

    public enum AnimState
    {
        stay,
        run,
        jump
    }
    public AnimState State//передаем состояние анимации в аниматор
    {
        get { return (AnimState)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }
    #endregion

    #region Eny GameObjects
    [Header("Eny GameObjects")]
    public Fire FirePrefab;
    public Fire AttackWavePrefab;
    public Deleter DeleterSmoke;
    Fire prefab; //префаб текущей атаки
    #endregion

    #region Flags
    [Header("Flags")]
    [HideInInspector]
    public bool isGrounded = false; //проверка, стоит ли на земле
    bool lastIsGrounded = true;//для проверки приземления(звук)
    bool landing = false;//для порверки приземдения (звук)
    [Tooltip ("мы находимся в состоянии прыжка?")]
    bool CheckJump;
    #endregion

    #region Audio
    [Header("Audio")]
    public AudioSource AuSourse;
    public AudioClip[] RunSound = new AudioClip[7];//звуки шагов
    int i;
    public AudioClip JumpSound;
    #endregion


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CheckJump = false;
        lives = 100;
        speed = 3.5F;
        LastTimeToPlusLives = 0;
        FireColb = 0;
        napravlenie = Vector3.right;
        //счетчики для звуков шагов
        i = 0;
        AuSourse.volume = 0.03F;
        LastTimeToPlusLives = -5;
    }

    private void FixedUpdate()
    {
        CheckGround();
        if (lives <= 0) { Die(); }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
    }

    private void Update()
    {
        #region input left - right

        if (Input.GetAxis("Horizontal") != 0)//обычное хождение
        {
            napravlenie = transform.right * Input.GetAxis("Horizontal"); //(возвращает 1\-1) Unity-> edit-> project settings -> Input 
            GetComponent<SpriteRenderer>().flipX = napravlenie.x > 0.0F;
            State = AnimState.run;
        }
        else if (isGrounded)
        {
            State = AnimState.stay;
        }
        if (!isGrounded)
        {
            State = AnimState.jump;
        }

        #endregion

        #region input Jump
        if (isGrounded && Input.GetButton("Jump") && (CheckJump == false))//прыжок 
        {
            rb.AddForce(new Vector3(10F * Input.GetAxis("Horizontal"), 72), ForceMode2D.Impulse);
            CheckJump = true;
        }
        if (((Input.GetButton("Jump")) == false) && isGrounded)//если отпустили клавишу прыжка
        {
            CheckJump = false;
        }
        //звук приземления
        if (lastIsGrounded != isGrounded)
        {
            if (landing) AuSourse.PlayOneShot(JumpSound);
            landing = !landing;
        }
        lastIsGrounded = isGrounded;
        #endregion

        #region  Attack
        if (Input.GetButtonDown("Fire2"))//shooting
        {
            AttackType = 2;
            animator.SetBool("attack", true);
        }
        else if (Input.GetButtonDown("Fire1"))//near attack
        {
            AttackType = 1;
            animator.SetBool("attack", true);
        }
        else
        {
            animator.SetBool("attack", false);
        }
        #endregion

        #region input ConvertLives
        if (Input.GetButtonDown("Lives")&&(lives<100)) ConvertToLives();//поменять огонь на жизни
        #endregion

        //изменение цвета лисицы от хп
        deltaColor = Mathf.Lerp(deltaColor, (lives / 100.0F), Time.deltaTime * 2);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(deltaColor, deltaColor, deltaColor);   
    }

    private void CheckGround()//проверка стоит ли персонаж на земле
    {
        //круг вокруг нижней линии персонажа. если в него попадают колайдеры то массив заполняется ими
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1F);
        isGrounded = colliders.Length > 1; //один колайдер всегда внутри (кол. персонажа)
    }

    private void Attack()//вызывается из аниматора
    {
        if (AttackType == 1) { prefab = AttackWavePrefab; OfssetY = 0.6F; }
        else if(AttackType==2){ prefab = FirePrefab; OfssetY = 0.7F; }
        Vector3 position = new Vector3(transform.position.x + (GetComponent<SpriteRenderer>().flipX ? 0.5F : -0.5F), transform.position.y+OfssetY);//место создания пули относительно персонажа
        Fire fire = PoolManager.GetObject(prefab.name,position, prefab.transform.rotation).GetComponent<Fire>();
        fire.napravlenie = fire.transform.right * (GetComponent<SpriteRenderer>().flipX ? 0.5F : -0.5F);//задаем направление и скорость пули (?если  true : false)
        fire.CurrentSpeed+=Mathf.Abs(rb.velocity.x);
        fire.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
        FireColb -= fire.minusFire;
    }

    private void OnTriggerEnter2D(Collider2D collision)//собирание огоньков
    {
        if (collision.GetComponent<FireSphere>())
        {
            collision.gameObject.GetComponent<PoolObject>().ReturnToPool();
            FireColb++;
        }
    }

    private void ConvertToLives()//конвертирование огня в жизнь
    {
        if ((FireColb >= 40)&&(Time.time>LastTimeToPlusLives+TimeToPlusLives))//в колбе достаточно огня и прошло время перезарядки
        {
            if (lives <= 80)
            {
                FireColb -= 40;
                lives = lives + 20;//добавляем жизней
            }
            else//если хп больше 80
            {
                FireColb = FireColb - (100 - lives) * 2;
                lives = 100;
            }
            LastTimeToPlusLives = Time.time;
        }
    }

    void Die()//смерть персонажа
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        lives = 0;
        timeDie = Time.time;
        if (timeDie + zaderzhka > Time.time)
        {
            transform.position = DeleterSmoke.RespPos;
            lives = 100;
            FireColb = 0;
        }
    }

    #region Audio

    private void PlayRunSound()//вызывается из аниматора
    {
        i = Random.Range(0,6);
        AuSourse.PlayOneShot(RunSound[i]);
    }

    #endregion
}

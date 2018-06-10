﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField]
    int live;
    [SerializeField]
    int Damage;//количество наносимого урона
    [SerializeField]
    bool Udar;//одиночный удар уже был нанесен?
    [SerializeField]
    bool dvig;

    private void OnEnable()
    {
        if (dvig)
        GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-0.9F, 0.9F), Random.Range(-1.5F, 1.5F)));
    }

    private void Update()
    {
        if (live < 1)
        {
            GetComponent<PoolObject>().ReturnToPool();//"удаление объекта"
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Character unit = collision.collider.GetComponent<Character>();
        if (unit)//Обнуляем флаг одиночного удара
        {
            Udar = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character unit =collision.collider.GetComponent<Character>();
        if (unit)//Одиночный удар
        {
            unit.lives = unit.lives - Damage;
            Udar = true;
        }
        else
        {
            Asteroid notBHole = collision.collider.GetComponent<Asteroid>();
            notBHole.live -= Damage;
        }
    }

}

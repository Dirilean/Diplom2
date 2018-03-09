﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPlatform : MonoBehaviour
{
    public GameObject[] f = new GameObject[41]; //все платформы на "земле"
    public GameObject gr;//земля
    Vector3 GenPos;//текущая позиция генерации
    Vector3 PlatGenPos;//следующая выбранная позиция генерации
    Quaternion GenQ = new Quaternion(0, 0, 0, 0);//текущий разворот
    [SerializeField]
    ForGen forgen;
    float LastPos;//Координата последней платформы
    byte method;//номер метода платформы, совпадает с номером метода

    System.Random rnd = new System.Random();
    int RndStep;//рандомный шаг для создания следущей платформы, ОБЯЗАТЕЛЬНО КРАТНО 2
    int RndPak;//из какого пака платформ выбирать
    int RndVid;//какую платформу из пака выбрать

    void Start()
    {
        GenPos.x = forgen.transform.position.x;
        LastPos = 20;
        RndStep = 2;
       // Debug.Log("GenPos=" + (int)GenPos.x + ", LastPos=" + LastPos+" rndstep="+RndStep);
    }


    void Update()//генерация !главный метод, вызывающий остальные!
    {
        GenPos.x = forgen.transform.position.x;
        if (forgen.busy != true) { GenGround(); }//если в позиции генерации пусто, создай землю
        if (((int)GenPos.x % 2 == 0)&&(((int)LastPos + RndStep == (int)GenPos.x)))//каждую четную позицию генерации вызывай это
        {
           // Debug.Log("GenPos=" + (int)GenPos.x + ", LastPos=" + (int)LastPos + " rndstep=" + RndStep + " метод=" + method);
            switch (method)
            {
                case 0: F0(); break;
                case 3:  F3(); break;
                case 6: F6(); break;
                case 9:  F9(); break;
                case 12:  F12(); break;
                case 15: F15(); break;
                case 18:  F18(); break;
                case 21:  F21(); break;
                case 24:  F24(); break;
                case 27:  F27(); break;
                case 30:  F30(); break;
                case 33: F33(); break;
                case 36:  F36(); break;
                case 39:  F39(); break;
            }
        }
    }

    void GenGround()//генерирование земли
    {
        Instantiate(gr, new Vector3(Mathf.Round(GenPos.x), GenPos.y - 3.0F), GenQ);
    }

    public int GetRandom(params int[] values)//для выбора рандомного числа из предложеных (используется в методах создания платформ)
    {
        if (values != null)
        { return values[rnd.Next(values.Length)]; }
        else { return 4; }
    }


    void GenPlat(GameObject a1, GameObject a2, GameObject a3)//универсальный метод рандомного создания платформ из подобных
    {
        RndVid = rnd.Next(2);
        switch (RndVid)//выбираем внешний вид текущей платформы платформы
        {
            case 0: Instantiate(a1, new Vector3(GenPos.x, a1.transform.position.y), GenQ); break;
            case 1: Instantiate(a2, new Vector3(GenPos.x, a2.transform.position.y), GenQ); break;
            case 2: Instantiate(a3, new Vector3(GenPos.x, a3.transform.position.y), GenQ); break;
        }
        LastPos = GenPos.x;
    }

    //__________описание платформ_________

    void F0()
    {
        GenPlat(f[0], f[1], f[2]);//Генерирование рандомной платформы из пака
        RndPak = rnd.Next(12);//число для выбора следующей платформы
        switch (RndPak)//какая платформа следующая?
        {
            case 0: RndStep = GetRandom(2, 4, 8, 12); method = 0; break;//выбираем метод по номеру первой платформы в паке
            case 1: RndStep = GetRandom(2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom(2, 4, 8, 12); method = 6; break;
            case 3: RndStep = 2; method = 9; break;
            case 4: RndStep = 2; method = 12; break;
            case 5: RndStep = 2; method = 15; break;
            case 6: RndStep = GetRandom(2, 4, 8, 12); method = 24; break;
            case 7: RndStep = GetRandom(4, 8, 12); method = 27; break;
            case 8: RndStep = GetRandom(4, 8, 12); method = 30; break;
            case 9: RndStep = GetRandom(4, 8, 12); method = 33; break;
            case 10: RndStep = 2; method = 36; break;
            case 11: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 4;//увеличиваем шаг на длинну платформы
    }

    void F3()
    {
        GenPlat(f[3], f[4], f[5]);
        RndPak = rnd.Next(12);
        switch (RndPak)
        {
            case 0: RndStep = GetRandom(2, 4, 8, 12); method = 0; break;
            case 1: RndStep = GetRandom(2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom(2, 4, 8, 12); method = 6; break;
            case 3: RndStep = 2; method = 9; break;
            case 4: RndStep = 2; method = 12; break;
            case 5: RndStep = GetRandom( 2); method = 15; break;
            case 6: RndStep = GetRandom(2, 4, 8, 12); method = 24; break;
            case 7: RndStep = GetRandom(4, 8, 12); method = 27; break;
            case 8: RndStep = GetRandom(4, 8, 12); method = 30; break;
            case 9: RndStep = GetRandom(4, 8, 12); method = 33; break;
            case 10: RndStep = 2; method = 36; break;
            case 11: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 8;
    }

    void F6()
    {
        GenPlat(f[6], f[7], f[8]);
        RndPak = rnd.Next(14);
        switch (RndPak)
        {
            case 0: RndStep = GetRandom( 2, 4, 8, 12); method = 0; break;
            case 1: RndStep = GetRandom( 2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom( 2, 4, 8, 12); method = 6; break;
            case 3: RndStep = 2; method = 9; break;
            case 4: RndStep = 2; method = 12; break;
            case 5: RndStep = 2; method = 15; break;
            case 6: RndStep = 2; method = 18; break;
            case 7: RndStep = 2; method = 21; break;
            case 8: RndStep = GetRandom( 2, 4, 8, 12); method = 24; break;
            case 9: RndStep = GetRandom(2, 4, 8, 12); method = 27; break;
            case 10: RndStep = GetRandom(2, 4, 8, 12); method = 30; break;
            case 11: RndStep = GetRandom(2, 4, 8, 12); method = 33; break;
            case 12: RndStep = 2; method = 36; break;
            case 13: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 8;
    }



    void F9()
    {
        GenPlat(f[9], f[10], f[11]);
        RndPak = rnd.Next(12);
        switch (RndPak)
        {
            case 0: RndStep = GetRandom( 2, 4, 8, 12); method = 0; break;
            case 1: RndStep = GetRandom( 2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom( 2, 4, 8, 12); method = 6; break;
            case 3: RndStep = 2; method = 9; break;
            case 4: RndStep = 2; method = 12; break;
            case 5: RndStep = 2; method = 15; break;
            case 6: RndStep = GetRandom(2, 4, 8, 12); method = 24; break;
            case 7: RndStep = GetRandom(2, 4, 8, 12); method = 27; break;
            case 8: RndStep = GetRandom(2, 4, 8, 12); method = 30; break;
            case 9: RndStep = GetRandom(2, 4, 8, 12); method = 33; break;
            case 10: RndStep = 2; method = 36; break;
            case 11: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 4;
    }



    void F12()
    {
        GenPlat(f[12], f[13], f[14]);
        RndPak = rnd.Next(10);
        switch (RndPak)
        {
            case 0: RndStep = GetRandom( 2, 4, 8, 12); method = 0; break;
            case 1: RndStep = GetRandom( 2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom( 2, 4, 8, 12); method = 6; break;
            case 3: RndStep = 2; method = 15; break;
            case 4: RndStep = GetRandom( 2, 4, 8, 12); method = 24; break;
            case 5: RndStep = GetRandom( 2, 4, 8, 12); method = 27; break;
            case 6: RndStep = GetRandom( 2, 4, 8, 12); method = 30; break;
            case 7: RndStep = GetRandom( 2, 4, 8, 12); method = 33; break;
            case 8: RndStep = 2; method = 36; break;
            case 9: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 8;
    }


    void F15()
    {
        GenPlat(f[15], f[16], f[17]);
        RndPak = rnd.Next(12);
        switch (RndPak)
        {
            case 0: RndStep = GetRandom( 2, 4, 8, 12); method = 0; break;
            case 1: RndStep = GetRandom( 2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom( 2, 4, 8, 12); method = 6; break;
            case 3: RndStep = 2; method = 15; break;
            case 4: RndStep = 2; method = 18; break;
            case 5: RndStep = 2; method = 21; break;
            case 6: RndStep = GetRandom( 2, 4, 8, 12); method = 24; break;
            case 7: RndStep = GetRandom( 2, 4, 8, 12); method = 27; break;
            case 8: RndStep = GetRandom( 2, 4, 8, 12); method = 30; break;
            case 9: RndStep = GetRandom( 2, 4, 8, 12); method = 33; break;
            case 10: RndStep = 2; method = 36; break;
            case 11: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 8;
    }


    void F18()
    {
        GenPlat(f[18], f[19], f[20]);
        RndPak = rnd.Next(12);
        switch (RndPak)
        {
            case 0: RndStep = GetRandom(2, 4, 8, 12); method = 0; break;
            case 1: RndStep = GetRandom(2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom(2, 4, 8, 12); method = 6; break;
            case 3: RndStep = 2; method = 15; break;
            case 4: RndStep = 2; method = 18; break;
            case 5: RndStep = 2; method = 21; break;
            case 6: RndStep = GetRandom(2, 4, 8, 12); method = 24; break;
            case 7: RndStep = GetRandom(2, 4, 8, 12); method = 27; break;
            case 8: RndStep = GetRandom(2, 4, 8, 12); method = 30; break;
            case 9: RndStep = GetRandom(2, 4, 8, 12); method = 33; break;
            case 10: RndStep = 2; method = 36; break;
            case 11: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 4;
    }

    void F21()
    {
        GenPlat(f[21], f[22], f[23]);
        RndPak = rnd.Next(12);
        switch (RndPak)
        {
            case 0: RndStep = GetRandom(2, 4, 8, 12); method = 0; break;
            case 1: RndStep = GetRandom(2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom(2, 4, 8, 12); method = 6; break;
            case 3: RndStep = 2; method = 15; break;
            case 4: RndStep = 2; method = 18; break;
            case 5: RndStep = 2; method = 21; break;
            case 6: RndStep = GetRandom(2, 4, 8, 12); method = 24; break;
            case 7: RndStep = GetRandom(2, 4, 8, 12); method = 27; break;
            case 8: RndStep = GetRandom(2, 4, 8, 12); method = 30; break;
            case 9: RndStep = GetRandom(2, 4, 8, 12); method = 33; break;
            case 10: RndStep = 2; method = 36; break;
            case 11: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 8;
    }

    void F24()
    {
        GenPlat(f[24], f[25], f[26]);
        RndPak = rnd.Next(14);
        switch (RndPak)
        {
            case 0: RndStep = GetRandom( 2, 4, 8, 12); method = 0; break;
            case 1: RndStep = GetRandom( 2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom( 2, 4, 8, 12); method = 6; break;
            case 3: RndStep = 2; method = 9; break;
            case 4: RndStep = 2; method = 12; break;
            case 5: RndStep = 2; method = 15; break;
            case 6: RndStep = 2; method = 18; break;
            case 7: RndStep = 2; method = 21; break;
            case 8: RndStep = GetRandom( 2, 4, 8, 12); method = 24; break;
            case 9: RndStep = GetRandom( 2, 4, 8, 12); method = 27; break;
            case 10: RndStep = GetRandom( 2, 4, 8, 12); method = 30; break;
            case 11: RndStep = GetRandom( 2, 4, 8, 12); method = 33; break;
            case 12: RndStep = 2; method = 36; break;
            case 13: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 12;
    }

    void F27()
    {
        GenPlat(f[27], f[28], f[29]);
        RndPak = rnd.Next(12);
        switch (RndPak)
        {
            case 0: RndStep = GetRandom(2, 4, 8, 12); method = 0; break;
            case 1: RndStep = GetRandom(2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom(2, 4, 8, 12); method = 6; break;
            case 3: RndStep = 2; method = 9; break;
            case 4: RndStep = 2; method = 12; break;
            case 5: RndStep = 2; method = 15; break;
            case 6: RndStep = GetRandom(4, 8, 12); method = 24; break;
            case 7: RndStep = GetRandom(4, 8, 12); method = 27; break;
            case 8: RndStep = GetRandom(4, 8, 12); method = 30; break;
            case 9: RndStep = GetRandom(4, 8, 12); method = 33; break;
            case 10: RndStep = 2; method = 36; break;
            case 11: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 4;
    }

    void F30()
    {
        GenPlat(f[30], f[31], f[32]);
        RndPak = rnd.Next(12);
        switch (RndPak)
        {
            case 0: RndStep = GetRandom(2, 4, 8, 12); method = 0; break;
            case 1: RndStep = GetRandom(2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom(2, 4, 8, 12); method = 6; break;
            case 3: RndStep = 2; method = 9; break;
            case 4: RndStep = 2; method = 12; break;
            case 5: RndStep = 2; method = 15; break;
            case 6: RndStep = GetRandom(4, 8, 12); method = 24; break;
            case 7: RndStep = GetRandom(4, 8, 12); method = 27; break;
            case 8: RndStep = GetRandom(4, 8, 12); method = 30; break;
            case 9: RndStep = GetRandom(4, 8, 12); method = 33; break;
            case 10: RndStep = 2; method = 36; break;
            case 11: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 8;
    }

    void F33()
    {
        GenPlat(f[33], f[34], f[35]);
        RndPak = rnd.Next(12);
        switch (RndPak)
        {
            case 0: RndStep = GetRandom(2, 4, 8, 12); method = 0; break;
            case 1: RndStep = GetRandom(2, 4, 8, 12); method = 3; break;
            case 2: RndStep = GetRandom(2, 4, 8, 12); method = 6; break;
            case 3: RndStep =2; method = 15; break;
            case 4: RndStep = 2; method = 18; break;
            case 5: RndStep = 2; method = 21; break;
            case 6: RndStep = GetRandom(2, 4, 8, 12); method = 24; break;
            case 7: RndStep = GetRandom(4, 8, 12); method = 27; break;
            case 8: RndStep = GetRandom(4, 8, 12); method = 30; break;
            case 9: RndStep = GetRandom(4, 8, 12); method = 33; break;
            case 10: RndStep = 2; method = 36; break;
            case 11: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 8;
    }

    void F36()
    {
        GenPlat(f[36], f[37], f[38]);
        RndPak = rnd.Next(12);
        switch (RndPak)
        {
            case 0: RndStep = 2; method = 0; break;
            case 1: RndStep = 2; method = 3; break;
            case 2: RndStep = 2; method = 6; break;
            case 3: RndStep = 2; method = 15; break;
            case 4: RndStep = 2; method = 18; break;
            case 5: RndStep = 2; method = 21; break;
            case 6: RndStep = GetRandom(2, 4, 8, 12); method = 24; break;
            case 7: RndStep = GetRandom(4, 8, 12); method = 27; break;
            case 8: RndStep = GetRandom(4, 8, 12); method = 30; break;
            case 9: RndStep = GetRandom(4, 8, 12); method = 33; break;
            case 10: RndStep = 2; method = 36; break;
            case 11: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 4;
    }

    void F39()
    {
        GenPlat(f[39], f[40], f[41]);
        RndPak = rnd.Next(12);
        switch (RndPak)
        {
            case 0: RndStep = 2; method = 0; break;
            case 1: RndStep = 2; method = 3; break;
            case 2: RndStep = 2; method = 6; break;
            case 3: RndStep = 2; method = 15; break;
            case 4: RndStep = 2; method = 18; break;
            case 5: RndStep = 2; method = 21; break;
            case 6: RndStep = GetRandom(2, 4, 8, 12); method = 24; break;
            case 7: RndStep = GetRandom(4, 8, 12); method = 27; break;
            case 8: RndStep = GetRandom(4, 8, 12); method = 30; break;
            case 9: RndStep = GetRandom(4, 8, 12); method = 33; break;
            case 10: RndStep = 2; method = 36; break;
            case 11: RndStep = 2; method = 39; break;
        }
        RndStep = RndStep + 8;
    }
}

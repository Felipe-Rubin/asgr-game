using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;



public class VMath
{
    private float pi;
    private float radangle;

    private static VMath _instance;

    private VMath() {
        pi = Mathf.Acos(-1.0f);
        radangle = 180.0f / Mathf.Acos(-1.0f);
    }

    public static VMath Instance {
        get {
            if (_instance == null)
            {
                _instance = new VMath();
            }
            return _instance;
        }
    }


    /*
        Distancia entre Pontos P(x,y) e Q(x0,y0)
     */


    public float Distance(Vector2 p, Vector2 q)
    {
        float dx = Mathf.Abs(p.x - q.x);
        float dy = Mathf.Abs(p.y - q.y);
        return Mathf.Sqrt(dx * dx + dy * dy);

    }

    /*
        Modulo do Vetor |a| 
    */
    public float Magnitude(Vector2 p)
    {
        return Mathf.Sqrt(p.x * p.x + p.y * p.y);
    }

    /*			
        Vetor Unitario
    */
    public Vector2 Unitvector(Vector2 p)
    {
        float mag = Magnitude(p);
        if (mag == 0.0f)
        {
            Debug.Log("mag " + mag);
            Vector2 unitv = new Vector2(0.0f, 0.0f);
            return unitv;
        }

        Vector2 unit = new Vector2(p.x / mag,p.y / mag );
        return unit;
    }


    /*
        A diferenca entre dois Pontos eh um vetor
        Deslocamento (Displacement)
        P ---> Q
    */
    public Vector2 Displacement(Vector2 p, Vector2 q)
    {
        Vector2 v = new Vector2(q.x - p.x,q.y - p.y);

        return v;
    }

    /*
        Cosseno do Angulo entre 2 Vetores
        Cos O = unitario(p) * unitario(q)
        Cos O = [-1;1]
     */
    public float CosVector(Vector2 p, Vector2 q)
    {

        Vector2 unitp = Unitvector(p);

        Vector2 unitq = Unitvector(q);

        float a = unitp.x * unitq.x + unitp.y * unitq.y;

        if (a > 1.0f && a < 1.1f)
            a = 1.0f;
        if (a < -1.0f && a > -1.1f)
            a = -1.0f;

        if (a > 1 || a < -1)
        {
            Debug.Log("cosvector() Isso n deveria ocorrer");
            Debug.LogWarning("Exit_FAILURE cosvector()");
        }

        return a;
    }

    /*
        Angulo entre 2 dois vetores
    */
    public float Angle(Vector2 p, Vector2 q)
    {
        //Debug.Log("Radangle: " + radangle);
        float res = Mathf.Acos(CosVector(p, q)) * radangle;
        return res;
    }

    ///*
    //    Retorna um float pseudo-random entre [a,b]
    //    Como nao eh chamada srand(), ele ira devolver sempre a mesma sequencia de valores pseudo-random
    //*/
    //float get_random(float a, float b)
    //{

    //    return ((b - a) * ((float)Random() / RAND_MAX)) + a;
    //}




}

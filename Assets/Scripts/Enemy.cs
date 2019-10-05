using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    public int type = -1;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Enemy Started!");

        ac = 12;
        hp = 10;
        gold = UnityEngine.Random.Range(1, 6);
        hp_max = 10;
    }

    public void NewEnemy( )
    {
        ac = UnityEngine.Random.Range(8, 16);
        hp = UnityEngine.Random.Range(5, 15);
        gold = UnityEngine.Random.Range(1, 10) + 1;
        hp_max = hp;
    }

    public string Attack( Player plyr )
    {
        Debug.Log("Enemy Attacks!");
        //type = t;

        float rand = UnityEngine.Random.Range(0.0f, 1.0f);

        if (rand > 0.6f)
        {
            plyr.Hit(UnityEngine.Random.Range(1, 4));
            return "Enemy Hit You!\n";
        }
        else
        {
            return "Enemy Missed!\n";
        }
    }
    
    public string Defend( Player plyr )
    {
        //type = t;

        float rand = UnityEngine.Random.Range(0.0f, 1.0f);

        if (rand > 0.6f)
        {
            Hit(UnityEngine.Random.Range(1, 6) );

            if (hp <= 0)
            {
                plyr.gold += gold;
                gold = 0;
            }

            return "You Hit the Enemy!\n";
        }
        else
        {
            return "You Missed!\n";
        }
    }
}

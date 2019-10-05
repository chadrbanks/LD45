using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int ac;
    public int hp;
    public int hp_max;
    public int gold = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Creature Started!");

        ac = 12;
        hp = 10;
        hp_max = 10;
    }

    public void Hit(int i)
    {
        hp -= i;

        if (hp <= 0)
        {
            Debug.Log("CREATURE DIED!");
        }
    }

    public bool isAlive()
    {
        if (hp > 0)
            return true;
        else
            return false;
    }

    public string RenderText()
    {
        return "AC: " + ac + " \nHP: " + hp + " \\ " + hp_max + "  \nGold: " + gold + "    \nActions: Punch    +5  1d4+1    \n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

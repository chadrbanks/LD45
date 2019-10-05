using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//using Enemy;

public class TestController : MonoBehaviour
{
	public static TestController control;

	public string title = "Coleseums && ";

    public int enemy = 0;

    public Player plyr;
    public Enemy number_one;

    string mtext = "";

    void Awake ()
	{
		if (control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if( control != this )
		{
			Destroy (gameObject);
        }

        float rand = UnityEngine.Random.Range(0.0f, 1.0f);

        if (rand > 0.6f )
        {
            title = title + "Commoners";
        }
        else if (rand > 0.3f)
        {
            title = title + "Conjurers";
            enemy = 1;
        }
        else
        {
            title = title + "Cockatrice";
            enemy = 2;
        }

        number_one.type = enemy;
    }

    void OnGUI( )
	{
        //GUI.Label(new Rect(150, 10, 100, 30), "Health: " + health);
        GUI.Label(new Rect(200, 10, 200, 30), title );

        // x, y, l, w
        GUI.Box(new Rect(200, 200, 250, 400), "Player");
        GUI.Box(new Rect(500, 200, 250, 400), "Actions");
        GUI.Box(new Rect(800, 200, 250, 400), "Enemy");

        GUI.Label( new Rect(210, 250, 200, 300), plyr.RenderText( ) );
        GUI.Label( new Rect(510, 250, 200, 300), mtext );
        GUI.Label( new Rect(810, 250, 200, 300), number_one.RenderText( ) );

        
        if ( GUI.Button( new Rect(10, 100, 100, 30), "Punch" ) )
		{
            if( plyr.isAlive( ) && number_one.isAlive( ) )
            {
                mtext = number_one.Defend(plyr) + mtext;

                if (number_one.isAlive())
                    mtext = number_one.Attack(plyr) + mtext;
            }
            else
            {

                if (plyr.isAlive())
                {
                    mtext = "The match has ended!\n" + mtext;
                    mtext = "Starting new match....\n" + mtext;
                    number_one.NewEnemy();
                }
                else
                {
                    mtext = "The game has ended!\n" + mtext;
                }
            }
        }/*
		if( GUI.Button( new Rect(10, 150, 100, 30), "Heals" ) )
		{
			//TestController.control.health += 10;
		}

		if( GUI.Button( new Rect(10, 200, 100, 30), "Save" ) )
		{
			TestController.control.Save ();
		}
		if( GUI.Button( new Rect(10, 250, 100, 30), "Load" ) )
		{
			TestController.control.Load ();
		}
        */
    }

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create( Application.persistentDataPath + "/ptest.dat" );

		TestPlayerData data = new TestPlayerData ();
		//data.health = health;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load( )
	{
		if (File.Exists( Application.persistentDataPath + "/ptest.dat" ))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open( Application.persistentDataPath + "/ptest.dat", FileMode.Open );
			TestPlayerData data = (TestPlayerData)bf.Deserialize (file);
			file.Close ();

			//health = data.health;
		}
	}
}

[Serializable]
class TestPlayerData
{
	public int health;
}
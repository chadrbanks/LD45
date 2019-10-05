using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{
	public static GameController control;
	public int saveslot = 0;

	private string seed;
	private System.Random pseudoRandom;

	private string message;

	public int gold = 0;
	public int xp = 0;
	public int stuff = 0;

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

		seed = System.DateTime.Now.ToString();
		pseudoRandom = new System.Random(seed.GetHashCode());
	}

	void OnGUI( )
	{
		if( saveslot == 0 )
		{
			if( GUI.Button(new Rect (300, 100, 100, 30), "Save Slot 1") )
			{
				GameController.control.Load (1);
			}
			if( GUI.Button(new Rect (300, 200, 100, 30), "Save Slot 2") )
			{
				GameController.control.Load (2);
			}
			if( GUI.Button(new Rect (300, 300, 100, 30), "Save Slot 3") )
			{
				GameController.control.Load (3);
			}
		}
		else
		{
			GUI.Label (new Rect (150, 10, 400, 30), message );
			GUI.Label (new Rect (150, 40, 100, 30), "Gold: " + gold );
			GUI.Label (new Rect (150, 70, 100, 30), "XP: " + xp );
			GUI.Label (new Rect (150, 100, 100, 30), "Useless stuff: " + stuff );

			if( GUI.Button(new Rect (10, 10, 100, 30), "Adventure") )
			{
				GameController.control.xp++;

				int x = pseudoRandom.Next( 0, 3 );
				if (x == 1) {
					GameController.control.gold++;
					message = "You adventure and find gold!";
				}else if( x == 2 ) {
					GameController.control.stuff++;
					message = "You adventure and find some useless stuff!";
				} else {
					message = "Your adventure is nothing exciting.";
				}
			}
			if( GUI.Button(new Rect (10, 50, 100, 30), "Sell Stuff") )
			{
				if (stuff > 0) {
					GameController.control.gold++;
					GameController.control.stuff--;
					message = "Your sell stuff.";
				} else {
					message = "You have nothing to sell.";
				}
			}
			if( GUI.Button(new Rect (10, 90, 100, 30), "Do Stuff 3") )
			{
				int x = pseudoRandom.Next( 0, 2 );
				message = x.ToString ();
				if (x == 0)
					message = "Nothing happened.";
				else if (x == 1)
					message = "Everything happened.";
				else
					message = "I am sure something happened.";
			}
		}
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create( Application.persistentDataPath + "/p" + saveslot.ToString() + ".dat" );

		PlayerData data = new PlayerData ();

		data.gold = gold;
		data.xp = xp;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load( int id )
	{
		if (File.Exists (Application.persistentDataPath + "/p" + id.ToString () + ".dat"))
		{
			GameController.control.saveslot = id;

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/p" + id.ToString () + ".dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			gold = data.gold;
			xp = data.xp;
		}
		else
		{
			GameController.control.saveslot = id;
		}

	}

	void OnDisable()
	{
		GameController.control.Save();
	}
}

[Serializable]
class PlayerData
{
	public int gold;
	public int xp;
}
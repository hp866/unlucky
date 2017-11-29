﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The class is used as a collection of sectors.
 * 
 * It is attached to the 'Map' GameObject
 */
public class MapClass : MonoBehaviour {
	//Key = Sector, Data = list of sectors adjacent to key.
	private Dictionary<GameObject,List<GameObject>> sector_graph = new Dictionary<GameObject,List<GameObject>>(); 
	/*
	 * Start: called when the game is initiated.
	 * Puts all sectors into the graph sector_graph (stored as a dictionary);
	 */
	void Start() {
		GameClass.init ();
		foreach (Transform child in transform) 
		{
			if (child.name.Substring (0, 8) == "Sector #") 
			{
				Sector sector = child.GetComponent<Sector>();
				List<GameObject> adjacent_sectors = sector.AdjacentSectors;

				this.sector_graph.Add(child.gameObject,adjacent_sectors);
			}
		}	
		this.assignSectorsToPlayers ();
		this.colourSectors ();
	}

	private void assignSectorsToPlayers()
	{
		List<PlayerClass> players = GameClass.Players;

		foreach (GameObject sector in sector_graph.Keys)
		{
			int index = Random.Range (0, players.Count);
			Sector sectorClass = sector.GetComponent<Sector> ();
			sectorClass.Owner = players [index];
			print ("Sector: " + sectorClass.name + " Owner: " + sectorClass.Owner.Name);
		}
	}

	public void deselectAll(){
		foreach (GameObject sector in sector_graph.Keys) 
		{
			Sector sectorClass = sector.GetComponent<Sector> ();
			sector.GetComponent<SpriteRenderer> ().color = sectorClass.Owner.Colour;
			if (sectorClass.Selected == true) 
			{
				sectorClass.Selected = false;
			}
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (1)) 
		{
			this.deselectAll ();
		}
	}

	public void colourSectors()
	{
		foreach (GameObject sector in sector_graph.Keys)
		{
			Sector sectorClass = sector.GetComponent<Sector> ();
			sector.GetComponent<SpriteRenderer> ().color = sectorClass.Owner.Colour;
		}
	}

	/* 
	 * getSelectedSector():
	 * Return:  The sector where it's Selected attribute is true;
	 * 			otherwise: null. 
	 * Searches through all sectors in the game until a Selected one is found.
	 */
	public GameObject getSelectedSector()
	{
		foreach (GameObject sector in sector_graph.Keys) 
		{
			if (sector.GetComponent<Sector>().Selected == true) 
			{
				return sector;
			}
		}
		return null;
	}

	private void printSectorGraph() 
	{
		//Printing sectors (for debug);
		foreach (GameObject key in this.sector_graph.Keys) 
		{
			string output = key.name + ": ";
			foreach (GameObject adjSect in this.sector_graph[key]) 
			{
				output += adjSect.name + " ";
			}
			print (output);
		}
    }

    public void Combat(GameObject Attacker, GameObject Defender)
    {

        Sector AttackSector = Attacker.GetComponent<Sector>();
        Sector DefenderSector = Defender.GetComponent<Sector>();
        AttackSector.Attack = 10;
        DefenderSector.Defence = 5;
        //this is to test it with expected values
        int AttackerA = AttackSector.Attack;
        int DefenderD = DefenderSector.Defence;
        //SceneManager.LoadScene("SliderGame");
        float attack = Random.Range(1f, AttackerA);
        float defence = Random.Range(1f, DefenderD);
        float result = defence - attack;

        print(attack + "=attack " + defence + "=defence " + result + "=result");



    }
}

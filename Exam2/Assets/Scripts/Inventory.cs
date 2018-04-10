using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public static int monkeys;
	public Text score;
	void Start () {
		monkeys = 0;
	}
	
	// Update is called once per frame
	void MonkeyPickup()
	{
		monkeys++;
		score.text = "Monkeys: " + monkeys;
	}
}

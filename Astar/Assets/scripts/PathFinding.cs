using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;

public class PathFinding : MonoBehaviour {

	public GameObject mapGroup; 
	//public GameObject Image;
	public GameObject Square;

	// Use this for initialization
	void Start () {

		//print ("Enter the file name: ");

		//string fileName = Console.ReadLine ();

		int[,] map = getData ("Assets/inputFiles/txt1.txt");
		//print (map [0,1] + " ");
		//Instantiate(Image);
		GameObject tile = new Square();

		Instantiate (tile);



		var graph = new Graph (map);

		var Asearch = new ASearch (graph);
		Asearch.Start (graph.nodes [0], graph.nodes [2]);

		while (!Asearch.finished) {
			Asearch.Step ();

		}
		foreach(var node in Asearch.path){
			print (node.Hcost + " | " + node.Gcost);
		}

		print ("Search done. Path Length: " + Asearch.path.Count + " Iterations: " + Asearch.iterations);

		resetMapGroup (graph);

		foreach (var node in Asearch.path) {
			getImage (node.label).color = Color.red;
		}
	}

	Image getImage(string label){
		var id = Int32.Parse (label);
		var go = mapGroup.transform.GetChild (id).gameObject;
		return go.GetComponent<Image> ();
	}

	void resetMapGroup(Graph graph){
		foreach (var node in graph.nodes) {
			getImage (node.label).color = node.adjacent.Count == 0 ? Color.white : Color.blue;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	private int[,] getData(string fileName){
		//int [,] graph = new int[,];
		//List<int[,]> list = new List<int[,]>();
		List<string> list = new List<string>();

		int cols = 0;

		try{
			string line;

			StreamReader reader = new StreamReader(fileName, Encoding.Default);

			using(reader){

				do{
					line = reader.ReadLine();

					if(line != null){
						string [] entries = line.Split(',');
						if(entries.Length>0){
							foreach(string entry in entries){
								list.Add(entry);
							}
							cols = entries.Length;
						}
					}
				}while(line != null);
			}
			reader.Close();
		}catch(Exception e){
			print("{0}\n" + e.Message);
			//
			return null;
		}
		int rows = list.Count / cols;

		int[,] graph = new int[cols, rows];

		for (int x = 0; x < rows; x++) {
			for (int y = 0; y < cols; y++) {
				graph [x, y] = Int32.Parse (list [cols * x + y]);
			}
		}
		return graph;

	}
}



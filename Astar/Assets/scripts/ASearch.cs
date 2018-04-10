using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ASearch {
	public Graph graph;

	public List<Node> reachable;
	public List<Node> explored;
	public List<Node> path;
	public List<int> weight;

	public Node endNode;
	public Node startNode;
	public int iterations;
	public bool finished;

	public ASearch(Graph graph){
		this.graph = graph;

	}

	public void Start(Node start, Node goal){
		reachable = new List<Node> ();
		reachable.Add (start);

		endNode = goal;
		startNode = start;

		explored = new List<Node> ();
		path = new List<Node> ();
		weight = new List<int> ();
		iterations = 0;

		for (int i = 0; i < graph.nodes.Length; i++) {
			graph.nodes [i].Clear ();

		}
	}

	public void Step(){
		if (path.Count > 0) {
			return;
		}
		if (reachable.Count == 0) {
			finished = true;
			return;
		}

		iterations++;



		var node = chooseNode ();

		if (node == endNode) {
			while (node != null) {
				path.Insert (0, node);
				node = node.previous;
			}
			finished = true;
			return;
		}
		reachable.Remove (node);
		explored.Add (node);

		for (var i = 0; i < node.adjacent.Count; i++) {
			addAdjacent (node, node.adjacent [i]);
		}
	}
	public void addAdjacent(Node node, Node adjacent){
		if (findNode (adjacent, explored) || findNode (adjacent, reachable)) { //look - if available nodes surrounding
			return;
		}
		adjacent.previous = node;
		reachable.Add (adjacent);
	}

	public int getNodeIndex(Node node, List<Node> list){
		for (int i = 0; i < list.Count; i++) {
			if (node == list [i]) {
				return i;
			}

		}
		return -1;
	}

	public bool findNode(Node node, List<Node> list){
		return getNodeIndex (node, list) >= 0;
	}

	public Node chooseNode(){
		for (int i = 0; i < reachable.Count; i++) {
			weight.Add ((getHCost (Int32.Parse(reachable[i].label), i)) + (getGCost (Int32.Parse(reachable[i].label), i)));
		}
		int min = 0;
		int minVal = weight [0];
		for (int z= 0; z < reachable.Count; z++) {
			if (weight [z] < minVal) {
				min = z;
				minVal = weight [z];
			}
		}
		weight.Clear ();
		return reachable [min];
	}
	public int getHCost(int current, int i){

		int curRow = current / graph.cols; //row
		int curCol = current % graph.cols; //col


		int H = (Math.Abs (curRow - ((Int32.Parse(endNode.label)) / graph.cols)) + Math.Abs (curCol - ((Int32.Parse(endNode.label)) % graph.cols)))*10;

		reachable [i].Hcost = H;
		return(H);
	}
	public int getGCost(int current, int i){
		int distance = Math.Abs(current - Int32.Parse (startNode.label));

		int cols = distance % graph.cols;
		int rows = distance / graph.cols;

		int weight = 0;

		while (rows != 0 || cols != 0) {
			if (rows >= 1 && cols >= 1) {
				weight += 14;
				rows--;
				cols--;
			} else if (rows > 0) {
				weight += 10;
				rows--;
			} else {
				weight += 10;
				cols--;
			}
		}
		reachable [i].Gcost = weight;
		//print ("" + weight);
		return weight;

	}

}

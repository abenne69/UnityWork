using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dstar : MonoBehaviour {

	// Use this for initialization

	// Update is called once per frame

	Graph grid;

	int nodesVisited;
	int cutoff;

	Node startNode;
	Node endNode;

	public Transform seeker1, target1;


	void Awake(){
		grid = GetComponent<Graph> ();
	}

	void Update () {
		if (Input.GetButtonDown ("Vertical")) {
			findPath (seeker1.position, target1.position);
		}
	}

	void findPath(Vector3 startPos, Vector3 endPos){
		startNode = grid.NodeFromWorldPoint (startPos);
		endNode = grid.NodeFromWorldPoint (endPos);

		cutoff = heuristic (startNode, endNode);
		Node t;

		List<Node> path = new List<Node> ();
		print("Searching...");
		for (int j = 0; j > -1; j++) {
			
			t = search (startNode, 0, cutoff, path, 0);
		
			if (cutoff == int.MaxValue) { // if the loop has reached a max length;
				return;
			}
			if (t is Node && t != null) {
				grid.path = path; // if t returned a node path has been founds
				print("Path found");
				return;
			}
		}
		return;
	}

	int cost(Node nodeA, Node nodeB){
		return (nodeA.gridX == nodeB.gridX || nodeA.gridY == nodeB.gridY) ? 10 : 14;
	}
	int heuristic(Node nodeA, Node nodeB){
		return euclidian (Mathf.Abs (nodeB.gridX - nodeA.gridX), Mathf.Abs (nodeB.gridY - nodeA.gridY));
	}
	int euclidian(int dx, int dy){
		return (int) Mathf.Round((Mathf.Sqrt (dx * dx - dy * dy)*10));
	}
	Node search(Node node, int Gcost, int cutoff, List<Node> path, int depth){
		print("Searching...");
		nodesVisited++;

		int Fcost = Gcost + heuristic (node, endNode) * 1;

		if (Fcost > cutoff) {
			cutoff = Fcost;
		}

		if (node == endNode) {
			path.Add (node);
			print("Node found");
			return node;
		}

		List<Node> neighbors = new List<Node> ();
		neighbors = grid.GetNeighbors (node);

		Node t;
		int min = int.MaxValue;

		foreach(Node neighbor in neighbors){
			t = search(neighbor, Gcost + cost(node, neighbor), cutoff, path, (depth + 1) );

			if(t is Node){
				path.Add(node);
				return t;
			}
			if(cutoff < min){
				cutoff = min;
			}
		}
		return null;
	}

}

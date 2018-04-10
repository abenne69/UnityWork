using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Astar : MonoBehaviour {

	PathRequestManager requestManager;
	Graph grid;

	void Awake(){
		grid = GetComponent<Graph> ();
		requestManager = GetComponent<PathRequestManager> ();
	}
	public void StartFindPath(Vector3 startPos, Vector3 targetPos){
		StartCoroutine(findPath(startPos, targetPos));
	}
	IEnumerator findPath(Vector3 startPos, Vector3 endPos){

		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (endPos);

		if (targetNode.walkable && startNode.walkable) {
			Heap<Node> openSet = new Heap<Node> (grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node> ();

			openSet.Add (startNode);

			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst ();
				closedSet.Add (currentNode);

				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}

				foreach (Node neighbor in grid.GetNeighbors(currentNode)) {
					if (!neighbor.walkable || closedSet.Contains (neighbor)) {
						continue;
					}

					int newMovementCostToNeighbor = currentNode.Gcost + getDistance (currentNode, neighbor);

					if (newMovementCostToNeighbor < neighbor.Gcost || !openSet.Contains (neighbor)) {
						neighbor.Gcost = newMovementCostToNeighbor;
						neighbor.Hcost = getDistance (neighbor, targetNode);
						neighbor.parent = currentNode;

						if (!openSet.Contains (neighbor)) {
							openSet.Add (neighbor); 
						}
					}
				}
			}
		}
		yield return null;
		if (pathSuccess) {
			waypoints = RetracePath (startNode, targetNode);
		}
		requestManager.FinishedProcessing (waypoints, pathSuccess);
	}
	Vector3[] RetracePath(Node startNode, Node endNode){
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}

		Vector3[] waypoints = SimplifyPath (path);

		//Node [] pathArr = path.ToArray ();
		//Array.Reverse (pathArr);
		Array.Reverse (waypoints);

		path.Reverse ();
		grid.path = path;

		return waypoints;

	}

	Vector3[] SimplifyPath(List<Node> path){
		List<Vector3> waypoints = new List<Vector3> ();
		Vector2 directionOld = Vector2.zero;

		for (int i = 1; i < path.Count; i++) {
			Vector2 directionNew = new Vector2 (path [i - 1].gridX - path[i].gridX, path [i - 1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add (path [i].worldPos);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray ();
	}

	int getDistance(Node nodeA, Node nodeB){
		int disX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
		int disY = Mathf.Abs (nodeA.gridY - nodeB.gridY);

		if (disX > disY) {
			return 14 * disY + 10 * (disX - disY);
		}
		return 14 * disX + 10 * (disY - disX);
		
	}


}

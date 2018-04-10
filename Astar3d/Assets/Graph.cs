using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Graph : MonoBehaviour {

	Node[,] grid;
	public Vector2 worldSize;
	public float nodeRadius;
	public LayerMask unwalkableMask;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	public bool displayPathGizmos;

	void Awake(){
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(worldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(worldSize.y / nodeDiameter);
		//print("alert");
		CreateGrid ();
	}
	public int MaxSize{
		get{ 
			return gridSizeX * gridSizeY;
		}
	}
	void CreateGrid(){
		grid = new Node [gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2;

		for (int i = 0; i < gridSizeX; i++) {
			for (int j = 0; j < gridSizeY; j++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
				grid [i, j] = new Node (walkable, worldPoint, i, j);

			}
		}

	}

	public List<Node> GetNeighbors(Node node){
		List<Node> Neighbors = new List<Node> ();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0) {
					continue;
				} 
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					Neighbors.Add (grid[checkX, checkY]);
				}
			}
		}
		return Neighbors;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition){
		float percentX =  (float) ((worldPosition.x + worldSize.x/2) / worldSize.x);
		float percentY =  (float) ((worldPosition.z + worldSize.y/2) / worldSize.y);
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

		return grid [x, y];

	}

	public List<Node> path;
	void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector3 (worldSize.x, 1, worldSize.y));

		if (displayPathGizmos) {
			if (path != null) {
				foreach (Node n in path) {
					Gizmos.color = Color.gray;
					Gizmos.DrawCube (n.worldPos, Vector3.one * (nodeDiameter - .1f));
				}
			}
		} else {
			if (grid != null) {
				foreach (Node n in grid) {
					Gizmos.color = (n.walkable) ? Color.white : Color.red;
					if (path != null)
					if (path.Contains (n)) {
						Gizmos.color = Color.gray;
					}

					Gizmos.DrawCube (n.worldPos, Vector3.one * (nodeDiameter - .1f));
				}
			}
		}
	}
}

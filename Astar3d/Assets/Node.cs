using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {

	public bool walkable;
	public Vector3 worldPos;

	public int Hcost;
	public int Gcost;

	public int gridX;
	public int gridY;

	public Node parent;

	int heapIndex;

	public Node(bool _walkable, Vector3 _worldPos, int _gridx, int _gridy){
		gridX = _gridx;
		gridY = _gridy;
		walkable = _walkable;
		worldPos = _worldPos;
	}
	public int Fcost{
		get{
			return Gcost+Hcost;
		}
	}
	public int HeapIndex{
		get{ 
			return heapIndex;
		}
		set{ 
			heapIndex = value;
		}
	}
	public int CompareTo(Node node){
		int compare = Fcost.CompareTo (node.Fcost);
		if (compare == 0) {
			compare = Hcost.CompareTo (node.Hcost);
		}
		return -compare;
	}
}

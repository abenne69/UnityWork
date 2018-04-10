
using System.Collections.Generic;
using UnityEngine;

public class Graph {
	public int rows = 0;
	public int cols = 0;
	public Node[] nodes;


	public Graph(int[,] grid){
		rows = grid.GetLength (0);
		cols = grid.GetLength (0);

		nodes = new Node[grid.Length];

		for (int i = 0; i < nodes.Length; i++) {
			var node = new Node ();
			node.label = i.ToString ();
			nodes [i] = node;

		}
		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < cols; c++) {
				var node = nodes [cols * r + c];
				if (grid [r, c] == 1) {
					continue;
				}
				//up
				if (r > 0) {
					node.adjacent.Add (nodes [cols * (r - 1) + c]);
					if(c < cols -1)
						node.adjacent.Add (nodes [cols * (r - 1) + (c+1)]);
					if(c > 0)
						node.adjacent.Add (nodes [cols * (r - 1) + (c-1)]);
				}
				//right
				if (c < cols - 1) {
					node.adjacent.Add (nodes [cols * r + (1 + c)]);
				}
				//down
				if (r < rows -1 ) {
					node.adjacent.Add (nodes [cols * (r + 1) + c]);
					if(c < cols-1)
						node.adjacent.Add (nodes [cols * (r + 1) + (c+1)]);
					if(c > 0)
						node.adjacent.Add (nodes [cols * (r + 1) + (c-1)]);
				}
				//left
				if (c > 0 ) {
					node.adjacent.Add (nodes [cols * r+c-1]);
				}

			}
		}
	}
}

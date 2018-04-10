using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathRequestManager : MonoBehaviour {


	Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
	PathRequest currentPathRequest;

	static PathRequestManager instance;
	Astar pathfinder;

	bool isProcessing;

	void Awake(){
		instance = this;
		pathfinder = GetComponent<Astar> ();
	}

	public static void RequestPath(Vector3 start, Vector3 end, Action<Vector3[], bool> callback){
		PathRequest newRequest = new PathRequest (start, end, callback);
		instance.pathRequestQueue.Enqueue (newRequest);
		instance.TryProcessNext ();
	}
	void TryProcessNext(){
		if (!isProcessing && pathRequestQueue.Count > 0) {
			currentPathRequest = pathRequestQueue.Dequeue ();
			isProcessing = true;
			pathfinder.StartFindPath (currentPathRequest.pathStart, currentPathRequest.pathEnd);
		}
	}
	public void FinishedProcessing(Vector3 [] path, bool success){
		currentPathRequest.callback (path, success);
		isProcessing = false;
		TryProcessNext ();
	}
	struct PathRequest{
		public Vector3 pathStart;
		public Vector3 pathEnd;
		public Action<Vector3[], bool> callback;

		public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback){
			pathStart = _start;
			pathEnd = _end;
			callback = _callback;
		}
	}
}

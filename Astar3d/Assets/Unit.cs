using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	// Use this for initialization
	public Transform target;
	public float speed = 1;
	Vector3[] path;
	int targetIndex;

	void Start(){
		PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);

	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccess){
		if (pathSuccess) {
			path = newPath;
			StopCoroutine ("FollowPath");
			StartCoroutine ("FollowPath");
		}
	}

	IEnumerator FollowPath(){
		Vector3 currentWayPoint = path [0];
		while (true) {
			if (transform.position == currentWayPoint) {
				targetIndex++;
				if (targetIndex >= path.Length) {
					yield break;
				}
				currentWayPoint = path [targetIndex];
			}
			transform.position = Vector3.MoveTowards (transform.position, currentWayPoint, speed);
			yield return null;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class ShowGizmosManager : MonoBehaviour {

	public GameObject m_GizmosParent;

	public static Action PositionChangeCallback;

	private Vector3[] vertices;

	private List<GameObject> gizmos; 

	void Awake()
	{
		vertices = GetComponent<MeshFilter> ().sharedMesh.vertices;

		gizmos = new List<GameObject> ();

		ShowIndexGizmos ();

		for (int i = 0; i < vertices.Length; i++) {
			gizmos.Add (GameObject.Find(m_GizmosParent.name + "/" + i));
		}

		PositionChangeCallback += UpdateGizmosPos;
	}
		
	void Update()
	{
		RaycastHit hit;

		if (Input.GetMouseButtonDown(0) && Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
			Debug.Log (hit.triangleIndex);

			if (hit.triangleIndex >= vertices.Length)
				return;

			IconManager.SetIcon (gizmos[hit.triangleIndex],IconManager.LabelIcon.Red);
		}
	}

	[ContextMenu("HideIndex")]
	void HideIndexGizmos()
	{
		m_GizmosParent.SetActive (false);
	}

	
	[ContextMenu("ShowIndex")]
	void ShowIndexGizmos()
	{
		if (m_GizmosParent.transform.childCount != 0) {
			m_GizmosParent.SetActive (true);

			return;
		}

		for (int i = 0; i < vertices.Length; i++){ 
			AddGizmos ("" + i,transform.TransformPoint(vertices[i]));
		}
	}

	void AddGizmos(string name,Vector3 position)
	{
		GameObject go = Instantiate (Resources.Load("ShowIndexGO") as GameObject);

		go.transform.parent = m_GizmosParent.transform;

		go.name = name;

		go.transform.position = position;

		IconManager.SetIcon (go,IconManager.LabelIcon.Green);
	}

	void UpdateGizmosPos()
	{
		var vertices = GetComponent<MeshFilter> ().sharedMesh.vertices;

		for (int i = 0; i < vertices.Length; i++) {
			gizmos [i].transform.position = transform.TransformPoint(vertices [i]);
		}  
	}
}

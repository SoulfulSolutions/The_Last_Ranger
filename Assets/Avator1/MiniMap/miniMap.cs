using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class miniMap : MonoBehaviour {

	public Transform Target;
	public float zoomLevel=10f;

	Vector2 XRotation=Vector2.right;
	Vector2 YRotation=Vector2.up;

	void LateUpdate()
	{
		XRotation = new Vector2 (Target.right.x, -Target.right.z);
		YRotation = new Vector2 (-Target.forward.x, Target.forward.z);
	}

	public Vector2 TransformPosition(Vector3 position)
	{
		Vector3 offset = position - Target.position;
		Vector2 newposition = offset.x * XRotation;
		newposition += offset.z * YRotation;
		newposition *= zoomLevel;
//		
	

//		newposition *= zoomLevel;
		return newposition;

	}
	public Vector3 TransformRotation(Vector3 rotation)
	{
		return new Vector3 (0,0,Target.eulerAngles.y-rotation.y);
	}

	public Vector2 MoveInside(Vector2 point)
	{
		Rect mapRect=GetComponent<RectTransform>().rect;
		point = Vector2.Max (point, mapRect.min);
		point = Vector2.Min (point,mapRect.max);
		return point;
 	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Cursor.visible = true;

	}
}

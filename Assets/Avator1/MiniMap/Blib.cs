using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Blib : MonoBehaviour
{
	public Transform Target; 
	miniMap map;
	RectTransform myRectTransform;
	public bool KeepInBounds=true;
	public bool LockScale=false;

	public bool LockRotation=false;

	// Use this for initialization
	void Start ()
	{
		map = GetComponentInParent<miniMap>();
		myRectTransform = GetComponent<RectTransform>();
	}

    void LateUpdate()
    { 

        if (Target == null)
        {

            Destroy(gameObject);
        }
        else
        {
			if(Target.tag!="Player")
			{

				if(Target.GetComponent<EnemyAI>().isAttacking==true)
				{
					Debug.Log(Target.GetComponent<EnemyAI>().isAttacking+" attack");
					gameObject.GetComponent<Image> ().color =  Color.blue;
				}
				else
				{
					gameObject.GetComponent<Image> ().color =  Color.red;
				}
			}

            Vector2 newPosistion = map.TransformPosition(Target.position);
            if (KeepInBounds)
               newPosistion = map.MoveInside(newPosistion);
			if(!LockScale)
				myRectTransform.localScale=new Vector3(map.zoomLevel,map.zoomLevel,1);

			if(!LockRotation)
				myRectTransform.localEulerAngles=map.TransformRotation(Target.eulerAngles);

            myRectTransform.localPosition = newPosistion;
        }
    }




	// Update is called once per frames 
}

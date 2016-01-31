using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour {
	
	float targetAngle= 0;
	float degreesPerClick= 2;
	float secsPerClick= 0.3f;

	private float curAngle= 0f;
	private float startAngle= 0f;
	private float startTime=0f;
	private Vector3 screenPoint;
	private Vector3 offset;

	void OnMouseDown () {

		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

}

	void OnMouseDrag()	{

		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

		transform.position = curPosition;

	}

	/*void OnMouseEnter()		{

	
		float clicks= Mathf.Round(Input.GetAxis("Mouse ScrollWheel") * 100);
				if (clicks != 0) {
					targetAngle += clicks * degreesPerClick;
					startAngle = curAngle;
					startTime = Time.time;
				}

				float t= (Time.time - startTime) / secsPerClick;
				if (t <= 1) {
					curAngle = Mathf.Lerp(startAngle, targetAngle, t);
					// finally, do the actual rotation
					transform.eulerAngles.y = curAngle;
				}


			}*/
		}


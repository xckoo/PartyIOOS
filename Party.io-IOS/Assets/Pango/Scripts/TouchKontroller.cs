using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;

public class TouchKontroller : MonoBehaviour ,IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerUpHandler,IPointerDownHandler{

	Vector2 oldPos;
	Vector2 diff;
	Vector2 currentDataPos;
	Vector2 firstPos;
	float oldAngle;
	float currentAngle;
	float angleDiff;
	public Image myImage;
	public PlayerController_A plc;
	public bool pressed = false;

	bool down=true;
	float drag_first_time=0,drag_end_time=100;
    public float time_first, time_end=0;
	bool dragger=false;
	public void OnBeginDrag(PointerEventData eventData)
	{	dragger=false;
		

		oldPos = eventData.position;
	}

	public void OnPointerUp(PointerEventData data){

		drag_end_time = Time.time;
        time_end = Time.time;

        
        if(first)
        {
            if(time_end>0.3f + time_first)
            {
               // dragger = true;
            }

        }
        if (dragger)
        {
            if (drag_end_time < drag_first_time + 1f)
            {


                if (plc.kaldiririyor)
                {
                    time_end = Time.time;
                    plc.BeldenFirlat();
                    dragger = false;
                }

            }

        }           

	}

	int kontrol=0;
	public float timer = 0,firlatma_sayisi=0;
	float temp_timer=0;
	bool first=true,firlat=true;
	public void OnPointerDown(PointerEventData data){
		drag_first_time = Time.time;
		firstPos = data.position;

        if (first)
            first = false;

        if(!plc.kaldiririyor)
        {
            first =true;

        }
        else {
            dragger = true;
            Debug.Log("-------------------------Drager ---------------------");
        }
        

        if (!plc.dustu)
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);

        
        if (plc.dustu)
            MMVibrationManager.Haptic(HapticTypes.Failure);


		if (!plc.duvarda) {
                plc.grab = true;
			CancelInvoke ("UnGrab");
			Invoke ("UnGrab", 0.4f);
        }
        time_first = Time.time;
        if (plc.dustu && !plc.duvarda ) {
			if(plc.uyanis<plc.uyanis_sure)
				plc.uyanis++;
		}
        if (plc.duvarda)
        {
            if (plc.uyanis_duvar < plc.uyanis_sure)
                plc.uyanis_duvar++;
        }
    }

	private void UnGrab(){
		plc.grab = false;
	}

	 

	public void OnDrag(PointerEventData data)
	{	

	//	Debug.Log ("Ondragggggggggggggg");

		currentDataPos = data.position;
		currentAngle = GetAngle (firstPos, currentDataPos);
		plc.donuyor = true;
		plc.angle = GetAngle (firstPos, currentDataPos);
	
		//plc.angle  = Mathf.Atan2(currentDataPos.y,currentDataPos.x);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		plc.donuyor = false;
	}

	void FixedUpdate(){
		

		diff.x = (currentDataPos - oldPos).x*(Screen.width/1080f);
		diff.y = (currentDataPos - oldPos).y*(Screen.height/1920f);
		angleDiff = currentAngle - oldAngle;
		plc.x = diff.x;
		plc.y = diff.y;
		oldPos = currentDataPos;
		oldAngle = currentAngle;

	}

	public float GetAngle(Vector2 vec1, Vector2 vec2)
	{
		float angle = Mathf.Atan2(vec2.y-vec1.y, vec2.x-vec1.x) * Mathf.Rad2Deg;

		return -angle + 90;
		//return angle;
	}


}

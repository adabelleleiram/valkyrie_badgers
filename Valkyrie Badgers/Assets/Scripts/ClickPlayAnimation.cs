using UnityEngine;
using System.Collections;

public class ClickPlayAnimation : MonoBehaviour 

{
		
	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0))
		{	
				
			// play the attached sound
			//GetComponent<AudioSource>().Play();

			//trigger animation
                        {
                            Animator anim = GetComponent<Animator>();
                            anim.SetTrigger("Flying_Room1");
                        
                        }

		}
	}
}
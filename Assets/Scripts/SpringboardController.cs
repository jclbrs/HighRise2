using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringboardController : MonoBehaviour
{
    public List<float> GetSpringboardXPosns()
	{
		List<float> xPosns = new List<float>();
		xPosns.Add(transform.Find("Springs/Spring0").position.x);
		xPosns.Add(transform.Find("Springs/Spring1").position.x);
		xPosns.Add(transform.Find("Springs/Spring2").position.x);
		xPosns.Add(transform.Find("Springs/Spring3").position.x);
		xPosns.Add(transform.Find("Springs/Spring4").position.x);
		xPosns.Add(transform.Find("Springs/Spring5").position.x);
		return xPosns;
	}
}

using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<float> _playerXBelowBins;
    private List<float> _springboardXPosns;
    private PlayerState _currentState;

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeSettings(List<float> binXPosns, float xOffSetFromBin, List<float> springboardXPosns)
	{
        _springboardXPosns = springboardXPosns;
        _playerXBelowBins = new List<float>();
        foreach (float binXPosn in binXPosns)
            _playerXBelowBins.Add(binXPosn + xOffSetFromBin);
        _currentState = PlayerState.IdleBySpringboard;
        transform.position = new Vector3(_playerXBelowBins[0], transform.position.y);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineBrain))]
public class GoalCameraSequencer : MonoBehaviour
{
    public GameObject PlayerCamera;
    public float TimeBetweenCameras = 1f;

    private GameObject[] _goalCameras;
    private CinemachineBrain _cinemachineBrain;

    void Awake()
    {
        _goalCameras = GameObject.FindGameObjectsWithTag("GoalCamera");
        TryGetComponent(out _cinemachineBrain);
        ToggleGoalCameras(false);
    }

    void Start()
    {
        if(_goalCameras.Length > 0)
        {
            StartCoroutine(SequenceCameras());
        }
    }

    void ToggleGoalCameras(bool value)
    {
        foreach(var camera in _goalCameras)
        {
            camera.SetActive(value);
        }
    }

    IEnumerator SequenceCameras()
    {
        float waitTime = TimeBetweenCameras + _cinemachineBrain.m_DefaultBlend.m_Time;
        yield return new WaitForSeconds(.1f);

        PlayerCamera.SetActive(false);
        _goalCameras[0].SetActive(true);

        for(int i=1; i<_goalCameras.Length; i++)
        {
            yield return new WaitForSeconds(waitTime);

            _goalCameras[i-1].SetActive(false);
            _goalCameras[i].SetActive(true);
        }

        yield return new WaitForSeconds(waitTime);
        _goalCameras[_goalCameras.Length - 1].SetActive(false);
        PlayerCamera.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffects : MonoBehaviour
{
    public List<AudioSource> audioSourceList;
    [SerializeField] float _timeIncreese = 1.5f;
    [Range(0.01f, 0.9f)][SerializeField] float _timeslowStart = 0.1f;
    bool isSlowingDown = false;

    private void Update()
    {
        if (isSlowingDown)
        {
            Time.timeScale += _timeIncreese * Time.deltaTime;

            for (int i = 0; i < audioSourceList.Count; i++)
            {
                audioSourceList[i].pitch = Time.timeScale;
            }

            if (Time.timeScale >= 1)
            {
                isSlowingDown = false;
                for (int i = 0; i < audioSourceList.Count; i++)
                {
                    audioSourceList[i].pitch = 1;
                }
            }
        }
    }

    public void TimeSlowdown()  //When activated, slow time
    {
        Time.timeScale = _timeslowStart;
        isSlowingDown = true;
    }
}

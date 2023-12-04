using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LoadGameAfterTimeLine : MonoBehaviour
{
    private void Start()
    {
        GetComponent<PlayableDirector>().stopped += LoadGameAfterTimeLine_paused;
    }

    private void LoadGameAfterTimeLine_paused(PlayableDirector obj)
    {
        SceneManager.LoadScene("Level0");
    }
}

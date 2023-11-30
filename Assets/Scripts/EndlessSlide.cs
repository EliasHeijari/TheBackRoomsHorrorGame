using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndlessSlide : MonoBehaviour
{
    [SerializeField] private int endBuildIndex = 1;

    private void Start()
    {
        GetComponent<PlayableDirector>().stopped += EndlessSlide_paused;
    }

    private void EndlessSlide_paused(PlayableDirector obj)
    {
        SceneManager.LoadScene(endBuildIndex);
    }
}

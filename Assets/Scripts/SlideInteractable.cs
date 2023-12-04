using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SlideInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WallGuide[] wallGuides;
    bool guide0 = false;
    bool guide1 = false;
    bool guide2 = false;
    [SerializeField] private int sceneBuildIndex = 1;
    bool isReadyToSlide = false;

    private void Start()
    {
        foreach (var guide in wallGuides)
        {
            guide.OnGuideSeen += Guide_OnGuideSeen;
        }
        if (PlayerPrefs.GetInt("Guide0") == 1)
        {
            guide0 = true;
        }
        if (PlayerPrefs.GetInt("Guide1") == 1)
        {
            guide1 = true;
        }
        if (PlayerPrefs.GetInt("Guide2") == 1)
        {
            guide2 = true;
        }
        if (guide0 && guide1 && guide2) isReadyToSlide = true;
    }

    private void Guide_OnGuideSeen(object sender, System.EventArgs e)
    {
        for(int i = 0; i <= 2; ++i)
        {
            switch (i)
            {
                case 0:
                    if (wallGuides[i].HasSeen())
                    {
                        guide0 = true;
                        PlayerPrefs.SetInt("Guide0", 1);
                    }
                    else
                    {
                        guide0 = false;
                        PlayerPrefs.SetInt("Guide0", 0);
                    }
                    break;
                case 1:
                    if (wallGuides[i].HasSeen())
                    {
                        guide1 = true;
                        PlayerPrefs.SetInt("Guide1", 1);
                    }
                    else
                    {
                        guide1 = false;
                        PlayerPrefs.SetInt("Guide1", 0);
                    }
                    break;
                case 2:
                    if (wallGuides[i].HasSeen())
                    {
                        guide2 = true;
                        PlayerPrefs.SetInt("Guide2", 1);
                    }
                    else
                    {
                        guide2 = false;
                        PlayerPrefs.SetInt("Guide2", 0);
                    }
                    break;
            }
        }
        if (guide0 && guide1 && guide2) isReadyToSlide = true;
    }

    public void Interact(Transform interactorTransform)
    {
        if (isReadyToSlide)
        {
            playerController.enabled = false;
            GetComponent<PlayableDirector>().Play();
            GetComponent<PlayableDirector>().stopped += SlideInteractable_played;
            PlayerPrefs.DeleteKey("Guide0");
            PlayerPrefs.DeleteKey("Guide1");
            PlayerPrefs.DeleteKey("Guide2");
        }
    }

    private void SlideInteractable_played(PlayableDirector obj)
    {
        Debug.Log("Load scene");
        SceneManager.LoadScene(sceneBuildIndex);
        GetComponent<PlayableDirector>().played -= SlideInteractable_played;
    }

    public string GetInteractText()
    {
        return "Go To The Slide";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}

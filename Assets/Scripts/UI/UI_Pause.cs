using System.Collections;
using UnityEngine;

public class UI_Pause : MonoBehaviour
{
    [Header("Dependencies")]
    [Tooltip("The script that manages the state of the paused button.")]
    [SerializeField] UI_PauseButtonManager buttonManager;

    [SerializeField] Canvas canvas;

    [Space(12)]
    [SerializeField] GameObject pausePanel;
    [SerializeField] Animator pausePanelAnimator;

    [Space(6)]
    [SerializeField] GameObject innerPausePanel;
    [SerializeField] Animator innerPausePanelAnimator;
    [SerializeField] AnimationClip innerPausePanelExitAnimation;
    [SerializeField] AnimationClip innerPausePanelSwitchedExitAnimation;

    [Space(6)]
    [SerializeField] GameObject innerSettingsPanel;
    [SerializeField] Animator innerSettingsPanelAnimator;
    [SerializeField] AnimationClip innerSettingsPanelExitAnimation;
    [SerializeField] AnimationClip innerSettingsPanelSwitchedExitAnimation;

    bool isPaused;

    bool settingsPanelOpened;

    void Awake()
    {
        canvas.enabled = false;
        //pausePanel.SetActive(false);
        innerPausePanel.SetActive(true);
        innerSettingsPanel.SetActive(false);
    }

    public bool GetPausedState()
    {
        return isPaused;
    }

    public void ChangePauseState()
    {
        isPaused = !isPaused;
        buttonManager.changeSprite(isPaused);

        if (isPaused)
        {
            innerPausePanel.SetActive(true);
            innerSettingsPanel.SetActive(false);
            settingsPanelOpened = false;

            Pause();
        }
        else
        {
            StartCoroutine(UnPause());
        }
    }

    void Pause()
    {
        settingsPanelOpened = false;

        canvas.enabled = true;
        //pausePanel.SetActive(true);
        pausePanelAnimator.SetTrigger("paused");

        Time.timeScale = 0f;

        innerPausePanelAnimator.SetTrigger("paused");
    }

    IEnumerator UnPause()
    {
        Time.timeScale = 1f;
        pausePanelAnimator.SetTrigger("un paused");

        if (settingsPanelOpened == false)
        {
            innerPausePanelAnimator.SetTrigger("un paused");
            yield return new WaitForSeconds(innerPausePanelExitAnimation.length);
        }
        else
        {
            innerSettingsPanelAnimator.SetTrigger("closed");
            yield return new WaitForSeconds(innerSettingsPanelExitAnimation.length);
        }

        canvas.enabled = false;
        //pausePanel.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        innerSettingsPanel.SetActive(true);
        settingsPanelOpened = true;

        
        innerSettingsPanelAnimator.SetTrigger("opened");
        innerPausePanelAnimator.SetTrigger("switch exit");
        //innerSettingsPanelAnimator.SetTrigger("switch on");

        StartCoroutine(disableInnerPausePanel(innerPausePanelSwitchedExitAnimation.length));
    }

    IEnumerator disableInnerPausePanel(float delay) 
    {
         
        //print("Disable inner pause");innerPausePanel.SetActive(false);

        yield return new WaitForSeconds(delay);
        print("paused");
        innerPausePanel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        print("Main Menu");
        Application.Quit();
    }

    public void BackToPause()
    {
        innerPausePanel.SetActive(true);

        innerPausePanelAnimator.SetTrigger("switch on");
        innerSettingsPanelAnimator.SetTrigger("switch exit");
        innerPausePanelAnimator.SetTrigger("switch enter");

        StartCoroutine(disableInnerSettingsPanel(innerSettingsPanelSwitchedExitAnimation.length));

        settingsPanelOpened = false;
    }

    IEnumerator disableInnerSettingsPanel(float delay)
    {
        //print("Disable inner settings"); innerSettingsPanel.SetActive(false);

        yield return new WaitForSeconds(delay);
        innerSettingsPanel.SetActive(false);
    }
}

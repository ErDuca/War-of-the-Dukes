using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIBehaviour : MonoBehaviour
{
    [Header("Info card related")]
    [SerializeField] private float infoTurnAnimTime;
    [SerializeField] private GameObject infoImage;
    [SerializeField] private GameObject infoBlock;
    [SerializeField] private Animator infoAnimator;

    [Header("Turns related")]
    [SerializeField] private GameObject thinkingIcon;
    [SerializeField] private AnimationClip turnSwapAnimation;
    [SerializeField] private Text turnText;
    [SerializeField] private Color colorBlue;
    [SerializeField] private Color colorRed;

    [Header("Timer related")]
    [SerializeField] private Text timeText;
    [SerializeField] private Slider timeSlider; 
    [SerializeField] private GameObject timeSliderFill;
    [SerializeField] private GameObject timeSliderImageLeft;
    [SerializeField] private GameObject timeSliderImageRight;
    private bool changingTurn; //TODO: this can be used to prevent raycasts during animations (alternatively make the black plane a raycast target)
    private float timeRemaining;
    private float timeToDisplay;
    [SerializeField] private float turnTime;

    [Header("Pause menu related")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject eventSystem;

    [Header("Transitions related")]
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private Animator gameAnimator;

    private void Start()
    {
        //TODO: Implement who starts (get value from other scene)
        transitionAnimator.SetTrigger("sceneStart");
        timeRemaining = turnTime;
        eventSystem.SetActive(false);
        changingTurn = true;
        //pauseMenu.SetActive(false);
        infoBlock.SetActive(false);
        StartCoroutine(GameStartAnimationCoroutine());
    }

    private IEnumerator GameStartAnimationCoroutine()
    {
        yield return new WaitUntil(() => gameAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle"));
        if (FirstSelectScript.startingPlayer == 0)
        {
            PlayersTurnStart();
        }
        else if (FirstSelectScript.startingPlayer == 1)
        {
            OpponentsTurnStart();
        }
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            if (!changingTurn)
            {
                timeRemaining -= Time.deltaTime;
                //We add 1 so it's more intuitive (if I set 70 secs it starts as 01:10 and not 01:09; time ends as soon as displaying 0)
                timeToDisplay = timeRemaining + 1;
                //Sets time text to minutes:seconds, both always displayed as 2 digits
                timeText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timeToDisplay / 60), Mathf.FloorToInt(timeToDisplay % 60));
                timeSlider.value = timeRemaining / turnTime;
            }
        }
        else
        {
            //TODO: add real action for when time runs out
            Debug.Log("TIME'S UP");
        }
    }

    public void PauseGame() => StartCoroutine(PauseGameCoroutine());
    private IEnumerator PauseGameCoroutine()
    {
        eventSystem.SetActive(false);
        pauseMenu.SetActive(true);
        gameAnimator.SetBool("paused", true);
        yield return new WaitUntil(() => gameAnimator.GetCurrentAnimatorStateInfo(0).IsName("paused"));
        eventSystem.SetActive(true);
    }

    public void ResumeGame() => StartCoroutine(ResumeGameCoroutine());
    private IEnumerator ResumeGameCoroutine()
    {
        eventSystem.SetActive(false);
        gameAnimator.SetBool("paused", false);
        yield return new WaitUntil(() => gameAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle"));
        pauseMenu.SetActive(false);
        eventSystem.SetActive(true);
    }

    //TODO: Replace int with proper GameObject type
    public void ShowPieceInfo(int piece)
    {
        infoBlock.SetActive(true);
    }
    public void HidePieceInfo() => StartCoroutine(HidePieceInfoCoroutine());
    private IEnumerator HidePieceInfoCoroutine()
    {
        infoAnimator.SetTrigger("hide");
        yield return new WaitUntil(() => infoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hidden"));
        infoBlock.SetActive(false);
    }

    //TODO: Replace int with proper GameObject type
    //PieceInfoTurning
    public void PieceInfoTurn(int piece) => StartCoroutine(PieceInfoTurnCoroutine(piece));
    private IEnumerator PieceInfoTurnCoroutine(int piece)
    {
        float elapsedTime = 0f;
        while (elapsedTime < infoTurnAnimTime)
        {
            //TODO: get values from current scale
            infoImage.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0f, 1f, 1f), (elapsedTime / infoTurnAnimTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        infoImage.transform.localScale = Vector3.zero;
        //TODO: add image change
        elapsedTime = 0f;
        while (elapsedTime < infoTurnAnimTime)
        {
            //TODO: get values from current scale
            infoImage.transform.localScale = Vector3.Lerp(new Vector3(0f, 1f, 1f), Vector3.one, (elapsedTime / infoTurnAnimTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        infoImage.transform.localScale = Vector3.one;

        yield return null;
    }

    public void PlayersTurnStart() => StartCoroutine(PlayersTurnStartCoroutine());
    private IEnumerator PlayersTurnStartCoroutine()
    {
        eventSystem.SetActive(false);
        //changingturn stops time during turn swap animations
        changingTurn = true;
        //TODO: Implement true behaviour for thinking icon (it should disappear when the opponent's move animations start playing out)
        thinkingIcon.SetActive(false);
        gameAnimator.SetTrigger("playersTurn");
        yield return new WaitForSeconds(turnSwapAnimation.length / 2);

        //Everything tunr-related gets moved to the left side
        timeSliderImageRight.SetActive(false);
        timeSliderImageLeft.SetActive(true);
        turnText.alignment = TextAnchor.MiddleLeft;
        turnText.text = "PLAYER'S\nTURN";
        timeSlider.direction = Slider.Direction.LeftToRight;
        timeSliderFill.GetComponent<Image>().color = colorBlue;

        yield return new WaitForSeconds(turnSwapAnimation.length / 2);

        timeRemaining = turnTime;
        changingTurn = false;
        eventSystem.SetActive(true);
    }

    public void OpponentsTurnStart() => StartCoroutine(OpponentsTurnStartCoroutine());
    private IEnumerator OpponentsTurnStartCoroutine()
    {
        eventSystem.SetActive(false);
        //changingturn stops turn timer during turn swap animations
        changingTurn = true;
        gameAnimator.SetTrigger("opponentsTurn");
        yield return new WaitForSeconds(turnSwapAnimation.length);

        //Everything tunr-related gets moved to the right side
        timeSliderImageLeft.SetActive(false);
        timeSliderImageRight.SetActive(true);
        turnText.alignment = TextAnchor.MiddleRight;
        turnText.text = "OPPONENT'S\nTURN";
        timeSlider.direction = Slider.Direction.RightToLeft;
        timeSliderFill.GetComponent<Image>().color = colorRed;

        //TODO: Implement true behaviour for thinking icon (it should disappear when the opponent's move animations start playing out)
        thinkingIcon.SetActive(true);
        timeRemaining = turnTime;
        changingTurn = false;
        eventSystem.SetActive(true);
    }

    //TODO: Remove and implement a "giving up" system if you want to go back to main menu and automatically lose
    public void ToMM()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimelineController : MonoBehaviour
{
    private PlayableDirector playableDirector;
    public Slider timelineSlider; // Assign this in the Unity Inspector
    public float timeStep = 0.5f; // Adjust this to control how much it moves per key press
    private bool isScrubbing = false; // Flag to check if user is dragging the slider

    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();

        // Ensure the slider's value and range are correctly set
        if (timelineSlider != null)
        {
            timelineSlider.minValue = 0;
            timelineSlider.maxValue = (float)playableDirector.duration; // Explicit cast from double to float
            timelineSlider.value = 0;
            timelineSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        playableDirector.Pause(); // Start paused
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Toggle Play/Pause
        {
            if (playableDirector.state == PlayState.Playing)
                playableDirector.Pause();
            else
                playableDirector.Play();
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Pause Timeline
        {
            playableDirector.Pause();
        }

        if (Input.GetKeyDown(KeyCode.L)) // Restart Timeline
        {
            //playableDirector.time = 0;
            //playableDirector.Evaluate(); // Force update
            //UpdateSlider();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //if (Input.GetKey(KeyCode.RightArrow)) // Move Forward
        //{
        //    ScrubTimeline(timeStep);
        //}

        //if (Input.GetKey(KeyCode.LeftArrow)) // Move Backward
        //{
        //    ScrubTimeline(-timeStep);
        //}

        // Update slider only when it's not being dragged
        if (!isScrubbing && timelineSlider != null && playableDirector.state == PlayState.Playing)
        {
            timelineSlider.value = (float)playableDirector.time; // Explicit cast
        }
    }

    void ScrubTimeline(float step)
    {
        if (playableDirector.state == PlayState.Playing)
        {
            playableDirector.Pause(); // Pause to prevent accidental playing while scrubbing
        }

        double newTime = playableDirector.time + step;
        newTime = Mathf.Clamp((float)newTime, 0f, (float)playableDirector.duration); // Ensure within bounds

        playableDirector.time = newTime;
        playableDirector.Evaluate();
        UpdateSlider();
    }

    void OnSliderValueChanged(float value)
    {
        if (playableDirector != null && !isScrubbing) // Only update if not actively scrubbing
        {
            playableDirector.time = value;
            playableDirector.Evaluate();
        }
    }

    public void StartScrubbing()
    {
        isScrubbing = true;
        playableDirector.Pause(); // Pause while scrubbing
    }

    public void StopScrubbing()
    {
        isScrubbing = false;
        playableDirector.Evaluate();
    }

    void UpdateSlider()
    {
        if (timelineSlider != null)
        {
            timelineSlider.value = (float)playableDirector.time; // Explicit cast
        }
    }
}

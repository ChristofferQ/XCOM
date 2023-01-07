using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    private float timeDuration = 3F * 60;
    public static Timer Instance; 

    [SerializeField]
    private bool countDown = true;

    private float timer;

    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI seperator;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;

    private float flashTimer;
    private float flashDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (countDown)
        {
            timer = timeDuration;
        }else
        {
            timer = 0;
        }
        SetTextDisplay(true);
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown && timer > 6)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay(timer);
            
        } else if (!countDown)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);
        } else if ((timer < 6) && (timer > 0))
            {
                Flash();
                timer -= Time.deltaTime;
                UpdateTimerDisplay(timer);
            }else 
        {
            PlayerManager.Instance.end();
            ResetTimer();
        }
        
        
    }

    public void ResetTimer()
    {
        timer = timeDuration;
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }    

    private void Flash()
    {
        if (countDown && timer != 0)
        {
            //timer = 0;
            UpdateTimerDisplay(timer);
        } 
        if (!countDown && timer != timeDuration)
        {
            timer = timeDuration;
            UpdateTimerDisplay(timer);
        } 
        if (flashTimer <= 0)
        {
            flashTimer = flashDuration;
        }else if(flashTimer >= flashDuration / 2)
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(false);
        }else
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
        }
    }

    private void SetTextDisplay(bool enabled)
    {
        firstMinute.enabled = enabled;
        secondMinute.enabled = enabled;
        seperator.enabled = enabled;
        firstSecond.enabled = enabled;
        secondSecond.enabled = enabled;
    }
}

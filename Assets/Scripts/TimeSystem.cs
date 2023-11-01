using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;









public class TimeSystem : MonoBehaviour
{

    private static TimeSystem Instance;

    private float _timeSpeed;

    private Vector3 rotationVector;

    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private TMP_Text day;


    [SerializeField]
    private float _hour;
    [SerializeField]
    private float _minute;
    
    public float _day;


    


    [SerializeField]
    private GameObject hourHand;
    [SerializeField]
    private GameObject minutehand;

    [SerializeField]
    private GameObject AttributeManager;

    private AttributeManager _am;



    private bool tv = false;
    private bool game = false;
    private bool work = false;
    private bool phone = false;
    private bool nap = false;
    private bool read = false;
    private bool gym = false;






    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;


        rotationVector = new Vector3(0, 0, 1);

        if (AttributeManager != null)
        {
            _am = AttributeManager.GetComponent<AttributeManager>();
        }


        //hourHand = GameObject.FindGameObjectWithTag("hour");
        //minutehand = GameObject.FindGameObjectWithTag("minute");
        



        


            _day = 1;
            _minute = 0;
            _hour = 8;
            _timeSpeed = 6;
            
          DontDestroyOnLoad(gameObject);

        
    
        
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(hourHand != null && minutehand != null)
        {
            _minute += _timeSpeed * Time.deltaTime;
            _am.IncreaseFatigue(0.08f * _timeSpeed* Time.deltaTime );
            _am.DropHappiness(0.05f * _timeSpeed* Time.deltaTime);

            ChangeAttribute();

            if (_minute > 60)
            {
                _hour++;
                _minute = 0;
            }


            if(_hour >= 24)
            {
                _hour = 8;
                GoNextDay(); //reload scene, go to next day
            }

            //Update Clock UI
            hourHand.transform.localEulerAngles = -rotationVector * ((_hour + (_minute / 60)) / 12) * 360;
            minutehand.transform.localEulerAngles = -rotationVector * (_minute / 60 * 360 )  ;
        }


        if(text != null)
        {
            if(_hour > 12)
            {
                text.SetText("PM");     
            }
            else
            {
                text.SetText("AM");
            }
        }
        day.SetText("Day " + _day);


    }

    public void SpeedUp(float newTimeSpeed)
    {
        _timeSpeed = newTimeSpeed;
    }


    public void ResetTimeSpeed()
    {
        _timeSpeed = 6f;
    }



    public void ResetAction()
    {
        tv = false;
        game = false;
        work = false;
        phone = false;
        nap = false;
        read = false;
        gym = false;
    }



    public void StartTV()
    {
        tv = true;
    }
    public void StartGame()
    {
        game = true;
    }
    public void StartWork()
    {
        work = true;
    }
    public void StartPhone()
    {
        phone = true;
    }
    public void StartNap()
    {
        nap = true;
    }
    public void StartRead()
    {
        read = true;
    }
    public void StartGym()
    {
        gym = true;
    }



    public void GoNextDay()
    {
        
        
        ResetTimeSpeed();
        ResetAction();

        StartCoroutine("LoadScene");


       
        

        //reload scene, go to next day
    }




    private void ChangeAttribute()
    {

        if (tv)
        {
            _am.DropFatigue(0.15f * _timeSpeed * Time.deltaTime);
            _am.IncreaseHappiness(0.2f * _timeSpeed * Time.deltaTime);
        }

        else if (game)
        {
            _am.DropFatigue(0.04f * _timeSpeed * Time.deltaTime);
            _am.IncreaseHappiness(0.3f * _timeSpeed * Time.deltaTime);
        }

        else if (work)
        {
            _am.IncreaseFatigue(0.15f * _timeSpeed * Time.deltaTime);
            _am.IncreaseDeposit();
            _am.DropHappiness(0.08f * _timeSpeed * Time.deltaTime);
        }

        else if (phone)
        {
            
            _am.IncreaseHappiness(0.15f * _timeSpeed * Time.deltaTime);
            _am.DropFatigue(0.06f * _timeSpeed * Time.deltaTime);
        }

        else if (nap)
        {
            _am.DropFatigue(0.4f * _timeSpeed * Time.deltaTime);
            _am.IncreaseHappiness(0.01f * _timeSpeed * Time.deltaTime);
        }

        else if (read)
        {
            _am.IncreasePotential(0.05f * _timeSpeed * Time.deltaTime);
            _am.IncreaseFatigue(0.05f * _timeSpeed * Time.deltaTime);
            _am.IncreaseHappiness(0.04f * _timeSpeed * Time.deltaTime);
        }

        else if (gym)
        {
            _am.IncreasePotential(0.04f * _timeSpeed * Time.deltaTime);
            _am.IncreaseFatigue(0.2f * _timeSpeed * Time.deltaTime);
            _am.IncreaseHappiness(0.065f * _timeSpeed * Time.deltaTime);
        }
    }


    IEnumerator LoadScene()
    {
        GameObject crossfade = GameObject.FindGameObjectWithTag("crossfade");
        crossfade.GetComponent<Animator>().SetTrigger("transition");

        yield return new WaitForSeconds(2.5f);


        _hour = 8;
        _minute = 0;
        _day++;
        _am.SpendDeposit(30);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

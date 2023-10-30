using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AttributeManager : MonoBehaviour
{
    private static AttributeManager Instance;

    [SerializeField]
    private float _fatigue;
    [SerializeField]
    private float _happiness;
    [SerializeField]
    private float _deposit;
    [SerializeField]
    private float _potential;

    public GameObject[] backgroundObjects;

    public TMP_Text _fatigueText;
    public TMP_Text _hapineesText;
    public TMP_Text _potentialText;
    public TMP_Text _depositText;

    [SerializeField]
    private GameObject TimeSystem;
    private TimeSystem _ts;


    public Sprite[] _sprites;
    public Sprite[] _sprites2;




    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _fatigue = 10;
        _happiness = 50;
        _deposit = 100;
        _potential = 10;

        _ts = TimeSystem.GetComponent<TimeSystem>();
        
    }

   
    void Update()
    {
        _fatigueText.SetText("Fatigue: " + _fatigue.ToString("F0") + " %");
        _hapineesText.SetText($"Happiness: {_happiness.ToString("F0")} %");
        _potentialText.SetText("Potential: " + _potential.ToString("F0") + " %");
        _depositText.SetText("Bank deposit: " + _deposit.ToString("F0") + " $");



        //colour control

        SetColour();






        if(_deposit<=0)
        {
            //gg
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                if (o.name != "Audio Source")
                {
                    Destroy(o);
                }
            }

            SceneManager.LoadScene(5);
        }

        if (_happiness <= 0)
        {
            //depression
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                if (o.name != "Audio Source")
                {
                    Destroy(o);
                }
            }

            SceneManager.LoadScene(4);
        }

        if (_fatigue >= 100)
        {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                if (o.name != "Audio Source")
                {
                    Destroy(o);
                }
            }

            SceneManager.LoadScene(3);
        }
    }


    public void IncreaseFatigue(float f)
    {
        if (_fatigue+f < 100)
        {
            _fatigue += f;
        }
        else
        {
            _fatigue = 100f;
        }
    }

    public void IncreaseHappiness(float f)
    {
        if (_happiness+f < 100)
        {
            _happiness += f;
        }
        else
        {
            _happiness = 100f;
        }
    }

    public void IncreaseDeposit()
    {
        _deposit += 10f * (_potential/100) *Time.deltaTime;
    }

    public void IncreasePotential(float f)
    {
        if (_potential+f < 100)
        {
            _potential += f;
        }
        else
        {
            _potential = 100f;
        }
    }



    public void DropFatigue(float f)
    {
        if (_fatigue - f >= 0)
        {
            _fatigue -= f;
        }
        else
        {
            _fatigue = 0;
        }
    }

    public void DropHappiness(float f)
    {
        if (_happiness - f >= 0)
        {
            _happiness -= f;
        }
        else
        {
            _happiness = 0;
        }
    }

    public void SpendDeposit(float f)
    {
        _deposit -= f;
    }


    private void SetColour()
    {
        if (_ts._day > 15)
        {

            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                if (o.name != "Audio Source")
                {
                    Destroy(o);
                }
            }

            SceneManager.LoadScene(6);
        }

        if (_fatigue < 35 && _happiness > 85 && _ts._day == 15)
        {
            //WIN

            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                if (o.name != "Audio Source")
                {
                    Destroy(o);
                }
            }

            SceneManager.LoadScene(7);

        }

        if (_fatigue < 40 && _happiness > 70 && _ts._day > 12)
        {
            
            backgroundObjects[6].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourwindow1");
            backgroundObjects[7].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourwindow1");
            backgroundObjects[8].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourwindow2");
            


            GameObject.Find("water").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourdrink");
            GameObject.Find("tv").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourtv");
            GameObject.Find("bed").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourbed");

            GameObject.Find("pc").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourpc open");
        }



        if (_fatigue < 45 && _happiness > 60 && _ts._day > 8)
        {
           
            backgroundObjects[4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourshelf");
            backgroundObjects[5].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourtable");
            


            GameObject.Find("gym").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourgym");
            GameObject.Find("sofa").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("coloursofa");

            GameObject.Find("pc").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourpc open");
        }


        if(_fatigue<50 && _happiness > 50 && _ts._day> 3)
        {
            
            backgroundObjects[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourfloor");
            backgroundObjects[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourfloor");
            backgroundObjects[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourfloor");
            backgroundObjects[3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourfloor");
            

            GameObject.Find("pc").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourpc open");
            GameObject.Find("read").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colourbook");
        }

        else
        {

            GameObject[] list = GameObject.FindGameObjectsWithTag("trigger");
            for (int i = 0; i < list.Length; i++)
            {
                list[i].GetComponent<SpriteRenderer>().sprite = _sprites[i];
            }

            for (int i = 0; i < backgroundObjects.Length; i++)
            {
                backgroundObjects[i].GetComponent<SpriteRenderer>().sprite = _sprites2[i];
            }

        }

    }

}

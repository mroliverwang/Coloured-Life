using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;





[Serializable]   
public class ActivateTriggerEvent : UnityEvent<GameObject> { }




public class Trigger : MonoBehaviour
{


    private Sprite _newSprite;

    private int _foodAndDrink;

    public TMP_Text _meal;


    public ActivateTriggerEvent m_activateTriggerEvent;
    public ActivateTriggerEvent m_deactivateTriggerEvent;
    public ActivateTriggerEvent m_cancelTriggerEvent;

    private GameObject _player;
    private MainCharacter _character;
    private Animator _animator;

    private GameObject AttributeManager;
    private GameObject TimeSystem;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("man");
        if (_player != null)
        {
            _character = _player.GetComponent<MainCharacter>();
            _animator = _player.GetComponent<Animator>();
        }

        AttributeManager = GameObject.Find("AttributeManager");
        TimeSystem = GameObject.Find("TimeSystem");

        _foodAndDrink = 3;

        _newSprite = Resources.Load<Sprite>("pc open");


    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "man")
        {
            
            Activate(collision.gameObject);
            ListenAction();


        }

    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "man")
        {

            ListenAction();


        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "man")
        {
            
            DeActivate(collision.gameObject);

        }
    }


    private void Activate(GameObject g)
    {
        
        m_activateTriggerEvent.Invoke(g);
    }

    private void DeActivate(GameObject g)
    {
        
        m_deactivateTriggerEvent.Invoke(g);
    }

    private void ActivateCancel(GameObject g)
    {

        m_cancelTriggerEvent.Invoke(g);
    }

    private void ListenAction()
    {

        

        if (_character.GetMoveable())
        {




            if(gameObject.name == "tv")
            {


                if (Input.GetKeyDown(KeyCode.E))
                {

                    
                    


                    _character.SetMoveable(false);

                    DeActivate(gameObject);
                    ActivateCancel(gameObject);


                    TimeSystem.GetComponent<TimeSystem>().SpeedUp(25f);
                    TimeSystem.GetComponent<TimeSystem>().StartTV();
                }


            }

            else if (gameObject.name == "pc")
            {


                if (Input.GetKeyDown(KeyCode.E))
                {
                    _player.transform.position = new Vector3(-4.67f, -0.94f, 0);
                    _player.GetComponent<SpriteRenderer>().flipX = true;

                    _animator.SetBool("sit", true);
                    _character.SetMoveable(false);

                    DeActivate(gameObject);
                    ActivateCancel(gameObject);


                    TimeSystem.GetComponent<TimeSystem>().SpeedUp(30f);
                    TimeSystem.GetComponent<TimeSystem>().StartGame();

                    GetComponent<SpriteRenderer>().sprite = _newSprite;
                    

                }


                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    _player.transform.position = new Vector3(-4.67f, -0.94f, 0);
                    _player.GetComponent<SpriteRenderer>().flipX = true;

                    _animator.SetBool("sit", true);
                    _character.SetMoveable(false);

                    DeActivate(gameObject);
                    ActivateCancel(gameObject);


                    TimeSystem.GetComponent<TimeSystem>().SpeedUp(9f);
                    TimeSystem.GetComponent<TimeSystem>().StartWork();

                    GetComponent<SpriteRenderer>().sprite = _newSprite;

                }


            }

            else if(gameObject.name == "read")
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _player.transform.position = new Vector3(18.14f, -0.94f, 0);
                    _player.GetComponent<SpriteRenderer>().flipX = true;

                    _animator.SetBool("sit", true);
                    _character.SetMoveable(false);

                    DeActivate(gameObject);
                    ActivateCancel(gameObject);

                    TimeSystem.GetComponent<TimeSystem>().SpeedUp(15f);
                    TimeSystem.GetComponent<TimeSystem>().StartRead();
                }



            }

            else if(gameObject.name == "gym")
            {


                if (Input.GetKeyDown(KeyCode.E))
                {

                    _animator.SetBool("workout", true);
                    _character.SetMoveable(false);

                    DeActivate(gameObject);
                    ActivateCancel(gameObject);


                    TimeSystem.GetComponent<TimeSystem>().SpeedUp(20f);
                    TimeSystem.GetComponent<TimeSystem>().StartGym();
                }


            }


            else if(gameObject.name == "water")
            {


                if (_foodAndDrink > 0)
                {

                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        AttributeManager.GetComponent<AttributeManager>().SpendDeposit(5f);
                        AttributeManager.GetComponent<AttributeManager>().DropFatigue(5f);
                        AttributeManager.GetComponent<AttributeManager>().IncreaseHappiness(10f);
                        _foodAndDrink--;

                        if (_meal != null)
                        {
                            _meal.SetText("Meals left: " + _foodAndDrink);
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        AttributeManager.GetComponent<AttributeManager>().SpendDeposit(30f);
                        AttributeManager.GetComponent<AttributeManager>().DropFatigue(10f);
                        AttributeManager.GetComponent<AttributeManager>().IncreaseHappiness(20f);
                        _foodAndDrink--;


                        _meal.SetText("Meals left: " + _foodAndDrink);
                    }
                }

            }


            else if(gameObject.name == "bed")
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _player.GetComponent<BoxCollider2D>().isTrigger = true;
                    //_player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                    _player.transform.position = new Vector3(-21.41f, -2.11f, 0);
                    _player.GetComponent<SpriteRenderer>().flipX = false;
                    




                    _animator.SetBool("sleep", true);


                    _character.SetMoveable(false);

                    DeActivate(gameObject);
                    ActivateCancel(gameObject);


                    TimeSystem.GetComponent<TimeSystem>().SpeedUp(50f);
                    TimeSystem.GetComponent<TimeSystem>().StartNap();
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //go next day
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    TimeSystem.GetComponent<TimeSystem>().GoNextDay();
                }

            }


            else if(gameObject.name == "sofa")
            {

                if (Input.GetKeyDown(KeyCode.E))
                {

                    _player.transform.position = new Vector3(-11.2f, -0.94f, 0);


                    _animator.SetBool("phone", true);
                    _character.SetMoveable(false);

                    DeActivate(gameObject);
                    ActivateCancel(gameObject);


                    TimeSystem.GetComponent<TimeSystem>().SpeedUp(30f);
                    TimeSystem.GetComponent<TimeSystem>().StartPhone();
                }



            }





        }

        if (Input.GetKeyDown(KeyCode.Space) && !_character.GetMoveable())
        {


            _player.transform.position = new Vector3(_player.transform.position.x, -0.94f, 0);
            _player.GetComponent<BoxCollider2D>().isTrigger = false;

            _animator.SetBool("sit", false);
            _animator.SetBool("workout", false);
            _animator.SetBool("phone", false);
            _animator.SetBool("sleep", false);
            _character.SetMoveable(true);

            Activate(gameObject);

            if(gameObject.name == "pc")
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("pc") ;
            }


            TimeSystem.GetComponent<TimeSystem>().ResetTimeSpeed();
            TimeSystem.GetComponent<TimeSystem>().ResetAction();
        }

    }


}

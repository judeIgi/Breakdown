using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player1Wins, Player2Wins, Draw;
    private Key UpKey, RightKey, LeftKey, DownKey, WKey, SKey, AKey, DKey;
    public Transform Player1ScoreLocation, Player2ScoreLocation;
    public GameObject Add50, Remove25;
    public TextMeshProUGUI Player1ScoreText, Player2ScoreText;
    public GameObject IncorrectUp, IncorrectLeft, IncorrectDown, IncorrectRight;
    public GameObject IncorrectW, IncorrectA, IncorrectS, IncorrectD;
    public GameObject ActionKeyW, ActionKeyA, ActionKeyS, ActionKeyD, ActionKeyLeft, ArrowKeyRight, ArrowKeyUp, ArrowKeyDown;
    public GameObject PressKeyW, PressKeyA, PressKeyS, PressKeyD, PressKeyLeft, PressKeyRight, PressKeyUp, PressKeyDown;
    public GameObject ActionSpriteUp, ActionSpriteDown, ActionSpriteLeft, ActionSpriteRight, ActionSpriteW, ActionSpriteD, ActionSpriteS, ActionSpriteA;
    public GameObject ActionSpriteIdleOne, ActionSpriteIdleTwo;
    public int Player1Score, Player2Score;
    public List<Key> KeyList;
    private bool CanPressA , CanPressW, CanPressS, CanPressD;
    private bool CanPressLeft, CanPressUp, CanPressDown, CanPressRight;

    Coroutine PressKeyWCoroutine, PressKeyACoroutine, PressKeySCoroutine, PressKeyDCoroutine ;
 
    Coroutine PressKeyLeftCoroutine, PressKeyUpCoroutine, PressKeyRightCoroutine, PressKeyDownCoroutine ;
    

    bool isPlayer1PressingUp,isPlayer1PressingLeft,isPlayer1PressingRight,isPlayer1PressingDown, isPlayer2PressingW,isPlayer2PressingA,isPlayer2PressingS,isPlayer2PressingD;



    void Start()
    {
        foreach ( Key key in KeyList) key.KeyWasPressedP1 = false;
        foreach ( Key key in KeyList) key.KeyWasPressedP2 = false;

        StartCoroutine(Gameplay());
    }

    // Update is called once per frame
    void Update()
    {
        #region ActionKeys
        if (CanPressA) ActionKeyA.SetActive(true); else ActionKeyA.SetActive(false);
        if (CanPressS) ActionKeyS.SetActive(true); else ActionKeyS.SetActive(false);
        if (CanPressW) ActionKeyW.SetActive(true); else ActionKeyW.SetActive(false);
        if (CanPressD) ActionKeyD.SetActive(true); else ActionKeyD.SetActive(false);

        if (CanPressUp) ArrowKeyUp.SetActive(true); else ArrowKeyUp.SetActive(false);
        if (CanPressLeft) ActionKeyLeft.SetActive(true); else ActionKeyLeft.SetActive(false);
        if (CanPressDown) ArrowKeyDown.SetActive(true); else ArrowKeyDown.SetActive(false);
        if (CanPressRight) ArrowKeyRight.SetActive(true); else ArrowKeyRight.SetActive(false);


        Player1ScoreText.text = "Score: " + Player1Score.ToString();
        Player2ScoreText.text = "Score: " + Player2Score.ToString();


        foreach ( Key key in KeyList)
        {
            if(FindObjectOfType<MusicManager>().Time >= key.timeStamp && FindObjectOfType<MusicManager>().Time <= key.timeStamp + key.TimeToPress)
            {
                if (key.FinalKey)
                {
                    Debug.Log("Final Key is here");
                    StartCoroutine (EndGame());
                }
              if (key.colorToPress == KeyColors.Green && !key.KeyWasPressedP1)
              {
                LeftKey = key;
                CanPressLeft = true;
              }
            if (key.colorToPress == KeyColors.Green && !key.KeyWasPressedP2)
              {
                AKey = key;

                CanPressA = true;
              }
            if (key.colorToPress == KeyColors.Red && !key.KeyWasPressedP1)
              {

                RightKey = key;
                CanPressRight = true;
              }
             if (key.colorToPress == KeyColors.Red && !key.KeyWasPressedP2)
             {
                DKey = key;
                CanPressD = true;
             }


            if (key.colorToPress == KeyColors.Yellow && !key.KeyWasPressedP1)
              {
              DownKey = key;
              CanPressDown = true;
              }
            if (key.colorToPress == KeyColors.Yellow && !key.KeyWasPressedP2)
              {
              SKey = key;
              CanPressS = true;
              }
              if (key.colorToPress == KeyColors.Blue && !key.KeyWasPressedP1)
              {
                UpKey = key;
                CanPressUp = true;
              }
              if (key.colorToPress == KeyColors.Blue && !key.KeyWasPressedP2)
              {
                WKey = key;
                CanPressW = true;
              }              
            //   yield return new WaitForSeconds (key.TimeToPress);
            //   if (key.colorToPress == KeyColors.Green)
            //   {
            //     CanPressA = false;
            //     CanPressLeft = false;
            //   }
            // if (key.colorToPress == KeyColors.Red)
            //   {
            //     CanPressD = false;
            //     CanPressRight = false;
            //   }

            // if (key.colorToPress == KeyColors.Yellow)
            //   {
            //     CanPressS = false;
            //     CanPressDown = false;
            //   }
            //   if (key.colorToPress == KeyColors.Blue)
            //   {
            //     CanPressW = false;
            //     CanPressUp = false;
            //   }
            } 
            else if(FindObjectOfType<MusicManager>().Time > key.timeStamp + key.TimeToPress)
            { 
                key.KeyWasPressedP1 = true;
                key.KeyWasPressedP2 = true;

                if (key.colorToPress == KeyColors.Green )
              {
            
                CanPressLeft = false;
                CanPressA = false;
              }
                if (key.colorToPress == KeyColors.Red )
              {
               
                CanPressRight = false;
                CanPressD = false;
              }              
                if (key.colorToPress == KeyColors.Yellow )
              {
              
                CanPressDown = false;
                CanPressS= false;
              }       
                if (key.colorToPress == KeyColors.Blue )
              {
                CanPressUp = false;
                CanPressW= false;
              }              
       



                // Debug.Log($"Can not press the key, because {key.timeStamp} is not in between {FindObjectOfType<MusicManager>().Time} and {FindObjectOfType<MusicManager>().Time + key.TimeToPress}");
            }

           //  make it so that the keys can turn to false (by making 'pressed' bool in scriptable)
            
        }



        


    #endregion

        #region Idles
        if (!isPlayer1PressingUp && !isPlayer1PressingRight && !isPlayer1PressingLeft && !isPlayer1PressingDown)
        {
            ActionSpriteIdleOne.SetActive(true);
        }
        else
        {
            ActionSpriteIdleOne.SetActive(false);
        }

        if (!isPlayer2PressingW && !isPlayer2PressingD && !isPlayer2PressingA && !isPlayer2PressingS)
        {
            ActionSpriteIdleTwo.SetActive(true);
        }
        else
        {
            ActionSpriteIdleTwo.SetActive(false);
        }
#endregion
        #region Player1
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (PressKeyUpCoroutine!= null)
                StopCoroutine (PressKeyUpCoroutine);

            PressKeyUpCoroutine = StartCoroutine(PressKey_Up());
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (PressKeyLeftCoroutine!= null)
                StopCoroutine (PressKeyLeftCoroutine);

            PressKeyLeftCoroutine = StartCoroutine(PressKey_Left());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (PressKeyDownCoroutine!= null)
                StopCoroutine (PressKeyDownCoroutine);

            PressKeyDownCoroutine = StartCoroutine(PressKey_Down());
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (PressKeyRightCoroutine!= null)
                StopCoroutine (PressKeyRightCoroutine);

            PressKeyRightCoroutine = StartCoroutine(PressKey_Right());
        }
        #endregion
        #region Player2
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (PressKeyWCoroutine!= null)
                StopCoroutine (PressKeyWCoroutine);

            PressKeyWCoroutine = StartCoroutine(PressKey_W());
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (PressKeyACoroutine!= null)
                StopCoroutine (PressKeyACoroutine);

            PressKeyACoroutine = StartCoroutine(PressKey_A());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (PressKeySCoroutine!= null)
                StopCoroutine (PressKeySCoroutine);

            PressKeySCoroutine = StartCoroutine(PressKey_S());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (PressKeyDCoroutine!= null)
                StopCoroutine (PressKeyDCoroutine);

            PressKeyDCoroutine = StartCoroutine(PressKey_D());
        }
        #endregion
    }

 
#region Player1


    IEnumerator PressKey_Up ()
    {
        isPlayer1PressingUp = true ;

        if(CanPressUp)
        {
            PressKeyUp.SetActive(true);
            CanPressUp = false;

            Player1Score += 50;

            if (UpKey != null)LeftKey.KeyWasPressedP1 = true;

            GameObject ScoreEffect = Instantiate(Add50,Player1ScoreLocation);
            Destroy(ScoreEffect,2);         

            // add destroy and instiante for every other key and WINSCREEN TOMMOROW//
        }
        else
        {
            if(Player1Score >= 25)
            {
            IncorrectUp.SetActive(true);
            Player1Score-=25;
            
            if (UpKey != null)LeftKey.KeyWasPressedP1 = true;


            GameObject ScoreEffect = Instantiate(Remove25,Player1ScoreLocation);
            Destroy(ScoreEffect,2);
            }
        }

        

        ActionSpriteUp.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PressKeyUp.SetActive(false);
        IncorrectUp.SetActive(false);
        ActionSpriteUp.SetActive(false);    
        isPlayer1PressingUp = false ;
    }

    IEnumerator PressKey_Left ()
    {
        isPlayer1PressingLeft = true ;

        if(CanPressLeft)
        {
            PressKeyLeft.SetActive(true);
            CanPressLeft = false;

            if (LeftKey != null)LeftKey.KeyWasPressedP1 = true;

            Player1Score += 50;

            GameObject ScoreEffect = Instantiate(Add50,Player1ScoreLocation);
            Destroy(ScoreEffect,2);         

        }
        else
        {
            if(Player1Score >= 25)
            {
            IncorrectLeft.SetActive(true);

            if (LeftKey != null)LeftKey.KeyWasPressedP1 = true;

            Player1Score-=25;


            GameObject ScoreEffect = Instantiate(Remove25,Player1ScoreLocation);
            Destroy(ScoreEffect,2);
            }
        }
        

        
        ActionSpriteLeft.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PressKeyLeft.SetActive(false);
        IncorrectLeft.SetActive(false);
        ActionSpriteLeft.SetActive(false);
        isPlayer1PressingLeft = false ;

    }
    
    IEnumerator PressKey_Down ()
    {
        isPlayer1PressingDown = true ;

        if(CanPressDown)
        {
            PressKeyDown.SetActive(true);
            CanPressDown = false;
            
            if (DownKey != null)DownKey.KeyWasPressedP1 = true;

            Player1Score += 50;

            GameObject ScoreEffect = Instantiate(Add50,Player1ScoreLocation);
            Destroy(ScoreEffect,2);   
        }
        else
        {
             if(Player1Score >= 25)
            {
            IncorrectDown.SetActive(true);
            Player1Score-=25;

            if (DownKey!= null)DownKey.KeyWasPressedP1 = true;

            GameObject ScoreEffect = Instantiate(Remove25,Player1ScoreLocation);
            Destroy(ScoreEffect,2);
            }
        }
        
        ActionSpriteDown.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PressKeyDown.SetActive(false);
        IncorrectDown.SetActive(false);
        ActionSpriteDown.SetActive(false);
        isPlayer1PressingDown = false ;

    }

    IEnumerator PressKey_Right ()
    {
        isPlayer1PressingRight = true ;

         if(CanPressRight)
        {
            PressKeyRight.SetActive(true);
            CanPressRight = false;
            
            Player1Score += 50;

            if (RightKey != null)RightKey.KeyWasPressedP1 = true;

            GameObject ScoreEffect = Instantiate(Add50,Player1ScoreLocation);
            Destroy(ScoreEffect,2);   
        }
        else
        {
            if(Player1Score >= 25)
            {
            IncorrectRight.SetActive(true);
            Player1Score-=25;

            if (RightKey != null)RightKey.KeyWasPressedP1 = true;

            GameObject ScoreEffect = Instantiate(Remove25,Player1ScoreLocation);
            Destroy(ScoreEffect,2);
            }
        }


        ActionSpriteRight.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PressKeyRight.SetActive(false);
        IncorrectRight.SetActive(false);
        ActionSpriteRight.SetActive(false);
        isPlayer1PressingRight = false ;

    }
    #endregion

#region Player2


    IEnumerator PressKey_W ()
    {
        isPlayer2PressingW = true ;

         if(CanPressW)
        {
            PressKeyW.SetActive(true);
            CanPressW = false;
            
           if (WKey != null) WKey.KeyWasPressedP2 = true;

            Player2Score += 50;

            GameObject ScoreEffect = Instantiate(Add50,Player2ScoreLocation);
            Destroy(ScoreEffect,2);
            
        }
        else
        {
           if(Player2Score >= 25)
            {
            IncorrectW.SetActive(true);
            Player2Score-=25;
            
            if (WKey != null) WKey.KeyWasPressedP2 = true;

            GameObject ScoreEffect = Instantiate(Remove25,Player2ScoreLocation);
            Destroy(ScoreEffect,2);
            }
        }


        ActionSpriteW.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PressKeyW.SetActive(false);
        IncorrectW.SetActive(false);
        ActionSpriteW.SetActive(false);
        isPlayer2PressingW = false ;

    }

    IEnumerator PressKey_A ()
    {
        isPlayer2PressingA = true ;

         if(CanPressA)
        {
            PressKeyA.SetActive(true);
            CanPressA = false;

            if (AKey != null)AKey.KeyWasPressedP2 = true;

            
            Player2Score += 50;

            GameObject ScoreEffect = Instantiate(Add50,Player2ScoreLocation);
            Destroy(ScoreEffect,2);
        }
        else
        {
           if(Player2Score >= 25)
            {
            IncorrectA.SetActive(true);
            Player2Score-=25;

            if (AKey != null)AKey.KeyWasPressedP2 = true;


            GameObject ScoreEffect = Instantiate(Remove25,Player2ScoreLocation);
            Destroy(ScoreEffect,2);
            }
        }

        ActionSpriteA.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PressKeyA.SetActive(false);
        IncorrectA.SetActive(false);
        ActionSpriteA.SetActive(false);
        isPlayer2PressingA = false ;


    }
    
    IEnumerator PressKey_S ()
    {
        isPlayer2PressingS = true ;

         if(CanPressS)
        {
            PressKeyS.SetActive(true);
            CanPressS = false;
            
            Player2Score += 50;

            if (SKey != null)SKey.KeyWasPressedP2 = true;


            GameObject ScoreEffect = Instantiate(Add50,Player2ScoreLocation);
            Destroy(ScoreEffect,2);
        }
        else
        {
            if(Player2Score >= 25)
            {
            IncorrectS.SetActive(true);
            Player2Score-=25;

            if (SKey != null)  SKey.KeyWasPressedP2 = true;


            GameObject ScoreEffect = Instantiate(Remove25,Player2ScoreLocation);
            Destroy(ScoreEffect,2);
            }
            
        }


        ActionSpriteS.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PressKeyS.SetActive(false);
        IncorrectS.SetActive(false);
        ActionSpriteS.SetActive(false);
        isPlayer2PressingS = false;
    }

    IEnumerator PressKey_D ()
    {
        isPlayer2PressingD = true ;

         if(CanPressD)
        {
            PressKeyD.SetActive(true);
            CanPressD = false;
            
            Player2Score += 50;

             if (DKey != null) DKey.KeyWasPressedP2 = true;


            GameObject ScoreEffect = Instantiate(Add50,Player2ScoreLocation);
            Destroy(ScoreEffect,2);
        }
        else
        {
            if(Player2Score >= 25)
            {
            IncorrectD.SetActive(true);
            Player2Score-=25;

            if (DKey != null)   DKey.KeyWasPressedP2 = true;


            GameObject ScoreEffect = Instantiate(Remove25,Player2ScoreLocation);
            Destroy(ScoreEffect,2);
            }

        }


        ActionSpriteD.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PressKeyD.SetActive(false);
        IncorrectD.SetActive(false);
        ActionSpriteD.SetActive(false);
        isPlayer2PressingD = false ;
    }
    #endregion

    IEnumerator Gameplay(){
        yield return  new WaitForSeconds (5);
        foreach ( Key key in KeyList)
        {
            if(FindObjectOfType<MusicManager>().Time >= key.timeStamp && FindObjectOfType<MusicManager>().Time <= key.timeStamp + key.TimeToPress)
            {
              if (key.colorToPress == KeyColors.Green)
              {
                CanPressA = true;
                CanPressLeft = true;
              }
                if (key.colorToPress == KeyColors.Red)
                {
                    CanPressD = true;
                    CanPressRight = true;
                if (key.FinalKey)
                {
                    Debug.Log("Final Key is here");
                    StartCoroutine (EndGame());
                }
                else
                {
                    Debug.Log("Not Final Key");
                }
                }

                if (key.colorToPress == KeyColors.Yellow)
                {
                    CanPressS = true;
                    CanPressDown = true;
                }
                if (key.colorToPress == KeyColors.Blue)
                {
                    CanPressW = true;
                    CanPressUp = true;
                }
                yield return new WaitForSeconds (key.TimeToPress);
                if (key.colorToPress == KeyColors.Green)
                {
                    CanPressA = false;
                    CanPressLeft = false;
                }
                if (key.colorToPress == KeyColors.Red)
                {
                    CanPressD = false;
                    CanPressRight = false;
                }

                if (key.colorToPress == KeyColors.Yellow)
                {
                    CanPressS = false;
                    CanPressDown = false;
                }
                if (key.colorToPress == KeyColors.Blue)
                {
                    CanPressW = false;
                    CanPressUp = false;
                }

            } 
            else
            
            {
            }

           //  make it so that the keys can turn to false (by making 'pressed' bool in scriptable)
            
        }
    }
    IEnumerator EndGame(){
        yield return new WaitForSeconds (3);

        if (Player1Score > Player2Score)
        {
            Player1Wins.SetActive(true);
            Debug.Log("Player 1 wins");
        }
        else if (Player2Score > Player1Score)
        {
            Player2Wins.SetActive(true);
            Debug.Log("Player 2 wins");
        }
        else 
        {
            Draw.SetActive(true);
            Debug.Log("Draw");
        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Home Page");
    }

}


public enum KeyColors
{
    Green, Yellow ,Blue, Red
}


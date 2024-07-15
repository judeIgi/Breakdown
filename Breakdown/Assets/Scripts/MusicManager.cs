using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MusicManager : MonoBehaviour

{
    public  float Time;
    public AudioSource MusicPlayer;
    public AudioClip WinSound;
    public AudioClip PerfectSFX;
    public AudioClip IncorrectSFX;
    public AudioClip stage1;
    public AudioClip CountdownTick;
    public AudioClip FinalTick;
    public TextMeshProUGUI CountdownText;
    public void StartGame()
    {
        MusicPlayer.Play();

        // PlayerPrefs.SetInt("highscore", player1Score);
        // PlayerPrefs.Save();
    }
    IEnumerator Countdown()
    {
         MusicPlayer.PlayOneShot(CountdownTick);
        CountdownText.text = "5";
        yield return new WaitForSeconds(1);
        MusicPlayer.PlayOneShot(CountdownTick);
        CountdownText.text = "4";
        yield return new WaitForSeconds(1);
        MusicPlayer.PlayOneShot(CountdownTick);
        CountdownText.text = "3";
        yield return new WaitForSeconds(1);
        MusicPlayer.PlayOneShot(CountdownTick);
        CountdownText.text = "2";
        yield return new WaitForSeconds(1);
        MusicPlayer.PlayOneShot(CountdownTick);
        CountdownText.text = "1";
        yield return new WaitForSeconds(1);
        MusicPlayer.PlayOneShot(FinalTick);
        CountdownText.text = "Breakdown!";

        StartGame();
        yield return new WaitForSeconds(1);
        CountdownText.text = "";

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
        

    }

    void Update()
    {
        // Debug.Log(Time);
        Time = GetComponent<AudioSource>().time;
    }


}

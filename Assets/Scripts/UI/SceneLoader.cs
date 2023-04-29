using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static int levelCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //load game scene
    public void startGame(){        
        SceneManager.LoadScene(1); 
    }

    //load main menu scene
    public void endGame(){
        SceneManager.LoadScene(0); 
    }

    //change scenes when leveling up
    public void levelSecond(){
        SceneManager.LoadScene(2); 
    }

    //change scenes when leveling up
    public void levelThird(){    
        SceneManager.LoadScene(3); 
    }
}

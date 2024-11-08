using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonX : MonoBehaviour
{
    //reference to the Button component attached to this game object
    private Button button;
    //reference to the GameManagerX component to communicate with game management
    private GameManagerX gameManagerX;
    //integer to store the difficulty level associated with this button (1, 2, or 3)
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        //find the GameManager object in the scene and get its GameManagerX component
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();
        //get the Button component attached to this game object
        button = GetComponent<Button>();
        //add a listener to the button that will call the SetDifficulty() method when clicked
        button.onClick.AddListener(SetDifficulty);
    }

    /* When a button is clicked, call the StartGame() method
     * and pass it the difficulty value (1, 2, 3) from the button 
    */
    void SetDifficulty()
    {
        //log the name of the button that was clicked
        Debug.Log(button.gameObject.name + " was clicked");
        //call the StartGame method in the GameManagerX, passing the selected difficulty level
        gameManagerX.StartGame(difficulty);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetX : MonoBehaviour

    {
        // Rigidbody component for potential physics interactions
        private Rigidbody rb;

        // Reference to the GameManagerX to update score and handle game state
        private GameManagerX gameManagerX;

        // The point value awarded when this target is clicked
        public int pointValue;

        // Explosion effect to display when the target is destroyed
        public GameObject explosionFx;

        // The amount of time the target stays on screen before moving off
        public float timeOnScreen = 1.0f;

        // Variables for calculating random spawn positions on the grid
        private float minValueX = -3.75f; // X-coordinate of the left-most square
        private float minValueY = -3.75f; // Y-coordinate of the bottom-most square
        private float spaceBetweenSquares = 2.5f; // Distance between centers of squares on the board

        // Start is called before the first frame update
        void Start()
        {
            // Get the Rigidbody component attached to this object
            rb = GetComponent<Rigidbody>();

            // Find the Game Manager object and get its GameManagerX component
            gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();

            // Set the target's initial position at a random spot on the grid
            transform.position = RandomSpawnPosition();

            // Start a coroutine to remove the target after a set time
            StartCoroutine(RemoveObjectRoutine());
        }

        // This method is called when the target is clicked by the player
        private void OnMouseDown()
        {
            // If the game is active, destroy the target, update the score, and trigger an explosion
            if (gameManagerX.isGameActive)
            {
                Destroy(gameObject); // Destroy the target object
                gameManagerX.UpdateScore(pointValue); // Add the target's point value to the score
                Explode(); // Show explosion effect
            }
        }

        // Generate a random spawn position on the grid based on random X and Y indices
        Vector3 RandomSpawnPosition()
        {
            // Calculate the X and Y positions based on random indices
            float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
            float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

            // Return the calculated position as a Vector3
            return new Vector3(spawnPosX, spawnPosY, 0);
        }

        // Generate a random index (0 to 3) to determine which square in the grid the target will spawn in
        int RandomSquareIndex()
        {
            return Random.Range(0, 4);
        }

        // If the target collides with a sensor and is not a "bad" object, trigger game over
        private void OnTriggerEnter(Collider other)
        {
            // Destroy the target when it collides with another object
            Destroy(gameObject);

            // Check if the target collided with a "Sensor" object and is not tagged as "Bad"
            if (other.gameObject.CompareTag("Sensor") && !gameObject.CompareTag("Bad"))
            {
                // Trigger game over if the conditions are met
                gameManagerX.GameOver();
            }
        }

        // Instantiate an explosion effect at the target's current position
        void Explode()
        {
            Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
        }

        // Coroutine to move the target off the screen after a delay, simulating it leaving the game area
        IEnumerator RemoveObjectRoutine()
        {
            // Wait for the specified time before moving the object
            yield return new WaitForSeconds(timeOnScreen);

            // If the game is still active, move the target forward, making it collide with the sensor
            if (gameManagerX.isGameActive)
            {
                transform.Translate(Vector3.forward * 5, Space.World);
            }
        }
    }

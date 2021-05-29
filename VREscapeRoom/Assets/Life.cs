using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    private int life = 5;
    // Start is called before the first frame update
    
    public void Hurt()
    {
        life--;
        // hurt sound
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            //Gameover
            // dead sound
        }        
    }
}

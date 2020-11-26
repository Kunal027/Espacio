using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_behav_ast : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 4.0f);
        
    }
}

using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoGameObject : MonoBehaviour
{
    public Interface inter;
    // Start is called before the first frame update
    void Start()
    {
        inter = new Interface();
        Debug.Log("Initialized Arduino Interface");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void write_movement(string cmd, int pos)
    {
        Interface.Movement move_type;
        switch (cmd)
        {
            case "pan":
                move_type = Interface.Movement.Pan;
                break;

            case "tilt":
                move_type = Interface.Movement.Tilt;
                break;

            case "step":
                move_type = Interface.Movement.Step;
                break;

            default:
                return;
        }
        inter.write_movement(pos, move_type);
    }
}

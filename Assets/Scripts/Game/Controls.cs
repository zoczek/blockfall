using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public enum KeyState
    {
        just_pressed,
        pressed,
        just_released,
        released,
    }


    public Dictionary<string, KeyState> actionStates = new Dictionary<string, KeyState>();

    private Dictionary<KeyCode, string> _keysToActions = new Dictionary<KeyCode, string>();

    private void Awake()
    {
        foreach (var action in DefaultControls.keys)
        {
            actionStates.Add(action.Key, KeyState.released);
        }
        
        foreach (var action in actionStates.Keys)
        {
            if (PlayerPrefs.HasKey(action))
                _keysToActions.Add((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(action)), action);
            else
                _keysToActions.Add(DefaultControls.keys[action], action);
        }
    }

    private void Update()
    {
        foreach (var key in _keysToActions)
        {
            KeyState previousState = actionStates[key.Value];

            if (Input.GetKey(key.Key))
            {
                if (previousState == KeyState.just_released || previousState == KeyState.released)
                    actionStates[key.Value] = KeyState.just_pressed;
                else
                    actionStates[key.Value] = KeyState.pressed;
            }
            else
            {
                if (previousState == KeyState.just_pressed || previousState == KeyState.pressed)
                    actionStates[key.Value] = KeyState.just_released;
                else
                    actionStates[key.Value] = KeyState.released;
            }
        }
    }


}

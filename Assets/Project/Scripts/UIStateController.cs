using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIStateController : MonoBehaviour
{
    [SerializeField] private List<UIState> uIStates = new();

    void OnEnable()
    {
        foreach (var k in uIStates)
        {
            k.Activated += OnActivated;
        }
    }
    void OnDisable()
    {
        foreach (var k in uIStates)
        {
            k.Activated -= OnActivated;
        }
    }
    private void OnActivated(UIState state)
    {
        uIStates.Where(a => a != state && !state.NearStates.Contains(a)).ToList().ForEach(a => a.Deactivate());
    }
}

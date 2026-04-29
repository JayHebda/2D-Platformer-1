using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinisher : SceneOpener
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        OpenScene();
    }
}

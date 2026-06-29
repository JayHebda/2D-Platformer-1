using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinisher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzamy, czy obiekt, który wszed³ w strefê, to na pewno gracz
        if (other.CompareTag("Player"))
        {
            // Pobieramy indeks obecnej sceny i dodajemy do niego 1
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // Sprawdzamy, czy nastêpna scena to nie jest ekran œmierci (który jest na samym koñcu listy)
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                // Jeœli to by³ ostatni poziom (Level 5), wracamy do Menu G³ównego (indeks 0)
                SceneManager.LoadScene(0);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentencesManager : MonoBehaviour
{
    public static SentencesManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private string[] correctSentences = {
        "She is happy.",
        "They are tired.",
        "We were excited.",
        "He has a book.",
        "I am hungry.",
        "It was raining outside.",
        "You have a beautiful smile.",
        "The dog is barking.",
        "The flowers are blooming.",
        "The sky is blue.",
        "The sun is shining.",
        "The music is loud.",
        "The food is delicious.",
        "The water is cold.",
        "The beach is crowded.",
        "The mountain is beautiful.",
        "The city is noisy.",
        "The park is peaceful.",
        "The car is fast.",
        "The train is late.",
        "The airplane is flying.",
        "The boat is sailing.",
        "The bird is singing.",
        "The cat is sleeping.",
        "The fish is swimming.",
        "The tree is tall.",
        "The building is old.",
        "The road is busy.",
        "The bridge is long."
    };

    private string[] incorrectSentences = {
        "She have happy.",
        "They am tired.",
        "We was exciting.",
        "He have a books.",
        "I are hungry.",
        "It were rain outside.",
        "You has a beautiful smile.",
        "The dogs is barking.",
        "The flower are blooming.",
        "The skies is blue.",
        "The suns is shining.",
        "The musics is loud.",
        "The foods is delicious.",
        "The waters is cold.",
        "The beachs is crowded.",
        "The mountains is beautiful.",
        "The cities is noisy.",
        "The parks is peaceful.",
        "The cars is fast.",
        "The trains is late.",
        "The airplanes is flying.",
        "The boats is sailing.",
        "The birds is singing.",
        "The cats is sleeping.",
        "The fishs is swimming.",
        "The trees is tall.",
        "The buildings is old.",
        "The roads is busy.",
        "The bridges is long."
    };

    public string generateCorrectSentence()
    {
        return correctSentences[Random.Range(0, correctSentences.Length)];
    }

    public string generateIncorrectSentence()
    {
        return incorrectSentences[Random.Range(0, incorrectSentences.Length)];
    }
}

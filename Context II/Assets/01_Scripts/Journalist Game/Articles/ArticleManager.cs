using MarcoHelpers;
using System.Collections.Generic;
using UnityEngine;

public class ArticleManager : MonoBehaviour
{
    public List<sArticle> articles = new List<sArticle>();
    private sArticle currentArticle;
    private int currentIndex;

    private void Start()
    {
        if (articles.Count > currentIndex)
        {
            currentArticle = articles[currentIndex];
        }

        DisplayCurrentArticle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) PreviousArticle();
        if (Input.GetKeyDown(KeyCode.RightArrow)) NextArticle();
    }

    public void NextArticle()
    {
        currentIndex = (currentIndex + 1 + articles.Count) % articles.Count;
        DisplayCurrentArticle();
    }

    public void PreviousArticle()
    {
        currentIndex = (currentIndex - 1 + articles.Count) % articles.Count;
        DisplayCurrentArticle();
    }

    public void DisplayCurrentArticle()
    {
        currentArticle= articles[currentIndex];
        EventSystem.RaiseEvent(EventName.ARTICLE_CHANGE, currentArticle);
    }
}

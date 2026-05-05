using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HP_Enemy : MonoBehaviour
{
    public Transform target;
    public FSM fsm;
    public Vector3 offset;

    public float percentage;
    public float speed = .003f;
    public float fadeSpeed = 3f;

    public Image HealthBar;
    public Image backGround;

    Color healthyColor = Color.green;
    Color midColor = Color.yellow;
    Color lowColor = Color.red;

    private Coroutine currentFadeCoroutine;

    void Update()
    {
        Follow();
        ChangeHP();
        ChangeColor();
    }

    public void Follow()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }

    public void ChangeHP()
    {
        percentage = (float)fsm.currentLife / (float)fsm.maxLife;
        if (HealthBar.fillAmount > percentage)
        {
            HealthBar.fillAmount -= speed;
        }
        if (HealthBar.fillAmount < percentage)
        {
            HealthBar.fillAmount = percentage;
        }

    }

    public void ChangeColor()
    {
        if (HealthBar.fillAmount < .34f)
        {
            HealthBar.color = lowColor;
            return;
        }
        if (HealthBar.fillAmount < .67)
        {
            HealthBar.color = midColor;
            return;
        }
        HealthBar.color = healthyColor;
    }
    public void ShowHealthBar()
    {
        if (currentFadeCoroutine != null)
            StopCoroutine(currentFadeCoroutine);
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        CanvasGroup back = backGround.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        back.alpha = 1;

        if (gameObject.activeInHierarchy)  
            HideHealthBar();
    }

    public void HideHealthBar()
    {
        if (!gameObject.activeInHierarchy)  
            return;
        currentFadeCoroutine = StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.5f);
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        CanvasGroup back = backGround.GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            back.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 0;
        back.alpha = 0;
    }
}

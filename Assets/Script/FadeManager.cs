using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeManager : MonoBehaviour {

	public static FadeManager instance;  

	public void Awake()  
	{  
		FadeManager.instance = this;  
	}  

	public void FlashOut(GameObject gameObject)
	{
		SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
		StartCoroutine(RendererFlashOut(spriteRenderer));
	}

	IEnumerator RendererFlashOut (SpriteRenderer spriteRenderer) {
		Color originalColor = new Color();

		originalColor = spriteRenderer.color;
		spriteRenderer.color = new Color (255, 0, 0, 1);

		while(true) {
			if (!spriteRenderer)
				break;
			spriteRenderer.color = Color.Lerp(spriteRenderer.color, originalColor, 5 * Time.deltaTime);
			if (Mathf.Abs (spriteRenderer.color.g - originalColor.g) < 0.01f)
				break;
			yield return null;
		}
	}

	public void CanvasFaidOut(GameObject gameObject)
	{
		Image[] images = gameObject.GetComponentsInChildren<Image>();
		StartCoroutine(CanvasFadeOut(images));

		Text[] texts = gameObject.GetComponentsInChildren<Text>();
		StartCoroutine(CanvasFadeOut(texts));
	}

	IEnumerator CanvasFadeOut(Image[] images)
	{
		for (float f = 1f; f > 0f; f -= 0.2f)
		{
			foreach (Image image in images)
			{
				Color c = image.color;
				c.a = f;
				image.color = c;
			}
			yield return null;
		}

		foreach (Image image in images)
		{
			Color c = image.color;
			c.a = 0;
			image.color = c;
			if(!image.CompareTag("NoActiveFalse"))
				image.gameObject.SetActive (false);
		}
	}

	IEnumerator CanvasFadeOut(Text[] texts)
	{
		for (float f = 1f; f > 0f; f -= 0.2f)
		{
			foreach (Text text in texts)
			{
				Color c = text.color;
				c.a = f;
				text.color = c;
			}
			yield return null;
		}

		foreach (Text text in texts)
		{
			Color c = text.color;
			c.a = 0;
			text.color = c;
			text.gameObject.SetActive (false);
		}
	}

	public void ImageFaidIn(GameObject image)
	{
		Image c = image.GetComponent<Image>();
		StartCoroutine(RendererImageFadeIn(c));
	}

	IEnumerator RendererImageFadeIn(Image img)
	{
		for (float f = 1f; f > 0f; f -= 0.2f)
		{
			Color c1 = img.color;
			c1.a = 1 - f;
			img.color = c1;

			yield return null;
		}

		Color c = img.color;
		c.a = 1;
		img.color = c;
	}

	public void ImageFaidOut(GameObject image)
	{
		Image c = image.GetComponent<Image>();
		StartCoroutine(RendererImageFadeOut(c));
	}

	IEnumerator RendererImageFadeOut(Image img)
	{
		for (float f = 1f; f > 0f; f -= 0.2f)
		{
			Color c1 = img.color;
			c1.a = f;
			img.color = c1;

			yield return null;
		}

		Color c = img.color;
		c.a = 0;
		img.color = c;
	}

	public void CanvasFaidIn(GameObject gameObject)
	{
		Image[] images = gameObject.GetComponentsInChildren<Image>();
		StartCoroutine(CanvasFaidIn(images));
		Text[] texts = gameObject.GetComponentsInChildren<Text>();
		StartCoroutine(CanvasFaidIn(texts));
	}

	IEnumerator CanvasFaidIn(Image[] images)
	{
		for (float f = 1f; f > 0f; f -= 0.2f)
		{
			foreach (Image image in images)
			{
				Color c = image.color;
				c.a = 1 - f;
				image.color = c;
			}
			yield return null;
		}

		foreach (Image image in images)
		{
			Color c = image.color;
			c.a = 1;
			image.color = c;
		}
	}

	IEnumerator CanvasFaidIn(Text[] texts)
	{
		for (float f = 1f; f > 0f; f -= 0.2f)
		{
			foreach (Text text in texts)
			{
				Color c = text.color;
				c.a = 1 - f;
				text.color = c;
			}
			yield return null;
		}

		foreach (Text text in texts)
		{
				Color c = text.color;
				c.a = 1;
				text.color = c;
		}
	}

	public void TextFaidIn(GameObject image)
	{
		Text c = image.GetComponent<Text>();
		StartCoroutine(TextFadeIn(c));
	}

	IEnumerator TextFadeIn(Text img)
	{
		for (float f = 1f; f > 0f; f -= 0.2f)
		{
			Color c1 = img.color;
			c1.a = 1 - f;
			img.color = c1;

			yield return null;
		}

		Color c = img.color;
		c.a = 1;
		img.color = c;
	}

	public void TextFaidOut(GameObject image)
	{
		Text c = image.GetComponent<Text>();
		StartCoroutine(TextFadeOut(c));
	}

	IEnumerator TextFadeOut(Text img)
	{
		for (float f = 1f; f > 0f; f -= 0.2f)
		{
			Color c1 = img.color;
			c1.a = f;
			img.color = c1;

			yield return null;
		}

		Color c = img.color;
		c.a = 0;
		img.color = c;
	}

	public void SpriteFaidIn(GameObject image)
	{
		SpriteRenderer s = image.GetComponent<SpriteRenderer>();
		StartCoroutine(SpriteFaidIn(s));
	}

	IEnumerator SpriteFaidIn(SpriteRenderer img)
	{
		for (float f = 1f; f > 0f; f -= 0.2f)
		{
			Color c1 = img.color;
			c1.a = 1 - f;
			img.color = c1;

			yield return null;
		}

		Color c = img.color;
		c.a = 1;
		img.color = c;
	}
	public void SpriteFaidOut(GameObject image)
	{
		SpriteRenderer s = image.GetComponent<SpriteRenderer>();
		StartCoroutine(SpriteFaidOut(s));
	}

	IEnumerator SpriteFaidOut(SpriteRenderer img)
	{
		for (float f = 1f; f > 0f; f -= 0.2f)
		{
			Color c1 = img.color;
			c1.a = f;
			img.color = c1;

			yield return null;
		}

		Color c = img.color;
		c.a = 0;
		img.color = c;
	}
}

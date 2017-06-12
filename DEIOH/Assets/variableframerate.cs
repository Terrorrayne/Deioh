using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class variableframerate : MonoBehaviour
{

	public Camera cam;
	public RenderTexture rendTex;
	public Material mat;

	public AnimationCurve extraScroll;

	public Shader accumShader;
	Material _accumMat;
	RenderTexture accumTex;
	[Range(0.0f, 0.92f)]
	public float blurAmount = 0.3f;

	float wobble = 20;
	float stretch = 2.5f;

	float startTime;

	protected Material material
	{
		get
		{
			if (_accumMat == null)
			{
				_accumMat = new Material(accumShader);
				_accumMat.hideFlags = HideFlags.HideAndDontSave;
			}
			return _accumMat;
		}
	}


	protected virtual void OnDisable()
	{
		if (_accumMat)
		{
			DestroyImmediate(_accumMat);
		}
	}

	void Start()
	{
		//rendTex.width = (int)(Screen.width / 1.5);
		//rendTex.height = (int)(Screen.height / 1.35f);

		startTime = Time.time;

		rendTex.height = 360;
		rendTex.width = (int)(((Screen.width * 480) / Screen.height) / 1.5f);

		StartCoroutine(DelayedRendering());
	}

	public IEnumerator DelayedRendering()
	{
		while (true)
		{
			cam.Render();
			yield return new WaitForSeconds(1 / 3f);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		//cam.Render();
		float time = Time.time - startTime;
		float rate = (time * 3) % 1;
		rate *= rate * 1.1f;
		rate = Mathf.Cos(rate);
		wobble = Mathf.Lerp(wobble, Random.value, 0.01f);
		stretch = Mathf.Lerp(stretch, Random.Range(0.95f, 1.00f), 0.02f);
		//mat.SetFloat("_Scroll", rate * 0.03f + wobble * 0.1f - 0.05f);
		mat.SetFloat("_Scroll", extraScroll.Evaluate(time) + rate * 0.015f);

		mat.SetFloat("_Stretching", stretch - rate * 0.015f);
		mat.SetFloat("_GammaCorrection", 0.8f + 0.15f + rate * -0.15f);


		// frame accum

		//if (accumTex == null || accumTex.width != source.width || accumTex.height != source.height)
		//{
		//	DestroyImmediate(accumTex);
		//	accumTex = new RenderTexture(source.width, source.height, 0);
		//	accumTex.hideFlags = HideFlags.HideAndDontSave;
		//	Graphics.Blit(source, accumTex);
		//}

		//// Clamp the motion blur variable, so it can never leave permanent trails in the image
		//blurAmount = Mathf.Clamp(blurAmount, 0.0f, 0.92f);

		//// Setup the texture and floating point values in the shader
		//material.SetTexture("_MainTex", accumTex);
		//material.SetFloat("_AccumOrig", 1.0F - blurAmount);

		//// We are accumulating motion over frames without clear/discard
		//// by design, so silence any performance warnings from Unity
		//accumTex.MarkRestoreExpected();

		// warping + chromabb 

		Graphics.Blit(rendTex, null as RenderTexture, mat);
		//Graphics.Blit(source, accumTex, material);
		//Graphics.Blit(accumTex, destination);
	}
}

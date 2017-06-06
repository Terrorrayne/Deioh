using UnityEngine;

[ExecuteInEditMode]
public class ApplyRenderTexture : MonoBehaviour
{
	public RenderTexture rendTex;
	public Camera pipescam;
	public Shader blendShader;
	public float intensity = 0.5f;

	Material blendMat;

	// Use this for initialization
	void Start()
	{
		pipescam.targetTexture = rendTex;
		blendMat = new Material(blendShader);

	}

	// Update is called once per frame
	void LateUpdate()
	{
		// gotta clear the rend tex. then get it,
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{

		rendTex = RenderTexture.GetTemporary(Screen.width, Screen.height);
		pipescam.targetTexture = rendTex;


		pipescam.Render();



		Vector4 UV_Transform = new Vector4(1, 0, 0, 1);

		//Graphics.Blit(rendTex, source);
		blendMat.SetVector("_UV_Transform", UV_Transform);
		blendMat.SetFloat("_Intensity", intensity);
		blendMat.SetTexture("_Overlay", rendTex);

		Graphics.Blit(source, destination, blendMat, 4);
		RenderTexture.ReleaseTemporary(rendTex);
	}
}

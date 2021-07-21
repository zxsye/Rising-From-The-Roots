// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;


// public class BCFC : MonoBehaviour
// {
//     public static BCFC instance;

//     public LAYER background = new LAYER();
//     public LAYER cinematic = new LAYER();
//     public LAYER foreground = new LAYER();


//     public Awake() {
//         instance = this;
//     }

//     [System.Serializable]
//     public class LAYER {
//         public GameObject root; // root of parent
//         public GameObject newImageObjectReference;
//         public RawImage activeImage;
//         public List<RawImage> allImages = new List<RawImage>();

//         public void SetTexture(Texture texture) {
//             if (activeImage != null && activeImage.texture != null)
//             {
//                 MovieTexture mov = texture as MovieTexture;
                
//                 if (move != null)
//                     mov.Stop();
//             }

//             if (texture != null) {
//                 if (activeImage == null)
//                     CreateNewActiveImage();

//                 activeImage.texture = texture;
//                 activeImage.color =GlobalF.SetAlpha(activeImage.color, 1f);

//                 // MovieTexture mov = texture as MovieTexture;
//                 // if (move != null) {
//                 //     mov.audioClip;
//                 //     mov.loop = true;
//                 //     mov.Play();
//                 // }



//             } else {
//                 if (activeImage != null) {
//                     allImagesImages.Remove(activeImage);
//                     GameObject.DestroyImmediate(activeImage.gameObject);
//                     activeImage = null;
//                 }
//             }
//         }

//         void CreateNewActiveImage() {
//             GameObject ob = Instantiate(newImageObjectReference, root.transform) as GameObject;
//             ob.SetActive(true);
//             RawImage raw = ob.GetCOmponent<RawImage>();
//             activeImage = raw;
//             allImagesImages.Add(raw);
//         }
//     }



//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BCFC : MonoBehaviour
{
	public static BCFC instance;

	public LAYER background = new LAYER();
	public LAYER cinematic = new LAYER();
	public LAYER foreground = new LAYER();

	void Awake()
	{
		instance = this;
	}

	[System.Serializable]
	public class LAYER
	{
		public GameObject root;
		public GameObject newImageObjectReference;
		public RawImage activeImage;
		public List<RawImage> allImages = new List<RawImage>();

		public void SetTexture(Texture texture, bool ifMovieThenLoop = true)
		{
			// if (activeImage != null && activeImage.texture != null)
			// {
			// 	MovieTexture mov = texture as MovieTexture;
			// 	if (mov != null)
			// 		mov.Stop();
			// }

			if (texture != null)
			{
				if (activeImage == null)
					CreateNewActiveImage();

				activeImage.texture = texture;
				activeImage.color = GlobalF.SetAlpha(activeImage.color, 1f);

				// MovieTexture mov = texture as MovieTexture;
				// if (mov != null)
				// {
				// 	mov.loop = ifMovieThenLoop;
				// 	mov.Play();
				// }
			}
			else
			{
				if (activeImage != null)
				{
					allImages.Remove(activeImage);
					GameObject.DestroyImmediate(activeImage.gameObject);
					activeImage = null;
				}
			}
		}

		public void TransitionToTexture(Texture texture, float speed = 2f, bool smooth = false, bool ifMovieThenLoop = true)
		{
			if (activeImage != null && activeImage.texture == texture)
				return;

			StopTransitioning();
			transitioning = BCFC.instance.StartCoroutine(Transitioning(texture, speed, smooth, ifMovieThenLoop));
		}


		void StopTransitioning()
		{
			if(isTransitioning)
				BCFC.instance.StopCoroutine(transitioning);

			transitioning = null;
		}

		public bool isTransitioning {get{return transitioning != null;}}
		Coroutine transitioning = null;
		IEnumerator Transitioning(Texture texture, float speed, bool smooth, bool ifMovieThenLoop)
		{
			if (texture != null)
			{
				for (int i = 0; i < allImages.Count; i++) 
				{
					RawImage image = allImages [i];
					if (image.texture == texture) 
					{
						activeImage = image;
						break;
					}
				}

				if (activeImage == null || activeImage.texture != texture)
				{
					CreateNewActiveImage();
					activeImage.texture = texture;
					activeImage.color = GlobalF.SetAlpha(activeImage.color, 0f);

					// MovieTexture mov = texture as MovieTexture;
					// if (mov != null)
					// {
					// 	mov.loop = ifMovieThenLoop;
					// 	mov.Play();
					// }
				}
			}
			else
				activeImage = null;

			while(GlobalF.TransitionRawImages(ref activeImage, ref allImages, speed, smooth))
				yield return new WaitForEndOfFrame();

			StopTransitioning();
		}

		void CreateNewActiveImage()
		{
			GameObject ob = Instantiate(newImageObjectReference, root.transform) as GameObject;
			ob.SetActive(true);
			RawImage raw = ob.GetComponent<RawImage>();
			activeImage = raw;
			allImages.Add(raw);
		}
	}
}

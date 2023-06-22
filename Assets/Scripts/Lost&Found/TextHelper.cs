using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LostAndFound
{
    public class TextHelper : MonoBehaviour
    {
        private static TextHelper instance;
        public static TextHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject("TextHelper").AddComponent<TextHelper>();
                    DontDestroyOnLoad(instance);
                }
                return instance;
            }
        }

        public GameObject LightTorch;
        public GameObject PortalText;
        // Start is called before the first frame update
        void Start()
        {
            GameObject canvas = GameObject.Find("Canvas");
            LightTorch = canvas.transform.Find("LightTorch").gameObject;
            PortalText = canvas.transform.Find("PortalText").gameObject;
            LightTorch.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void DislayTorchStat(string trochStat, float heat)
        {
            GameObject canvas = GameObject.Find("Canvas");
            Text stat = canvas.transform.Find("TorchStat").gameObject.GetComponent<Text>();
            stat.text = trochStat;
            if (heat > 0.3f)          
                stat.color = new Color(heat, 0, 0, 1);
            Debug.Log(stat.color);
        }

        public void DisplayWin(string content)
        {
            GameObject canvas = GameObject.Find("Canvas");
            var winObj = canvas.transform.Find("Win").gameObject;
            winObj.SetActive(true);
            Text win = winObj.GetComponent<Text>();
            win.text = content;
            StartCoroutine(SimpleLerp(win, 0f, 1f));
            StartCoroutine(DiplaRollWin());
            StartCoroutine(DiplayProducer());
        }

        public IEnumerator DiplayProducer()
        {
            yield return new WaitForSeconds(3.0f);
            GameObject canvas = GameObject.Find("Canvas");
            var producer = canvas.transform.Find("Producer").gameObject;
            producer.SetActive(true);

            float duration = 30.0f; //0.5 secs
            float currentTime = 0f;
            //
            while(currentTime < duration)
            {
                //float alpha = Mathf.Lerp(from, to, currentTime/duration);
                float y = producer.transform.localPosition.y + 0.01f * Time.deltaTime;
                producer.transform.localPosition = new Vector3(producer.transform.localPosition.x, y, producer.transform.localPosition.z);
                currentTime += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }

        public IEnumerator DiplaRollWin()
        {
            yield return new WaitForSeconds(10.0f);
            GameObject canvas = GameObject.Find("Canvas");
            var producer = canvas.transform.Find("Win").gameObject;
            producer.SetActive(true);

            float duration = 30.0f; //0.5 secs
            float currentTime = 0f;
            //
            while(currentTime < duration)
            {
                //float alpha = Mathf.Lerp(from, to, currentTime/duration);
                float y = producer.transform.localPosition.y + 0.01f * Time.deltaTime;
                producer.transform.localPosition = new Vector3(producer.transform.localPosition.x, y, producer.transform.localPosition.z);
                currentTime += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }

        public void FlickerClue()
        {
            GameObject canvas = GameObject.Find("Canvas");
            var winObj = canvas.transform.Find("Begin").Find("BeginClue").gameObject;
            winObj.SetActive(true);
            Text clue = winObj.GetComponent<Text>();
            //StartCoroutine(SimpleFilp(clue));
        }

        public void HideInit()
        {
            GameObject canvas = GameObject.Find("Canvas");
            var winObj = canvas.transform.Find("Begin").gameObject;
            winObj.SetActive(false);
        }

        public void ShowPause(bool flag)
        {
            GameObject canvas = GameObject.Find("Canvas");
            var winObj = canvas.transform.Find("PauseClue").gameObject;
            winObj.SetActive(flag);
        }

        private IEnumerator SimpleLerp(Text text, float from, float to)
        {
            float duration = 0.5f; //0.5 secs
            float currentTime = 0f;
            while(currentTime < duration)
            {
                float alpha = Mathf.Lerp(from, to, currentTime/duration);
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
                currentTime += Time.deltaTime;
                yield return null;
            }
        }

        float falpha = 0f;
        private IEnumerator SimpleFilp(Text text)
        {
            while(true)
            {
                if (falpha == 0f)falpha = Mathf.Lerp(0f, 1f, Time.deltaTime);
                if (falpha == 1f)falpha = Mathf.Lerp(1f, 0f, Time.deltaTime);
                text.color = new Color(text.color.r, text.color.g, text.color.b, falpha);
                yield return null;
            }
        }

        public void FadeIn()
        {
            if (LightTorch != null) {
                LightTorch.SetActive(true);
                Text targetText = LightTorch.GetComponent<Text>();
                if (targetText != null) {
                    StartCoroutine(SimpleLerp(targetText, 0f, 1f));
                }
            }
        }

        public void FadeOut()
        {
            Text targetText = LightTorch.GetComponent<Text>();
            if (targetText != null) {
                StartCoroutine(SimpleLerp(targetText, 1f, 0f));
            }
        }

        public void PortalFadeIn()
        {
            if (PortalText != null)
            {
                PortalText.SetActive(true);
                Text targetText = PortalText.GetComponent<Text>();
                if (targetText != null)
                {
                    StartCoroutine(SimpleLerp(targetText, 0f, 1f));
                }
            }
        }

        public void PortalFadeOut()
        {
            Text targetText = PortalText.GetComponent<Text>();
            if (targetText != null)
            {
                StartCoroutine(SimpleLerp(targetText, 1f, 0f));
            }
        }

    }
}


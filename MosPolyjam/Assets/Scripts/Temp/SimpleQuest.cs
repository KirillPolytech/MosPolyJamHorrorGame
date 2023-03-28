using System.Collections;
using UnityEngine;

namespace MosPolyJam
{
    public class SimpleQuest : MonoBehaviour, IQuestable
    {
        public void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public IEnumerator WaitForFinish()
        {
            yield return new WaitForSeconds(3);
            gameObject.SetActive(false);
        }
    }
}
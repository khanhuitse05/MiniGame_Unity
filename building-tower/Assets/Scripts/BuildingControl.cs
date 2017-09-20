using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingTower
{
    public class BuildingControl : MonoBehaviour
    {

        public static BuildingControl Instance { get; private set; }
        private void Awake() { Instance = this; }

        public GameObject pfBox;
        public Transform root;
        public Transform posSpawn;
        Box currentBox;
        Box preBox;
        public Transform groundPos;
        public Transform content;
        public GameObject roof;
        public float groundScale;
        List<GameObject> listBox;
        List<GameObject> listBoxPoor;
        void Start()
        {
            OnInit();
        }
        public void OnReset()
        {
            for (int i = 0; i < listBox.Count; i++)
            {
                listBox[i].SetActive(false);
                listBoxPoor.Add(listBox[i]);
            }
            listBox = new List<GameObject>();
            content.localPosition = Vector3.zero;
            isMove = false;
            preBox = null;
            roof.SetActive(false);
            Invoke("OnSpawnBox", 0.5f);
        }
        public void OnInit()
        {
            listBox = new List<GameObject>();
            listBoxPoor = new List<GameObject>();
            isMove = false;
            OnSpawnBox();
        }
        void OnSpawnBox()
        {
            if (listBoxPoor.Count > 0)
            {
                GameObject _obj = listBoxPoor[0];
                listBoxPoor.RemoveAt(0);
                _obj.transform.position = posSpawn.position;
                _obj.SetActive(true);
                listBox.Add(_obj);
                currentBox = _obj.GetComponent<Box>();
                currentBox.enabled = true;
                currentBox.OnInit();
            }
            else
            {
                GameObject _obj = Utils.Spawn(pfBox, root);
                _obj.transform.position = posSpawn.position;
                listBox.Add(_obj);
                currentBox = _obj.GetComponent<Box>();
                currentBox.enabled = true;
                currentBox.OnInit();
            }
        }
        /// <summary>
        /// TAP
        /// </summary>
        bool isTap = false;
        void Update()
        {
            if (isTap)
            {
                OnHold();
            }
            if (isMove)
            {
                OnMoveContent();
            }
        }
        void OnMoveContent()
        {
            float _yy = content.localPosition.y - Time.deltaTime * moveSpeed;
            if (_yy < nextPosContent)
            {
                _yy = nextPosContent;
                isMove = false;
                content.localPosition = new Vector2(content.localPosition.x, _yy);
                OnSpawnBox();
            }
            else
            {
                content.localPosition = new Vector2(content.localPosition.x, _yy);
            }
        }
        void OnHold()
        {
            if (currentBox != null && currentBox.isWait)
            {
                currentBox.OnHold();
                Debug.Log("OnHold");
            }
        }
        public void OnTap()
        {
            isTap = true;
            Debug.Log("OnTap");
        }
        public void OnUnTap()
        {
            isTap = false;
            Debug.Log("OnUnTap");
            if (currentBox != null && currentBox.isWait)
            {
                if (preBox != null)
                {
                    preBox.enabled = false;
                    currentBox.OnDrop(preBox.posTop.position);
                    scaleCorrect = currentBox.scale.x <= preBox.scale.x;
                }
                else
                {
                    currentBox.OnDrop(groundPos.position);
                    scaleCorrect = currentBox.scale.x <= groundScale;
                }
            }
        }
        bool scaleCorrect;
        bool isMove = false;
        float nextPosContent = 0;
        public float moveSpeed;
        public void OnDropFinish(float _hieght)
        {
            if (scaleCorrect)
            {
                isMove = true;
                nextPosContent = content.localPosition.y - _hieght;
                preBox = currentBox;
                currentBox = null;
            }
            else
            {
                roof.SetActive(true);
                roof.transform.position = preBox.posTop.position;
                roof.transform.localScale= preBox.scale;
                currentBox.gameObject.SetActive(false);
                preBox = currentBox;
                currentBox = null;
                Invoke("OnReset", 1);
            }
        }
    }
}

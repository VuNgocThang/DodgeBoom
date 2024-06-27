using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_ChickenMerge
{
    public class WallController : MonoBehaviour
    {
        public static WallController ins;
        public float HpMain;
        private void Awake()
        {
            ins = this;
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void ReceiveDamage(float damage_)
        {
            HpMain -= damage_;
            if (HpMain <= 0)
            {
                Debug.Log("Lose");
            }
        }

    }
}

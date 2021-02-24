using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WThred
{
    public class WDataManager : MonoBehaviour
    {
        /// <summary>
        /// int :task 넘버, WThreadData 데이터
        /// </summary>
        public Dictionary<int, WThreadData> data;
        public static WDataManager GET { private set; get; }
        public void Awake()
        {
            if (GET == null)
                GET = this;
            else
                Destroy(this);
        }
    }

}

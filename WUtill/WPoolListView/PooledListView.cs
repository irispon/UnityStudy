using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 작업자: 신원우
/// 무한 스크롤링(object pool)  ListView입니다. 상속해서 사용하면 되고. Data에는 prefab이 사용할 데이터를 입력하시면 됩니다. 21-06-24 추가된 부분이 많아서 코드 정리 한번 필요할 듯(지금은 조금 어거지임)
/// wide 여부를 급하게 추가하면서 if문으로 때웠는데 새롭게 widePooledList를 만드는게 베스트일거 같기도 함.
/// </summary>
/// <typeparam name="Data">prefab이 사용할 데이터 </typeparam>
/// 

///추가해야할 부분. Transform 에서 가장 첫번째 컨텐츠나 가장 마짐가 컨텐츠를 가져왔는데. 이 부분을 계산해서 프리팹에 직접 넣어줘야할 듯.(getcomponent 삭제)
public class PooledListView<Data> : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{


    [Header("자식 컴포넌트")]
    public ScrollRect _ScrollRect;
    public RectTransform _viewPortT;
    public RectTransform _DragDetectionT;
    public RectTransform _ContentT;
    public RectTransform _InitTransform;
    public XListItemPrefab<Data> _childPrefab;
   
    private Vector3 _InitPosition;
    [Header("아이템 크기(미 입력시 자동 할당)")]
    public float _ItemHeight = -1;      // => 이 부분은 아이템 크기 받으면 될 듯
    [Header("버퍼 크기")]
    public int _BufferSize=10;
    [Header("버퍼 visible")]
    public bool bufferVisible = false;

    private bool _Init=false;

    private float padding;
    private float spacing;

   // private List<XListItemPrefab> prefabs= new List<XListItemPrefab>();


    int TargetVisibleItemCount { get {


            if (isWide == false)
            {
                return Mathf.Max(Mathf.CeilToInt(_viewPortT.rect.height / _ItemHeight), 0);
            }
            else
            {
                return Mathf.Max(Mathf.CeilToInt(_viewPortT.rect.width / _ItemHeight), 0); } }//화면에 보이는 아이템들
            }
            
            
    int TopItemOutOfView { get {


            if (isWide == false)
            {
                return Mathf.CeilToInt(_ContentT.anchoredPosition.y / _ItemHeight);
            }
            else
            {
                return -Mathf.CeilToInt(_ContentT.anchoredPosition.x / _ItemHeight);
            }
            
        } }//view에서 나가는 top 아이템.
  

    float dragDetectionAnchorPreviousY = 0;//얼마나 움직였는지 확인

    [Header("horizental==true, vertical==false")]
    public bool isWide=false;
    #region Data
    public List<Data> _datas = new List<Data>();
    int dataHead = 0;
    int dataTail = 0;
    public List<XListItemPrefab<Data>> _prefabs;
    #endregion

    public void Awake()
    {
        Begin();
    }
    public virtual void Begin()
    {

    }
    public void Start()
    {
     

        BeginStart();
    }
    public virtual void BeginStart()
    {
        //if(_ItemHeight==-1)
        //_ItemHeight = _childPrefab.GetItemHeight();

    }
    public virtual void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
        if(isActive == true)
        {
            BeginActive();
        }
        else
        {
            EndActive();
        }

    }
    /// <summary>
    /// active가 되면 실행
    /// </summary>
    public virtual void BeginActive()
    {

    }
    /// <summary>
    /// active가 끝나면 실행
    /// </summary>
    public virtual void EndActive()
    {
    }

    /// <summary>
    /// 여기에서 아이템 관련된 선언(아이템 초기화. 데이터 입력 등등)을 하면 됩니다.
    /// </summary>
    /// <param name="data">데이터</param>
    public virtual void Setup(List<Data> data)
    {

        //_InitTransform.anchorMax = _ContentT.anchorMax;
        //_InitTransform.anchorMin = _ContentT.anchorMin;
        //_InitPosition = _ContentT.transform.localPosition;
        Init();
        _datas = data;
     
        _ItemHeight = _childPrefab.GetItemHeight();


        if (isWide == false)
        {
            VerticalLayoutGroup verticalgroup;
            if (_ContentT.TryGetComponent(out verticalgroup))
            {
                padding = verticalgroup.padding.top;
                spacing = verticalgroup.spacing;
            }

        }
        else
        {
            HorizontalLayoutGroup horizongroup;
            if (_ContentT.TryGetComponent(out horizongroup))
            {
                // padding = horizongroup.padding.left;
                spacing = horizongroup.spacing;

            }

        }

        if (isWide == false)
        {
         
            _DragDetectionT.sizeDelta = new Vector2(_DragDetectionT.sizeDelta.x, data.Count * _ItemHeight+ padding+(data.Count-1)*spacing);
          //  Debug.Log("is wide false");
        }

        else
        {
            _DragDetectionT.sizeDelta = new Vector2(data.Count * _ItemHeight+(data.Count - 1) * spacing, _DragDetectionT.sizeDelta.y);
            Debug.Log("데이터 크기" + data.Count + "아이템 높이" + _ItemHeight + "   dragDetection 크기" + _DragDetectionT.sizeDelta);
         //   Debug.Log("is wide true"+ _DragDetectionT.sizeDelta+"spacing    "+ spacing);
        }
       
        Debug.Log("데이터 숫자"+data.Count);


        if (data.Count > TargetVisibleItemCount + _BufferSize)//데이터가 buffersize + 보이는 크기 보다 크면 오브젝트 풀링 방식. 아니면 그냥 전부다 할당.(최대 크기 => 화면에 보이는 prefab(5~6개)  + buffer 크기
        {
            for (int i = 0; i < TargetVisibleItemCount + _BufferSize; i++)
            {


                if (data.Count < i)
                {
                    _prefabs.Add(Instantiate());
                    _prefabs[i].SetActive(false);
                }
                else
                {
                   // Debug.Log(data.Count + "     카운트" + "        " + i);
                    _prefabs.Add(Instantiate(data[i]));

                }
            }
        }
        else
        {
            for (int i = 0; i < data.Count; i++)
            {
                _prefabs.Add(Instantiate(data[i]));
            }

        }

        _ScrollRect.onValueChanged.AddListener(OnDragDetectionPositionChange);



    }
    public void Init()
    {
        _ScrollRect.onValueChanged.RemoveAllListeners();
        _ScrollRect.StopMovement();
        _ScrollRect.enabled = false;
        dataTail = 0;
        dataHead = 0;
        _DragDetectionT.position = _InitTransform.position;
        dragDetectionAnchorPreviousY = 0;
        Debug.Log(_prefabs.Count+"prefab count+++++++++++++++++++++++++++++++");
        if (_prefabs.Count > 0)
        {
            for (int i = 0; i < _prefabs.Count; i++)
            {
                Debug.Log("destroy prefb "+_prefabs.Count);
                Destroy(_prefabs[i].gameObject);

            }
            _prefabs.Clear();
        }
        Debug.Log(_prefabs.Count + "prefab count2+++++++++++++++++++++++++++++++");
        _ContentT.position = _InitTransform.position;
   //     _Init = true;
        _ContentT.localPosition = new Vector3(_ContentT.localPosition.x, 0, _ContentT.localPosition.z);
        _ScrollRect.enabled = true;
  
    }


    public virtual void ChangeData(List<Data> data)
    {
        _ScrollRect.onValueChanged.RemoveAllListeners();
        _ScrollRect.StopMovement();
        _ScrollRect.enabled = false;

        dataTail = 0;
        dataHead = 0;
        _DragDetectionT.position = _InitTransform.position;
        dragDetectionAnchorPreviousY = 0;

        _Init = true;


        if (isWide == false)
        {
            _DragDetectionT.sizeDelta = new Vector2(_DragDetectionT.sizeDelta.x, data.Count * _ItemHeight + padding + (data.Count - 1) * spacing);
            //  Debug.Log("is wide false");
        }

        else
        {
            _DragDetectionT.sizeDelta = new Vector2(data.Count * _ItemHeight + (data.Count - 1) * spacing, _DragDetectionT.sizeDelta.y);
            //   Debug.Log("is wide true"+ _DragDetectionT.sizeDelta+"spacing    "+ spacing);
        }


        for (int i =0; i < _prefabs.Count; i++)
        {
         
            if (data.Count>i)
            {
                _prefabs[i].SetActive(true);
                _prefabs[i].Setup(data[i]);
                dataTail++;
            }
            else
            {
                _prefabs[i].SetActive(false);
            }
        }

        _datas = data;
       
        _DragDetectionT.position = _InitTransform.position;
        _ContentT.localPosition = new Vector3(_ContentT.localPosition.x, 0, _ContentT.localPosition.z);
        _ContentT.position = _InitTransform.position;
        _ScrollRect.enabled = true;
        _ScrollRect.onValueChanged.AddListener(OnDragDetectionPositionChange);

    }
    private XListItemPrefab<Data> Instantiate(Data data)
    {
        XListItemPrefab< Data> itemGO = Instantiate(_childPrefab);
        itemGO.transform.SetParent(_ContentT);
        itemGO.SetActive(true);
        itemGO.transform.localScale = Vector3.one;
        itemGO.Setup(data);
        dataTail++;
        return itemGO;
    }
    private XListItemPrefab<Data> Instantiate()
    {
        XListItemPrefab< Data> itemGO = Instantiate(_childPrefab);
        itemGO.transform.SetParent(_ContentT);
        itemGO.SetActive(true);
        itemGO.transform.localScale = Vector3.one;
        return itemGO;
    }


    #region UI 이벤트 관련

    public void OnDragDetectionPositionChange(Vector2 dragNormalizePos)
    {
        if (_Init == true)//초기화용
        {
            _Init = false;
        }
        else
        {

            float dragDelta = _DragDetectionT.anchoredPosition.y - dragDetectionAnchorPreviousY;
            if (isWide == true)
            {
                dragDelta = _DragDetectionT.anchoredPosition.x - dragDetectionAnchorPreviousY;
            }

          if(isWide==false)
            _ContentT.anchoredPosition = new Vector2(_ContentT.anchoredPosition.x, _ContentT.anchoredPosition.y + dragDelta);
          else
            _ContentT.anchoredPosition = new Vector2(_ContentT.anchoredPosition.x + dragDelta, _ContentT.anchoredPosition.y);

            UpdateContentBuffer();

            if (isWide == false)
                dragDetectionAnchorPreviousY = _DragDetectionT.anchoredPosition.y;
            else
                dragDetectionAnchorPreviousY = _DragDetectionT.anchoredPosition.x;
        }

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isWide == false)
            dragDetectionAnchorPreviousY = _DragDetectionT.anchoredPosition.y;
        else
            dragDetectionAnchorPreviousY = _DragDetectionT.anchoredPosition.x;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {

    }

    #endregion



    #region 무한 스크롤링 메카니즘

    void UpdateContentBuffer()
    {
        //  Debug.Log("updateContentBuffer activate"+_ContentT.localPosition);
        //Debug.Log("ttt");

      

        if(TopItemOutOfView > _BufferSize)//아래로 내려갈 때
        {
       
            if (dataTail >= _datas.Count)
            {
             //   Debug.Log("??");
                return;
            }

            Transform firstChildT = _ContentT.GetChild(0);
            firstChildT.SetSiblingIndex(_ContentT.childCount - 1);//위에 있는 오브젝트를 아래로 내림.
            firstChildT.GetComponent<XListItemPrefab<Data>>().Setup(_datas[dataTail]);
           // Debug.Log("변경된 sibling     "+ firstChildT.transform.GetSiblingIndex());
           if(isWide==false)
            _ContentT.anchoredPosition = new Vector2(_ContentT.anchoredPosition.x, _ContentT.anchoredPosition.y - _ItemHeight);//아이템 높이 만큼 내림.
            else
           _ContentT.anchoredPosition = new Vector2(_ContentT.anchoredPosition.x + _ItemHeight, _ContentT.anchoredPosition.y);//아이템 높이 만큼 내림.
         
           


            dataHead++;
            dataTail++;
           // Debug.Log(dataTail++);
        }
        else if(TopItemOutOfView < _BufferSize)//위로 올라갈 때
        {
       
            if(dataHead <= 0)
            {
           //     Debug.Log("deataHead?"+dataHead);
                return;
          
            }
         
            Transform lastChildT = _ContentT.GetChild(_ContentT.childCount - 1);
            lastChildT.SetSiblingIndex(0);
            dataHead--;
            dataTail--;
            lastChildT.gameObject.GetComponent<XListItemPrefab<Data>>().Setup(_datas[dataHead]);

            if (isWide==false)
            _ContentT.anchoredPosition = new Vector2(_ContentT.anchoredPosition.x, _ContentT.anchoredPosition.y + _ItemHeight);
            else
            _ContentT.anchoredPosition = new Vector2(_ContentT.anchoredPosition.x - _ItemHeight, _ContentT.anchoredPosition.y);
        }
    }

    #endregion
}

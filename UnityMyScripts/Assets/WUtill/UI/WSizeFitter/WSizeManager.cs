using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WSizeManager : MonoBehaviour
{
    public static WSizeManager Get { get; private set; }
    public List<ISizeFitter> _fitters;
    // Start is called before the first frame update

    public void Update()
    {
        #if (UNITY_EDITOR)
        for(int i = 0; i < _fitters.Count; i++)
        {
            _fitters[i].Fit();
        }
        #endif
    }
    public void Add(ISizeFitter fitter)
    {
        _fitters.Add(fitter);
        fitter.Fit();
    }
    // Update is called once per frame

}

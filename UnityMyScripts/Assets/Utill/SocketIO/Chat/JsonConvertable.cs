using Newtonsoft.Json;
using SimpleJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonConvertable 
{


    public string Convert()
    {
        
        return JsonConvert.SerializeObject(this);
    }
}

async await가 비동기적 처리라는건 모두가 알고 있을 것이다.



하지만 이미 만들어놓은 프로젝트에 async await가 부득이하게 들어갈 경우 이식하는데 많은 코스트가 소모될 것이다.



그럴 때 이 비동기 방식을 동기적으로 구현하려면 



async Tast<string) abc()

{

​	return await "string"

}

​    var task = Task.Run(async () =>
​    {
​      return await ayncTest();
​    });
​    Debug.Log(task.Result);





이런 코드를 사용해서 얻어오면 된다.








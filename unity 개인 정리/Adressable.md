Addressable

 

런타임 메모리 관리

 메모리에서 효율적으로 언로드가 가능.

어드레서블을 통해 로드하면 로드된 에셋과 이들의 종속성에 대한 참조 횟수가 계산되므로 더 이상 사용하지 않는 번들과 관련 에셋을 자동으로 언로드함. 프로파일러 제공

**더욱 빠른 반복 작업**

어드레서블은 에셋 로드 프로세스에 대한 몇 가지 핵심적인 추상화 기능을 제공하여 콘텐츠 변경 시 바로 확인할 수 있으므로 반복 작업을 더욱 빠르게 수행할 수 있습니다.

- **플레이** **모드** **시뮬레이션**: 에셋을 빌드하지 않고도 디스크에서 직접     로드하며, 동시에 프로파일러를 통해 에셋 참조 횟수를 디버깅할 수 있습니다.
- **빠른** **증분** **빌드**: 어드레서블에는 증분 빌드의 속도와 안정성을     향상시키는 새로운 *스크립터블* *빌드* *파이프라인(Scriptable     Build Pipeline*)이 함께 제공됩니다.
- **에디터** **호스팅** **서비스**: 로컬에 어드레서블 콘텐츠를 서비스하기 위한     빌트인 기능, LAN에 연결된 기기들이 로컬 기기상에서 빠르고 편리하게 콘텐츠에 대한 반복     작업을 수행할 수 있는 방법을 제공합니다.

 

어드레서블은 에셋의 참조 및 패키징 문제를 분리하고, 플레이 모드 및 배포된 플레이어 빌드에서 반복 작업을 더욱 빠르게 수행할 수 있게 해주며, 자동 메모리 관리 및 프로파일링 툴을 제공합니다. 이를 통해 콘텐츠의 복잡도에 상관없이 모든 사용 사례에 적용할 수 있도록 확장하고 커스터마이즈 가능





# **[Open Source] Rule Based Unity Addressable Asset Importer**

 

A simple rule based addressable asset importer. It marks assets as addressable, by applying to files having a path matching the rule pattern.
 
 https://github.com/favoyang/unity-addressable-importer
 
 Features

- Path pattern supports both wildcard     (*, ?) and regex
- Specify a static group or dynamic     group.
- Add or replace labels 
- Address replacement (raw path,     simplified, regex based replacement)
- Install as upm package

Feel free to modify it based on your demand.

 

 

 

 

Addrssable 커스텀 소스

 

 

 

![img](file:///C:/Users/82109/AppData/Local/Temp/msohtmlclip1/01/clip_image002.jpg)

 

 

https://forum.unity.com/threads/set-addressable-via-c.671938/#post-4730333

 

C#으로 addressable을 설정 가능한지에 대한 질문과 관련된 코드

 

 

![img](file:///C:/Users/82109/AppData/Local/Temp/msohtmlclip1/01/clip_image004.jpg)

 

 

https://forum.unity.com/threads/set-asset-as-addressable-through-script.718751/

 

 

단점

 

단점에 관련되어서 신뢰도가 높지는 않지만 이러한 이슈가 있다함. 직접 테스트 필요

 

- 에셋번들 배리언트를 지원 안 함
- 패키지에 포함된 씬 로딩 불가
- 패키지에 포함된 cginc 로딩 불가
- 다수 객체에 대한 프로퍼티 변경시 에디터 멈춤
- 에셋 경로가 텍스트로 다 보이는 등의 보안 이슈
- 버전 컨트롤 툴과 호환 안됨
- 분리된 번들의 단일 에셋교체시 풀빌드 시간의 30~40% 소요
- 출시 후 추가 콘텐츠에 대한 버전 관리 및 배포 불편

 

 

 

 
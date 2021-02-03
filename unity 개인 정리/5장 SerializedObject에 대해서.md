5장 SerializedObject에 대해서

유니티에서는 파일(유니티의 Asset)을 조금 특수한 형식으로 변환시켜 사용합니다. 본 장에서는, 유니티에서 오브젝트를 다룰 때 기반으로  하는 SerializedObject에 대해서 해설합니다. 또한, [Serialize에 대한 모든 것을 망라한 정보]는 유니티 공식 메뉴얼에 설명되어 있습니다. 본 장에서는 입문으로써 알아야 하는 정보를 해설합니다.
http://docs.unity3d.com/Manual/script-Serialization.html

5.1 SerializedObject란

SerializedObject는, Serialize된 데이터를 Unity에서 다루기 쉬운 쪽으로 가공한 것입니다. 이로 인해, 다양한 데이터에 접근할 수 있습니다. 또한 Undo 처리와 게임 오브젝트에서 Prefab를 쉽게 작성할 수 있도록 합니다.

SerializedObject는, Unity 상에서 다루는 모든 오브젝트와 관계되어 있습니다. 보통 다루고 있는 Asset(재질이나 텍스쳐, 애니메이션 클립 등)도 SerializedObject가 없으면 만들 수 없습니다.

[UnityEngine.Object와 SerializedObject의 관계]

유니티 에디터 상에서는, 모든 오브젝트(UnityEngine.Object)는 SerializedObject로 변환되어 다루어집니다.  인스펙터에서 컴포넌트의 수치를 편집할 때도, Component의 인스턴스를 편집하고 있는게 아니라,  SerializedObject의 인스턴스를 편집하고 있는 것입니다.



​                                    ![img](https://mblogthumb-phinf.pstatic.net/20160724_141/hammerimpact_1469362066269cyPY4_PNG/ss0ss3.png?type=w800)                            


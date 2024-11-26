using System.Numerics;

namespace WebProject;

 

public partial class Calculator{
    public int num1 {get; set;}
    public int num2 {get; set;}
}

 
// // public partial class DD {

// // }

// // public partial class CC{
// //     public extern Calculator FindById()
// // }
// // public partial interface CC{
// //     public Calculator FindById(){

// //     }
// // }
// // 이게 문제는 증분형 소스 생성기라 클래스를 삭제했다가 새로 생성하는게 안됨
// // partial 로 여러개에  대해서 수정해야하는데 
// // public partial interface Myclass 면  새로 생성할때 partial 도 인터페이스가 강제됨.
// // => 아래와 같은 방법 응용할 수 있음.
// public partial class DD
// {
//     public void Initialize()
//     {
//         Console.WriteLine("Initialization Started");
//         PartialInitialize();
//     }

//     public partial void PartialInitialize(); // 선언만
// }// 다른 파일에서 실제 구현
// public partial class DD
// {
//    public partial void PartialInitialize()
//     {
//         Console.WriteLine("Partial Initialization Logic");
//     }
     
// }
// public partial class ICRepository<T> where T : class{
//     public partial void PartialInitialize(); // 선언만
// }
// public partial class ICRepository<T> where T : class
// {
//     public partial void PartialInitialize()  // 메서드 구현
//     {
//         // 메서드의 실제 구현 내용
//         Console.WriteLine("Partial Initialize 실행!");
//     }
// }

// // 보면 partial 클래스를 해도 정적인 콘텐츠로 정의까지 이미 되어 있어야함 


// // 
// public interface IICRepository<T ,E> where T : class where E : struct{
//     public Task<T> FindById();
// }

// //                     이런식으로 Entity 클래스와 ID의 유형을 입력하도록 하ㅣ고 
// public partial class IID : IICRepository<Calculator, int>
// {
//     public partial Task<Calculator> FindById();   
// } 
// public partial class IID : IICRepository<Calculator>{
//     [Getter]
//     public int Id;
//     public partial Task<Calculator> FindById(){
//         return Task.FromResult(new Calculator());
//     }
// }
// // 일단 이방법은 불가능


// [AttributeUsage(AttributeTargets.Field)]
// public class GetterAttribute:Attribute{
    
   
// }

// // 인터페이스만 만들고 인터페이스를 순회하면서 인터페이스를 IIncrementalSourceGenerator해당 클래스 파일을 수정할수 있나 ?


// [AttributeUsage(AttributeTargets.Method)]
// public class MethodAttribute:Attribute{
//     public string? Message {get;}

//     public MethodAttribute(string message){
//         Message = message;
//     }
// }

// public partial class MyClass{
//     [Method("dd")]
//     public void MyDynamicMethod();
// }

// public abstract partial class MyClass2{
//     public partial void MyDD();
// }
// public partial class MyClass2{
//     public partial void MyDD(){

//     }
// }

// public abstract partial class MyClass3
// {
//     public partial void MyDD(); // 본문 없이도 허용
// }
// public abstract partial class MyClass4
// {
//     public abstract void MyDD(); // abstract로 본문 없이 선언
// }

// public partial class  MyClass6{
//     public virtual void MyDD();
// }

// public partial class MyClass5
// {
//   partial void MyDD(); // partial 메서드는 본문이 없어도 허용됨
// }
// public partial class MyClass5{
//     partial void MyDD()
//     {
//         Console.Write("d");
//     }
// }

// // 빌드 전 작성된 코드
// public partial class MyClass7{
//     partial void MyDD();
//     // 이렇게했을때 
// }

// //빌드 후 생성되는코드 
// public partial class MyClass7
// {
    
    
//     // 선언된 함수들을 
//     public int Mydd(){
//         MyDD();
//         return 1;
//     }
// }

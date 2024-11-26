using System.Collections.Generic;
using System;
using WebProject;
using System.Security.Cryptography.X509Certificates;





// 이렇게하면 객체를 생성할  수 없음
public abstract partial class MyClass1
{
    partial void Func2(); // 선언
}

public partial class MyClass1
{
    partial void Func2() // 구현
    {           
        Console.WriteLine("Implemented Func2");
    }
    public void Fun3()
    {
        Console.WriteLine();
    }
}
// Repository 객체 인스턴스로 메서드 접근해야 하므로 
public partial  class MyClass2
{
    partial void Func();
}
public partial class MyClass2
{
    partial void Func2();
}

// 속성  -  Entity 객체 
[AttributeUsage(AttributeTargets.Class )]
public class EntityAttribute : Attribute
{

}
[AttributeUsage(AttributeTargets.Method)]
public class QueryAttribute : Attribute
{
    public string? QueryMessage { get;    }
    public QueryAttribute(string msg)
    {
        this.QueryMessage = msg;    
    }
    
}
// 예시 테이블 객체에는 무조건                                    
// 속성 -  Repository 객체
[AttributeUsage(AttributeTargets.Interface)]
public class RepositoryAttribute : Attribute
{

}
[AttributeUsage(AttributeTargets.Field)]
public class IDAttribute : Attribute
{
}
[Entity]
public class Student
{

    [ID]  // EntityAttribute.IDAttribute 적용
    private int id;
}
// 사용자가 상속받을 인터페이스  
public interface IRepository<T > where T : class  
{
    T FindById(int id);
    IEnumerable<T> FindAll();
    void Save(T entity);
    void Delete(T entity);
}
 // 사용자가 이렇게 만들고  이렇게 상속받게하던가
public interface IStudentRepository<T>: IRepository<T> where T : class
{
                           
    T FindByName(string naem);
}
// 사용자가 이렇게만들면 이때 사용자는 IRepositroy 안에 Entitity 객체를 명시해야 하며
// [Repository] 라는 속성을 명시해야한다.
// partial 클래스로는 구현이 힘들어 보임 . 인스턴스객체로 접근해서 해야하는데 
// 결국 객체를 통해서 메서드를 정의해야하기에 인터페이스를 내가 만들고
// 사용자가 인터페이스를 만드는데 이때 명명규칙을 인터페이스명을 "I"로시작하게하여 
//"I"StudnetRepositroy: IRepository<Student> 이렇게 하도록 한다.
// 빌드 후 "I"를 제거한 StudentRepository를 IIncrementalGerator가 자동으로 생성하도록한다.
//  이때 사용자가 만들 인터페이스 방식은 이런형식
[Repository]
public interface ITestRepository : IRepository<Student>
{

}
public class TestRepository : ITestRepository
{
    public void Delete(Student entity)
    {
        IEnumerable<Student> students  = new List<Student>();   
        

        throw new NotImplementedException();
    }

    public IEnumerable<Student> FindAll()
    {
        throw new NotImplementedException();
    }

    public Student FindById(int id)
    {
        throw new NotImplementedException();
    }

    public void Save(Student entity)
    {
        throw new NotImplementedException();
    }
}

[Repository]
public interface IStudentRepository2 : IRepository<Student>
{
    Student FindByName(string name);
    Student FindByUser(string username);
    void DeleteByName(string username);
    // 만약 사용자가 직접 쿼리를 던지고 싶다면 
    // [Query = "insert ~~"] 이런식임 
    [Query("insert")]
    void DeletedoSomething();

    void DeletedoSomething2();
}

// 아래는 이제 IIncrementalGenerator 에서 정의될 코드 
public class StudentRepository : IStudentRepository2
{
    IStudentRepository2? repository2;
   

    public void Delete(Student entity)
    {  
        repository2?.Delete(entity);
        throw new NotImplementedException();

    }

    public void DeleteByName(string username)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Student> FindAll()
    {
        throw new NotImplementedException();
    }

    public Student FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Student FindByName(string name)
    {
        throw new NotImplementedException();
    }

    public Student FindByUser(string username)
    {
        throw new NotImplementedException();
    }

    public void Save(Student entity)
    {
        throw new NotImplementedException();
    }
}

public class StudentRepository<T> : IStudentRepository<T> where T : class
{
    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> FindAll()
    {
        throw new NotImplementedException();
    }

    public T FindById(int id)
    {
        throw new NotImplementedException();
    }

    public T FindByName(string naem)
    {
        throw new NotImplementedException();
    }

    public void Save(T entity)
    {
        throw new NotImplementedException();
    }
}

public partial class Repository<T> : IRepository<T> where T : class
{

     
    // IRepository의 Delete 구현 (partial 메서드 호출)
    public void Delete(T entity)
    {
       
    }

    public T FindById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> FindAll()
    {
        throw new NotImplementedException();
    }

    public void Save(T entity)
    {
        throw new NotImplementedException();
    }

}
 
public class DD
{
    public void ddd()
    {
        Repository<Calculator> repository = new Repository<Calculator>();

        repository.FindAll();
        repository.Save(new Calculator()); 
    }
}

////  이때 
//using System.Security.Cryptography.X509Certificates;
//using WebProject;

//[AttributeUsage(AttributeTargets.Class)]
//public class EntityAttribute : Attribute
//{

//}
//[AttributeUsage(AttributeTargets.Interface)]
//public class RepositoryAttribute : Attribute
//{

//}
//// 다음은 엔티티 클래스
//// 인터페이스에서 가져온 T 인자를 가져온 후 .TextName3
//[Entity]
//public class StudentTable
//{
//    public int Id { get; set; } 
//    public string Name { get; set; }
//    public string Description { get; set; }
//    public string FirstName { get; set; } = string.Empty;
//    public string LastName { get; set; }
//    StudentTable(int id , string na , string des  , string first  , string last ) {
//        Id = id;
//        Name = na;
//        Description = des;
//        FirstName = first;
//        LastName = last;
//    }  

//}
//// 상속을 할 인터페이스 
//public interface ICRepotiory<T , ID> where T : class where ID : struct
//{
//    T FindById(ID id);
//    IEnumerable<T> FindAll();
//    void Save(T entity);
//    void Delete(T entity);
//}
//// 이건 사용자가 만들 인터페이스 
//[Repository]     //  이때 T 인자 값은 Entity 객체 , 기본 타입이 되는 ID의 타입
//public interface ISTRepository : ICRepotiory<Calculator, int>
//{
//    Calculator FindByNum(int id);
//    Calculator FindByIdOver30(int id);
//    Calculator FindByIdUnder30(int id);
//    Calculator DeleteById(int id);
//    Calculator DeleteByName(string name);

//}


//public class ISTRepository3 : ISTRepository
//{
//    public void Delete(Calculator entity)
//    {
//        throw new NotImplementedException();
//    }

//    public IEnumerable<Calculator> FindAll()
//    {
//        throw new NotImplementedException();
//    }

//    public Calculator FindById(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public void Save(Calculator entity)
//    {
//        throw new NotImplementedException();
//    }
//}

//// ICRepotiory 를 상속한 클래스를 순회하면서 
//// 이런식으로  IIncrementalGenerator 에서 생성할코드 
//// 근데 이렇게 상속하고 또 상속하는 구조는 안좋음 
//public class ISTRepository2 : ISTRepository 
//{
//    public void Delete(Calculator entity)
//    {


//        throw new NotImplementedException();
//    }

//    public Calculator DeleteById(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public Calculator DeleteByName(string name)
//    {
//        throw new NotImplementedException();
//    }

//    public IEnumerable<Calculator> FindAll()
//    {
//        throw new NotImplementedException();
//    }

//    public Calculator FindById(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public Calculator FindByIdOver30(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public Calculator FindByIdUnder30(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public Calculator FindByNum(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public void Save(Calculator entity)
//    {
//        throw new NotImplementedException();
//    }
//}
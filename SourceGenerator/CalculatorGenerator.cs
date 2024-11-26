using System;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGenerator.SharedLibrary;

namespace SourceGenerator;

[Generator]
public class CalculatorGenerator : IIncrementalGenerator
{

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<ClassDeclarationSyntax> calculatorClassesProvider = context.SyntaxProvider.CreateSyntaxProvider(
        predicate: (SyntaxNode node, CancellationToken cancelToken) =>
        {
            //the predicate should be super lightweight so it can quickly filter out nodes that are not of interest
            //it is basically called all of the time so it should be a quick filter
            return node is ClassDeclarationSyntax classDeclaration && classDeclaration.Identifier.ToString() == "Calculator";
        },
        transform: (GeneratorSyntaxContext ctx, CancellationToken cancelToken) =>
        {
            //the transform is called only when the predicate returns true
            //so for example if we have one class named Calculator
            //this will only be called once regardless of how many other classes exist
            var classDeclaration = (ClassDeclarationSyntax)ctx.Node;
            return classDeclaration;
        }
        );


        context.RegisterSourceOutput(calculatorClassesProvider, (sourceProductionContext, calculatorClass) => Execute(calculatorClass, sourceProductionContext));
    }
    
    /// <summary>
    /// This method is where the real work of the generator is done
    /// This ensures optimal performance by only executing the generator when needed
    /// The method can be named whatever you want but Execute seems to be the standard 
    /// </summary>
    /// <param name="calculatorClass"></param>
    /// <param name="context"></param>
    public void Execute(ClassDeclarationSyntax calculatorClass, SourceProductionContext context)
    {
        GeneratorLogging.SetLogFilePath("F:\\GeneratorLogger\\log.txt");
        try
        {
            var calculatorClassMembers = calculatorClass.Members;
            GeneratorLogging.LogMessage($"[+] Found {calculatorClassMembers.Count} members in the Calculator class");
            //check if the methods we want to add exist already 
            var addMethod = calculatorClassMembers.FirstOrDefault(member => member is MethodDeclarationSyntax method && method.Identifier.Text == "Add");
            var subtractMethod = calculatorClassMembers.FirstOrDefault(member => member is MethodDeclarationSyntax method && method.Identifier.Text == "Subtract");
            var multiplyMethod = calculatorClassMembers.FirstOrDefault(member => member is MethodDeclarationSyntax method && method.Identifier.Text == "Multiply");
            var divideMethod = calculatorClassMembers.FirstOrDefault(member => member is MethodDeclarationSyntax method && method.Identifier.Text == "Divide");
            
            GeneratorLogging.LogMessage("[+] Checked if methods exist in Calculator class");
            
            //this string builder will hold our source code for the methods we want to add
            StringBuilder calcGeneratedClassBuilder = new StringBuilder();
            foreach (var usingStatement in calculatorClass.SyntaxTree.GetCompilationUnitRoot().Usings)
            {
                calcGeneratedClassBuilder.AppendLine(usingStatement.ToString());
            }
            GeneratorLogging.LogMessage("[+] Added using statements to generated class");
            
            calcGeneratedClassBuilder.AppendLine();
            
            //The previous Descendent Node check has been removed as it was only intended to help produce the error seen in logging
            BaseNamespaceDeclarationSyntax? calcClassNamespace = calculatorClass.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
            calcClassNamespace ??= calculatorClass.Ancestors().OfType<FileScopedNamespaceDeclarationSyntax>().FirstOrDefault();
            
            if(calcClassNamespace is null)
            {
                GeneratorLogging.LogMessage("[-] Could not find namespace for Calculator class", LoggingLevel.Error);
            }
            GeneratorLogging.LogMessage($"[+] Found namespace for Calculator class {calcClassNamespace?.Name}");
            calcGeneratedClassBuilder.AppendLine($"namespace {calcClassNamespace?.Name};");
            calcGeneratedClassBuilder.AppendLine($"{calculatorClass.Modifiers} class {calculatorClass.Identifier}");
            calcGeneratedClassBuilder.AppendLine("{");
            
            //if the methods do not exist, we will add them
            if (addMethod is null)
            {
                //when using a raw string the first " is the far left margin in the file
                //if you want the proper indention on the methods you will want to tab the string content at least once
                calcGeneratedClassBuilder.AppendLine(
                """
                    public int Add(int a, int b)
                    {
                        var result = a + b;
                        Console.WriteLine($"The result of adding {a} and {b} is {result}");
                        return result;
                    }
                """);
            }
            if (subtractMethod is null)
            {
                calcGeneratedClassBuilder.AppendLine(
                """
                
                    public int Subtract(int a, int b)
                    {
                        var result = a - b;
                        if(result < 0)
                        {
                            Console.WriteLine("Result of subtraction is negative");
                        }
                        return result; 
                    }
                """);
            }
            if (multiplyMethod is null)
            {
                calcGeneratedClassBuilder.AppendLine(
                """
                
                    public int Multiply(int a, int b)
                    {
                        return a * b;
                    }
                """);
            }
            if (divideMethod is null)
            {
                calcGeneratedClassBuilder.AppendLine(
                """
                
                    public int Divide(int a, int b)
                    {
                        if(b == 0)
                        {
                            throw new DivideByZeroException();
                        }
                        return a / b;
                    }
                """);
            }
            calcGeneratedClassBuilder.AppendLine("}");
            //while a bit crude it is a simple way to add the methods to the class
            
            GeneratorLogging.LogMessage("[+] Added methods to generated class");
            
            //to write our source file we can use the context object that was passed in
            //this will automatically use the path we provided in the target projects csproj file
            context.AddSource("Calculator.Generated.cs", calcGeneratedClassBuilder.ToString());
            GeneratorLogging.LogMessage("[+] Added source to context");
        }
        catch (Exception e)
        {
            GeneratorLogging.LogMessage($"[-] Exception occurred in generator: {e}", LoggingLevel.Error);
        }
    }
}
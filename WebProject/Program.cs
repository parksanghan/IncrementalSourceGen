using Microsoft.VisualBasic;

namespace WebProject;

public class Project
{
   
    public static async Task Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
   
        // Add services to the container.
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        
        app.MapGet("/",() =>
        {
                Calculator myCalc = new Calculator();
                myCalc.num1 = 5;
                myCalc.num2 = 10;
               int additionResult = myCalc.Add(myCalc.num1, myCalc.num2);
               int subtractionResult =  myCalc.Subtract(myCalc.num1, myCalc.num2);
               int multiplyResult =  myCalc.Multiply(myCalc.num1, myCalc.num2);
               int divideResult =  myCalc.Divide(myCalc.num1, myCalc.num2);
               string response = $"""
                              Successfully executed source generated methods.
                              Results:
                                  Add: {additionResult}
                                  Subtract: {subtractionResult}
                                  Multiply: {multiplyResult}
                                  Divide: {divideResult}
                              """;
         



               return response;
        });
        
       await app.RunAsync();
    }
}
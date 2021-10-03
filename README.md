# JsonApi
Tiny library for communication between apps using JSON

## Basic usage

### Model
Model is standard C# object that will be serialized by System.Text.Json.
You can use data annotations to validate properties.

```c#
public class ExampleModel
{
    [MaxLength(10)]
    public string TextWithMaxLength { get; set; }

    [Required]
    public string RequiredTest { get; set; }
}
```

### Api handlers
Api handlers are classes containing method called after receiving adequate model.
It have to inherit `ApiHandler` class.

* Only one handler can be registered for each type.

```c#
public class ModelHandler : ApiHandler<ExampleModel>
{
    protected override void Handle(ExampleModel data)
    {
        Console.WriteLine(data.RequiredText);
    }
}
```

### Resolver
Resolver takes JSON string, deserialize it and sends call to Api handler.

```c#
var resolver = new Resolver(new IApiHandler[]
{
    new ModelHandler()
});

resolver.CallHandler("json string");
```

### Sender
Sender convert objects into JSON strings that can be handled by Resolver.

```c#
var sender = new Sender();
var data = new ExampleModel
{
    Text = "text",
    RequiredTest = "text"
};

var dataString = sender.GetDataString(data);
```

## Dependency injection
JsonApi can be used with DI. This example uses Autofac.
```c#
 public static class DiSetup
{
    public static IContainer Setup()
    {
        var builder = new ContainerBuilder();

        builder.RegisterAssemblyTypes(
                Assembly.GetExecutingAssembly())
            .Where(t => t.Name.EndsWith("Handler"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<Resolver>()
            .As<IResolver>()
            .InstancePerLifetimeScope();
        
        return builder.Build();
    }
}
 ```

## Data format
Serialized object has root key with C# type name of model.

```json
{"ExampleModel":{"TextWithMaxLength": "text", "RequiredTest": "text"}} 
```
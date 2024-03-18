# WebApplication con Asp.Net Core
Este proyecto es una aplicaci�n web desarrollada con ASP.NET Core, un framework para construir aplicaciones web y servicios con .NET. ASP.NET Core es ligero, de alto rendimiento y permite el desarrollo de aplicaciones en patrones de dise�o MVC (Model-View-Controller) y Razor Pages para facilitar el desarrollo de interfaces de usuario din�micas.

ASP.NET Core es adecuado tanto para el desarrollo de p�ginas web con Razor Pages, que promueve un patr�n de dise�o basado en la productividad y la simplicidad, como para la creaci�n de servicios web y APIs RESTful utilizando el enfoque de controladores de API, sobre este ultimo nos centraremos en este proyecto.

## C�mo agregar controladores de API

### Uso de Minimal APIs

Una manera sencilla de implementar funcionalidades como un "Hello World!"  es utilizando Minimal APIs. Este enfoque est� dise�ado para crear HTTP APIs con m�nimo esfuerzo y c�digo, haci�ndolo ideal para microservicios y aplicaciones peque�as.
Las Minimal APIs reducen la necesidad de boilerplate (c�digo repetitivo) al definir rutas y manejar solicitudes, permitiendo una sintaxis m�s limpia y directa.

```c#
app.MapGet("/", () => "Hello World!");
```

El contenido del **Program.cs** debera parecerse a lo siguiente 

```c#
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.MapGet("/add/{arg1}/{arg2}", (int arg1, int arg2) => $"Result: {arg1 + arg2}");

app.Run();
```

Este c�digo configura una aplicaci�n web que escucha en la ra�z del sitio (/) y devuelve "Hello World!" cuando se accede a ella.
</br>
El segundo endpoint **/add/{arg1}/{arg2}** est� dise�ado para recibir dos argumentos num�ricos (arg1 y arg2) directamente de la URL, sumarlos, y devolver el resultado en formato de texto. 

### Uso de Controladores

Existe otra forma de organizar y manejar solicitudes HTTP en tu aplicaci�n es mediante el uso de controladores en la carpeta **Controllers**.
Esta es una pr�ctica com�n en aplicaciones ASP.NET Core MVC o en aplicaciones que implementan APIs RESTful.
</br>
Los controladores ofrecen una estructura m�s formalizada para manejar solicitudes HTTP, agrupando l�gicas de operaciones similares en clases dedicadas.
Cada controlador puede contener m�ltiples acciones, que son m�todos que responden a diferentes rutas o tipos de solicitudes HTTP (como **GET, POST, PUT, DELETE**, etc.).

#### Enfoques y Ventajas
* El uso de controladores es particularmente beneficioso en los siguientes escenarios:
* Aplicaciones con L�gica de Negocio Compleja: Los controladores permiten una separaci�n clara entre la l�gica de presentaci�n y la l�gica de negocio, facilitando el mantenimiento y la escalabilidad de la aplicaci�n.
* APIs RESTful: Para proyectos que implementan APIs RESTful, el uso de controladores es un enfoque est�ndar que permite una organizaci�n clara de endpoints, apoyando principios REST como el uso de diferentes m�todos HTTP para representar operaciones CRUD.
* Integraci�n con Tecnolog�as de ASP.NET Core: Los controladores se integran estrechamente con otras caracter�sticas de ASP.NET Core, como el enlace de modelos, validaci�n, y la autorizaci�n basada en pol�ticas, ofreciendo un control fino sobre el comportamiento de las aplicaciones.
* Proyectos Grandes y a Largo Plazo: En proyectos con m�ltiples desarrolladores o que se espera que crezcan en complejidad, el uso de controladores ayuda a mantener el c�digo organizado y facilita la colaboraci�n.

#### Para utilizar controladores en ASP.NET Core:

1. Localiza y selecciona el directorio **Controllers** dentro de la estructura del proyecto. Si a�n no existe, cr�alo.
2. Dentro del directorio **Controllers**, crea un nuevo archivo. El nombre del archivo debe reflejar la funcionalidad del controlador, siguiendo la convenci�n de nomenclatura **NombreFuncionalidadController.cs**,
   donde NombreFuncionalidad es el prop�sito del controlador (por ejemplo, **ProductosController.cs**).
3. Abre el nuevo archivo y define la clase del controlador. Para que sea reconocido como un controlador, debe heredar de **ControllerBase** y decorarse con el atributo **[ApiController]**.
   El atributo **[Route]** sirve para definir c�mo se acceder� a este controlador a trav�s de la URL.

Ejemplo b�sico de un controlador: 

```c#
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAspNetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            return Ok($"Hello, {name}!");
        }
    }
}
```

Esto establecer� el endpoint en **/api/HelloWorld/{nombre}**, al cual puedes acceder proporcionando un nombre directamente en la URL, como **/api/HelloWorld/John**, lo que resultar�a en un saludo "Hello, John!".

Se puede conseguir el mismo resultado a trav�s de parametros de consulta.

```c#
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAspNetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloWorldController : ControllerBase
    {
      [HttpGet]
      public IActionResult Get([FromQuery] string name)
      {
          return Ok($"Hello, {name}!");
      }
    }
}
```
Para acceder a este endpoint, a�adir�as el nombre como un par�metro de consulta en la URL, como **/api/HelloWorld?nombre=John**, lo que tambi�n te devolver�a el saludo "Hello, John!".

> [!NOTE]
> * **[ApiController]**: Este atributo se aplica a la clase del controlador para habilitar caracter�sticas comunes de las APIs, como el manejo autom�tico de errores y la vinculaci�n de modelos.
> * **[Route]**: Define la ruta de acceso al controlador. Puedes especificar rutas a nivel de controlador y acci�n. Las rutas pueden incluir par�metros, como **[Route("api/[controller]")]**,
>   donde **[controller]** es un marcador de posici�n que se reemplaza por el nombre del controlador.
>   Dentro del controlador, defines m�todos para diferentes operaciones. Cada uno de estos m�todos se asocia con un verbo HTTP espec�fico mediante atributos como **[HttpGet]**, **[HttpPost]**, **[HttpPut]**, **[HttpDelete]**.

</br>




## Ejecuci�n con Docker

Este proyecto contiene una aplicaci�n ASP.NET Core que se puede ejecutar dentro de un contenedor Docker. A continuaci�n, se presentan los pasos para construir la imagen Docker de la aplicaci�n y ejecutarla en un contenedor.

### Requisitos Previos

Aseg�rate de tener Docker instalado y configurado en tu m�quina. Para construir y ejecutar un contenedor Docker para tu aplicaci�n ASP.NET Core, sigue estos pasos:

**1. Construcci�n de la Imagen Docker:**
</br>
Para construir la imagen Docker de tu aplicaci�n, navega en la terminal o l�nea de comandos hasta la ra�z del proyecto, donde se encuentra el `Dockerfile`, y ejecuta el siguiente comando:

docker build -t <nombre_imagen>:latest .

</br>

> [!NOTE]
> Reemplaza <*nombre_imagen*> con el nombre espec�fico que desees asignarle a tu imagen Docker.
> Este comando construye una imagen Docker para tu aplicaci�n usando el Dockerfile presente en el directorio actual, ejemplo: 
> ```sh
> docker build -t webappimage:latest .
> ````

**2. Ejecuta el contenedor:**
</br>
Una vez que la imagen est� construida, puedes ejecutarla en un contenedor con:

```sh
docker run -d -p <puerto_host>:<puerto_contenedor> [--name <nombre_contenedor>] <nombre_imagen>:latest
```

> [!NOTE]
> * Reemplaza <*puerto_host*> con el n�mero de puerto en tu m�quina anfitriona a trav�s del cual deseas acceder al servicio.
> Este puerto es el que utilizar�s para conectarte al servicio que se ejecuta dentro del contenedor desde fuera del mismo, como un navegador web o un cliente de API.
> * Sustituye <*puerto_contenedor*> con el n�mero de puerto dentro del contenedor donde tu aplicaci�n o servicio est� escuchando.
> * Es importante que este puerto coincida con el puerto que tu aplicaci�n dentro del contenedor utiliza para escuchar solicitudes entrantes.
> Opcionalmente, agrega --name <*nombre_contenedor*> para asignar un nombre �nico a tu contenedor. Si decides no asignar un nombre expl�citamente, Docker generar� uno autom�ticamente.
> Finalmente, especifica <nombre_imagen>:latest con el nombre de la imagen Docker que especificaste anteriormente.
> Ejemplo: 
> ```sh
> docker run -d -p 8080:8080 --name webappcontainer webappimage:latest 
> ```
> Si prefieres no asignarle un nombre expl�cito:
> ```sh
> docker run -d -p 8080:8080 webappimage:latest 
> ```

> [!IMPORTANT]
> A partir de .NET 8, las aplicaciones ASP.NET Core configuradas en im�genes Docker oficiales escuchan en el puerto **8080** por defecto.
> Si necesitas que tu aplicaci�n est� disponible en un puerto diferente, es esencial ajustar la configuraci�n para reflejar este cambio.
> Esto se logra estableciendo la variable de entorno **ASPNETCORE_HTTP_PORTS** con el valor del puerto deseado y exponer dicho puerto. A continuaci�n, se muestra c�mo se deber�a ver el **Dockerfile**:
> ```Dockerfile
> FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
> USER app
> WORKDIR /app
> ENV ASPNETCORE_HTTP_PORTS=8085
> EXPOSE 8085
> # (Contin�a con el resto del Dockerfile...)
> ````
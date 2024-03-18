# WebApplication con Asp.Net Core
Este proyecto es una aplicación web desarrollada con ASP.NET Core, un framework para construir aplicaciones web y servicios con .NET. ASP.NET Core es ligero, de alto rendimiento y permite el desarrollo de aplicaciones en patrones de diseño MVC (Model-View-Controller) y Razor Pages para facilitar el desarrollo de interfaces de usuario dinámicas.

ASP.NET Core es adecuado tanto para el desarrollo de páginas web con Razor Pages, que promueve un patrón de diseño basado en la productividad y la simplicidad, como para la creación de servicios web y APIs RESTful utilizando el enfoque de controladores de API, sobre este ultimo nos centraremos en este proyecto.

## Cómo agregar controladores de API

### Uso de Minimal APIs

Una manera sencilla de implementar funcionalidades como un "Hello World!"  es utilizando Minimal APIs. Este enfoque está diseñado para crear HTTP APIs con mínimo esfuerzo y código, haciéndolo ideal para microservicios y aplicaciones pequeñas.
Las Minimal APIs reducen la necesidad de boilerplate (código repetitivo) al definir rutas y manejar solicitudes, permitiendo una sintaxis más limpia y directa.

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

Este código configura una aplicación web que escucha en la raíz del sitio (/) y devuelve "Hello World!" cuando se accede a ella.
</br>
El segundo endpoint **/add/{arg1}/{arg2}** está diseñado para recibir dos argumentos numéricos (arg1 y arg2) directamente de la URL, sumarlos, y devolver el resultado en formato de texto. 

### Uso de Controladores

Existe otra forma de organizar y manejar solicitudes HTTP en tu aplicación es mediante el uso de controladores en la carpeta **Controllers**.
Esta es una práctica común en aplicaciones ASP.NET Core MVC o en aplicaciones que implementan APIs RESTful.
</br>
Los controladores ofrecen una estructura más formalizada para manejar solicitudes HTTP, agrupando lógicas de operaciones similares en clases dedicadas.
Cada controlador puede contener múltiples acciones, que son métodos que responden a diferentes rutas o tipos de solicitudes HTTP (como **GET, POST, PUT, DELETE**, etc.).

#### Enfoques y Ventajas
* El uso de controladores es particularmente beneficioso en los siguientes escenarios:
* Aplicaciones con Lógica de Negocio Compleja: Los controladores permiten una separación clara entre la lógica de presentación y la lógica de negocio, facilitando el mantenimiento y la escalabilidad de la aplicación.
* APIs RESTful: Para proyectos que implementan APIs RESTful, el uso de controladores es un enfoque estándar que permite una organización clara de endpoints, apoyando principios REST como el uso de diferentes métodos HTTP para representar operaciones CRUD.
* Integración con Tecnologías de ASP.NET Core: Los controladores se integran estrechamente con otras características de ASP.NET Core, como el enlace de modelos, validación, y la autorización basada en políticas, ofreciendo un control fino sobre el comportamiento de las aplicaciones.
* Proyectos Grandes y a Largo Plazo: En proyectos con múltiples desarrolladores o que se espera que crezcan en complejidad, el uso de controladores ayuda a mantener el código organizado y facilita la colaboración.

#### Para utilizar controladores en ASP.NET Core:

1. Localiza y selecciona el directorio **Controllers** dentro de la estructura del proyecto. Si aún no existe, créalo.
2. Dentro del directorio **Controllers**, crea un nuevo archivo. El nombre del archivo debe reflejar la funcionalidad del controlador, siguiendo la convención de nomenclatura **NombreFuncionalidadController.cs**,
   donde NombreFuncionalidad es el propósito del controlador (por ejemplo, **ProductosController.cs**).
3. Abre el nuevo archivo y define la clase del controlador. Para que sea reconocido como un controlador, debe heredar de **ControllerBase** y decorarse con el atributo **[ApiController]**.
   El atributo **[Route]** sirve para definir cómo se accederá a este controlador a través de la URL.

Ejemplo básico de un controlador: 

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

Esto establecerá el endpoint en **/api/HelloWorld/{nombre}**, al cual puedes acceder proporcionando un nombre directamente en la URL, como **/api/HelloWorld/John**, lo que resultaría en un saludo "Hello, John!".

Se puede conseguir el mismo resultado a través de parametros de consulta.

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
Para acceder a este endpoint, añadirías el nombre como un parámetro de consulta en la URL, como **/api/HelloWorld?nombre=John**, lo que también te devolvería el saludo "Hello, John!".

> [!NOTE]
> * **[ApiController]**: Este atributo se aplica a la clase del controlador para habilitar características comunes de las APIs, como el manejo automático de errores y la vinculación de modelos.
> * **[Route]**: Define la ruta de acceso al controlador. Puedes especificar rutas a nivel de controlador y acción. Las rutas pueden incluir parámetros, como **[Route("api/[controller]")]**,
>   donde **[controller]** es un marcador de posición que se reemplaza por el nombre del controlador.
>   Dentro del controlador, defines métodos para diferentes operaciones. Cada uno de estos métodos se asocia con un verbo HTTP específico mediante atributos como **[HttpGet]**, **[HttpPost]**, **[HttpPut]**, **[HttpDelete]**.

</br>




## Ejecución con Docker

Este proyecto contiene una aplicación ASP.NET Core que se puede ejecutar dentro de un contenedor Docker. A continuación, se presentan los pasos para construir la imagen Docker de la aplicación y ejecutarla en un contenedor.

### Requisitos Previos

Asegúrate de tener Docker instalado y configurado en tu máquina. Para construir y ejecutar un contenedor Docker para tu aplicación ASP.NET Core, sigue estos pasos:

**1. Construcción de la Imagen Docker:**
</br>
Para construir la imagen Docker de tu aplicación, navega en la terminal o línea de comandos hasta la raíz del proyecto, donde se encuentra el `Dockerfile`, y ejecuta el siguiente comando:

docker build -t <nombre_imagen>:latest .

</br>

> [!NOTE]
> Reemplaza <*nombre_imagen*> con el nombre específico que desees asignarle a tu imagen Docker.
> Este comando construye una imagen Docker para tu aplicación usando el Dockerfile presente en el directorio actual, ejemplo: 
> ```sh
> docker build -t webappimage:latest .
> ````

**2. Ejecuta el contenedor:**
</br>
Una vez que la imagen esté construida, puedes ejecutarla en un contenedor con:

```sh
docker run -d -p <puerto_host>:<puerto_contenedor> [--name <nombre_contenedor>] <nombre_imagen>:latest
```

> [!NOTE]
> * Reemplaza <*puerto_host*> con el número de puerto en tu máquina anfitriona a través del cual deseas acceder al servicio.
> Este puerto es el que utilizarás para conectarte al servicio que se ejecuta dentro del contenedor desde fuera del mismo, como un navegador web o un cliente de API.
> * Sustituye <*puerto_contenedor*> con el número de puerto dentro del contenedor donde tu aplicación o servicio está escuchando.
> * Es importante que este puerto coincida con el puerto que tu aplicación dentro del contenedor utiliza para escuchar solicitudes entrantes.
> Opcionalmente, agrega --name <*nombre_contenedor*> para asignar un nombre único a tu contenedor. Si decides no asignar un nombre explícitamente, Docker generará uno automáticamente.
> Finalmente, especifica <nombre_imagen>:latest con el nombre de la imagen Docker que especificaste anteriormente.
> Ejemplo: 
> ```sh
> docker run -d -p 8080:8080 --name webappcontainer webappimage:latest 
> ```
> Si prefieres no asignarle un nombre explícito:
> ```sh
> docker run -d -p 8080:8080 webappimage:latest 
> ```

> [!IMPORTANT]
> A partir de .NET 8, las aplicaciones ASP.NET Core configuradas en imágenes Docker oficiales escuchan en el puerto **8080** por defecto.
> Si necesitas que tu aplicación esté disponible en un puerto diferente, es esencial ajustar la configuración para reflejar este cambio.
> Esto se logra estableciendo la variable de entorno **ASPNETCORE_HTTP_PORTS** con el valor del puerto deseado y exponer dicho puerto. A continuación, se muestra cómo se debería ver el **Dockerfile**:
> ```Dockerfile
> FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
> USER app
> WORKDIR /app
> ENV ASPNETCORE_HTTP_PORTS=8085
> EXPOSE 8085
> # (Continúa con el resto del Dockerfile...)
> ````
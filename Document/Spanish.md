# ¿Qué es RipeConsole?

RipeConsole es una herramienta de consola diseñada para facilitar a los usuarios la Depuración, Prueba y Ejecución de su código JavaScript. Permite clasificar archivos en Categorías, Grupos, Subgrupos y Funciones para su selección desde su Menú. Utiliza el motor V8Script para esto, y ZCore, una Librería hecha también por mi (sin eso el Programa no funcionaría, la mayoría de las Dependencias y TypeDefinitions están ahí)

El proyecto es de código abierto, lo que significa que puedes tomar lo que necesites de él cuando quieras.

## Soporte

La herramienta soporta los siguientes sistemas operativos, estoy planeando añadir soporte móvil y también hacer una versión GUI para cada plataforma:

- `Windows x64+`
- `Linux x64+`
- `Macintosh x64`

## Instalación

Para instalar el programa no es necesario realizar ningún paso complejo, basta con hacer lo siguiente:

1. Descarga VisualStudio (si deseas un mayor rendimiento puedes usar VisualStudio Code, no hay problema) y la última versión del .NET Framework (RipeConsole utiliza la versión 8.0 por razones de optimización).

2. Una vez descargado e instalado VS, abre este repositorio (tendrás que descargarlo en tu dispositivo).

3. Una vez abierto puedes compilar el proyecto de la siguiente forma: si estás en VisualStudio haz click en `Ejecutar/Ejecutar y depurar`, si estás en VS Code debes abrir un terminal e introducir el comando `dotnen run` para iniciar la compilación del proyecto. Con esto ya deberías tener el ejecutable a mano, por defecto se genera en «Compiled», dentro de la misma carpeta del proyecto (si lo deseas, puedes cambiar la ruta en el archivo .csproj).

4. También puedes descargar estos assets que hice, sirven para manejar diferentes tipos de archivos: [aquí](https://github.com/RaFranVJ/RipeUtils)

Una vez finalizada la descarga, extrae el contenido del archivo y cópialo en el Proyecto Compilado, la Herramienta cargará estos Assets en tiempo de ejecución.

## Uso

El uso del programa es sencillo, permite cargar Scripts de JavaScript puro o con código inyectado (tipos de clases C# que se definen en la Entrada de una función junto con otros parámetros para personalizar la carga y compilación del código JS antes de ejecutarlo). Por defecto el programa genera plantillas con código genérico de las clases serializables que funcionan para hacer esto posible, a continuación una breve descripción de cada una de ellas:

- GroupCategory: representa una categoría de grupos, cada grupo se carga desde la misma carpeta. Se pueden personalizar ciertos parámetros como su ID, nombre o ruta a los grupos que se necesitan cargar desde ella (los jsons deben existir en esa carpeta y deben coincidir con la estructura de la clase FuncGroup que voy a definir a continuación

- FuncGroup: representa un grupo de funciones u otros grupos. Cada función/grupo se carga desde una única carpeta. Puedes personalizar parámetros como ID, Nombre y la Ruta a SubGrupos/Funciones, ten en cuenta que el grupo no puede cargar funciones y subgrupos simultáneamente, tienes que especificar uno de los dos.

- Function: representa una función JavaScript. Las funciones pueden filtrarse por determinados criterios (como por extensión o por nombre de archivo al seleccionarlas en el menú). Se puede personalizar el ID o el nombre de la función, se debe asignar una ruta a la entrada del Script para identificar correctamente los parámetros necesarios. Si la función requiere argumentos como rutas de E/S o el nombre de un método a llamar en tiempo de ejecución, tendrás que establecerlos en el json (los args deben estar en el mismo orden en que se pasan en el método del Script)

- JSCriptEntry: representa un punto de acceso para un archivo JS. En él puedes definir cómo debe compilarse el documento, entre otras funciones como su nombre o la ruta al fichero en cuestión. Si quieres inyectar código al Script debes mapear las variables que representan los tipos C# junto con el nombre que tienen en el código JS, en un Diccionario llamado TypesToExpose (o NamedItems para instancias de objetos), ten en cuenta que los tipos C# deben estar declarados como dependencias en la App (como Bibliotecas u otros Proyectos) para que la Inyección funcione correctamente.

- Archivo JS: es el archivo JavaScript en cuestión, puede ser una expresión directa como `2 + 1`, por ejemplo, o un conjunto de expresiones que se pueden separar en funciones. Estas funciones se llaman desde C# por su nombre y pueden o no tomar argumentos (del tipo directo o accediendo a propiedades de la clase UserParams).

Todas estas clases son cargadas en la ejecución del programa y pueden ser seleccionadas a través del teclado en el menú (ingresa un número o usa ARRIBA o ABAJO para navegar por el menú, luego presiona ENTER, puedes cambiar los Keybinds en el archivo de Configuración). UserParams es usado para especificar los argumentos que los métodos requieren, cámbielos en el JSON o a través del menú de la App).

## Agradecimientos

- [TwinStar](https://github.com/twinkles-twinstar/): parte de su código fue tomado de [TwinStar.ToolKit](https://github.com/twinkles-twinstar/TwinStar.ToolKit) y traducido a C#.

- [Haruma](https://github.com/Haruma-VN/): me ayudó mucho gracias a su proyecto [Sen](https://github.com/Haruma-VN/Sen) y sus opiniones en discord. 

- [YingFengTingYu](https://github.com/YingFengTingYu/): empecé a implementar parte de su código de [PopStudio](https://github.com/YingFengTingYu/PopStudio_Old) y a darle mejoras en mi aplicación.
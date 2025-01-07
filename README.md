## Cátedra 3 - UCN 
Para este repositorio es necesario tener instalado:
  1. .NET SDK (8.0.203 mínimo). 
  2. Una IDE como Visual Studio Code.
  3. Node.js (22.11.0 mínimo)

Alumno:

  Nicolás Patricio Tapia Carrasco

Profesor: 

  Jorge Rivera Mancilla
    
Ayudantes: 

  1. Guillermo Fuentes Ávila
  2. Ernes Fuenzalida Tello
    
## Instalación
1. Clona el repositorio en tu máquina local.
   ```sh
   git clone <link>
   ```
2. Abre el projecto en tu IDE preferida.
3. Abre el terminal y ejecuta el siguiente comando:
   ```sh
   dotnet restore
   ```
4. Iniciar la base de datos:
   ```sh
   dotnet ef database update
   ``` 
5. Al archivo ".env.example" hazle una copia y elimina ".example" del nombre.
6. Dentro del archivo ".env" se encontrará la url de Cloudinary, cambia los valores de <API_KEY>,<API_SECRET> y <CLOUD_NAME> por los de tu cuenta personal de Cloudinary.
7. Corre el proyecto:
   ```sh
   dotnet run
   ```

# Azure Service Bus Emulator - .NET Pub/Sub Pattern

Esta solución implementa el patrón de mensajería **Publish-Subscriber (Pub/Sub)** utilizando el emulador oficial de **Azure Service Bus** de Microsoft en un contenedor Docker. El objetivo es simular un entorno productivo de mensajería de forma local para el envío y recepción de mensajes.

## 🚀 Características
- **Infraestructura:** Uso de la imagen oficial del emulador de Azure Service Bus.
- **Publisher:** Una Web API en .NET que envía mensajes a una cola/tópico.
- **Subscriber:** Un Worker Service en .NET que consume y procesa los mensajes de forma asíncrona.
- **Persistencia:** Verificación de mensajes en cola incluso si el consumidor está offline.
- **Integración Externa:** Uso de Webhooks para pruebas de respuesta.

## 📋 Requisitos Previos
Antes de comenzar, asegúrate de tener instalado:
- [.NET SDK](https://dotnet.microsoft.com/download) (versión 8.0).
- [Docker Desktop](https://www.docker.com/products/docker-desktop/).
- Un cliente Git.

## 🛠️ Configuración e Instalación

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/iambenjamin-developer/AzureServiceBusEmulator.git
   cd AzureServiceBusEmulator
   ```

2. **Levantar el emulador de Azure Service Bus:**
   El proyecto incluye los archivos `Config.json` (configuración del emulador) y `docker-compose.yml`. Ejecuta el siguiente comando para descargar la imagen e iniciar el servicio:
   ```bash
   docker compose up -d
   ```

## 🏗️ Estructura del Proyecto

La solución se divide en dos proyectos principales:

1.  **Publisher (API):** Punto de entrada que expone un endpoint para enviar mensajes a la cola.
2.  **Subscriber (Worker Service):** Servicio en segundo plano que escucha la cola, procesa los mensajes y los marca como completados.

## 🚦 Cómo Probar la Solución

### Opción 1: Procesamiento en tiempo real
Ejecuta ambos proyectos simultáneamente desde tu IDE (Visual Studio / VS Code). Al enviar un mensaje a través de la API, verás de inmediato cómo el Worker Service lo recibe y lo procesa.

### Opción 2: Prueba de persistencia (Cola de mensajes)
Esta prueba demuestra que los mensajes no se pierden si el consumidor no está disponible:
1. Ejecuta únicamente el proyecto **API (Publisher)**.
2. Envía uno o varios mensajes a través del endpoint de prueba.
3. Detén el proyecto API.
4. Ejecuta el proyecto **Worker Service (Subscriber)**.
5. Observarás que el Worker recupera y procesa todos los mensajes que quedaron acumulados en la cola del emulador.

## 🔗 Webhook de Prueba
Para verificar las respuestas o integraciones externas, se utiliza un **Webhook** gratuito. 
> [!IMPORTANT]
> Puedes cambiar la URL del webhook en los archivos de configuración (`appsettings.json`) del proyecto para apuntar a tu propio endpoint de [Webhook.site](https://webhook.site) o cualquier otra herramienta similar.

## ⚙️ Archivos Clave
- `docker-compose.yml`: Define el contenedor del emulador.
- `Config.json`: Define los Namespaces, Queues y Topics que el emulador creará al iniciar.


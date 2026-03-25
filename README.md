# 📬 NotificationAPI

API de notificações construída com **ASP.NET Core**, aplicando princípios **SOLID**, **Injeção de Dependência**, **Strategy Pattern** e **Factory Pattern**.

Projeto criado com foco em escalabilidade — adicionar um novo canal de notificação não requer nenhuma alteração nos serviços existentes.

---

## 🚀 Tecnologias

- .NET 8
- ASP.NET Core Web API
- Swagger / OpenAPI
- xUnit (testes unitários)

---

## 📁 Estrutura do Projeto

```
NotificationAPI/
├── Controllers/
│   └── NotificationController.cs     # Recebe as requisições HTTP
├── Interfaces/
│   └── INotificationChannel.cs       # Contrato que todos os canais devem seguir
├── Services/
│   ├── EmailNotificationChannel.cs   # Implementação do canal de e-mail
│   ├── SmsNotificationChannel.cs     # Implementação do canal de SMS
│   └── NotificationService.cs        # Orquestra o envio
├── Factories/
│   └── NotificationChannelFactory.cs # Decide qual canal usar
├── Models/
│   └── Notification.cs               # NotificationRequest e NotificationResponse
└── Program.cs                        # Configuração do container de DI

NotificationAPI.Tests/
├── Fakes/
│   └── FakeNotificationChannel.cs    # Canal falso para testes
└── NotificationServiceTests.cs       # Testes unitários
```

---

## 🧠 Conceitos Aplicados

### Dependency Inversion Principle (SOLID)

O `NotificationService` não depende de `EmailNotificationChannel` nem de `SmsNotificationChannel` diretamente. Ele depende apenas da abstração `INotificationChannel`.

```csharp
// O serviço só conhece a interface — não sabe se é Email, SMS, ou qualquer outro
public class NotificationService
{
    private readonly NotificationChannelFactory _factory;

    public NotificationService(NotificationChannelFactory factory)
    {
        _factory = factory;
    }
}
```

Isso significa que adicionar um novo canal (`WhatsApp`, `Push`, etc.) não requer nenhuma mudança no `NotificationService`.

---

### Injeção de Dependência

As dependências não são criadas manualmente com `new`. O container de DI do ASP.NET as resolve automaticamente com base nos registros do `Program.cs`.

```csharp
builder.Services.AddScoped<INotificationChannel, EmailNotificationChannel>();
builder.Services.AddScoped<INotificationChannel, SmsNotificationChannel>();
builder.Services.AddScoped<NotificationChannelFactory>();
builder.Services.AddScoped<NotificationService>();
```

O container monta toda a cadeia de dependências automaticamente ao receber uma requisição.

---

### Strategy Pattern

Cada canal de notificação é uma estratégia intercambiável que segue o mesmo contrato:

```csharp
public interface INotificationChannel
{
    string ChannelName { get; }
    Task<NotificationResponse> SendAsync(string recipient, string message);
}
```

O canal correto é selecionado em tempo de execução com base no campo `channel` da requisição.

---

### Factory Pattern

A `NotificationChannelFactory` é responsável por entregar o canal correto. O `NotificationService` não precisa saber como essa decisão é tomada.

```csharp
public INotificationChannel Get(string channelName)
{
    if (!_channels.TryGetValue(channelName.ToLower(), out var channel))
        throw new ArgumentException($"Canal '{channelName}' não encontrado.");

    return channel;
}
```

---

### Task e Programação Assíncrona

Todos os métodos de envio são assíncronos, seguindo as boas práticas do .NET:

- Métodos assíncronos têm o sufixo `Async`
- `Task<T>` é usado quando há retorno
- `Task.CompletedTask` é usado para finalizar a Task
- `async/await` é propagado por todas as camadas

---

## 📡 Endpoint

### `POST /api/notification/send`

Envia uma notificação pelo canal especificado.

**Request:**
```json
{
  "recipient": "joao@email.com",
  "message": "Olá, mundo!",
  "channel": "email"
}
```

**Response:**
```json
{
  "success": true,
  "message": "[Email] Para: joao@email.com | Mensagem: Olá, mundo!",
  "sentAt": "2024-03-25T10:00:00"
}
```

**Canais disponíveis:** `email`, `sms`

---

## ▶️ Como rodar

```bash
# Clonar o repositório
git clone https://github.com/seu-usuario/NotificationAPI.git

# Entrar na pasta
cd NotificationAPI

# Confiar no certificado SSL local (apenas na primeira vez)
dotnet dev-certs https --trust

# Rodar a aplicação
dotnet run --project NotificationAPI
```

Acesse a documentação interativa em:
```
https://localhost:{porta}/swagger
```

---

## 🧪 Testes

Os testes cobrem o `NotificationService` de forma isolada, usando um canal falso (`FakeNotificationChannel`) que não realiza envios reais.

```bash
dotnet test
```

**Casos cobertos:**
- Deve enviar pelo canal correto quando o canal existe
- Deve lançar exceção quando o canal não existe

---

## ➕ Como adicionar um novo canal

1. Criar a classe implementando `INotificationChannel`:

```csharp
public class WhatsAppNotificationChannel : INotificationChannel
{
    public string ChannelName => "whatsapp";

    public Task<NotificationResponse> SendAsync(string recipient, string message)
    {
        await Task.CompletedTask;
        return new NotificationResponse(true, messageResult, DateTime.Now);
    }
}
```

2. Registrar no `Program.cs`:

```csharp
builder.Services.AddScoped<INotificationChannel, WhatsAppNotificationChannel>();
```

Nenhuma outra alteração é necessária.

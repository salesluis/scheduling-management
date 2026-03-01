## Scheduling Management - Documentação de Domínio

### Visão geral

Este projeto modela um sistema de **agendamento de serviços multiestabelecimento**, no qual:

- **`Establishment`** representa o estabelecimento (barbearia, clínica, salão, etc.).
- **`User`** representa a conta de usuário do sistema (login, identidade principal).
- **`Client`** representa o cliente de um estabelecimento.
- **`Professional`** representa o profissional que executa serviços em um estabelecimento.
- **`Service`** representa um tipo de serviço oferecido (ex.: corte, consulta).
- **`Appointment`** representa um agendamento entre cliente, profissional e serviço.
- **`ProfessionalService`** é a associação entre profissional e serviço.

As entidades compartilham uma base comum de auditoria (`BaseEntity`) e, quando multi-tenant, herdando de `TenantEntity` para vincular tudo a um `Establishment`.

---

### Modelo de herança e auditoria

- **`BaseEntity`**
  - **Campos**:
    - `Id`: identificador único (`Guid`), gerado automaticamente.
    - `CreatedAtUtc`: data de criação em UTC.
    - `UpdatedAtUtc`: data da última atualização em UTC.
  - **Comportamento**:
    - `Touch()`: atualiza `UpdatedAtUtc` para a data/hora atual em UTC.
  - **Regra de negócio**:
    - **Auditoria**: qualquer operação de atualização significativa deve chamar `Touch()` para manter o rastro de modificações.

- **`TenantEntity`**
  - **Herda**: `BaseEntity`.
  - **Campo**:
    - `EstablishmentId`: `Guid` do estabelecimento proprietário da entidade.
  - **Regras de negócio**:
    - **Isolamento por tenant**: toda operação de leitura/escrita em entidades que herdam de `TenantEntity` deve ser filtrada por `EstablishmentId`.
    - **Consistência**: entidades relacionadas em um fluxo (por exemplo, um `Appointment` com seu `Client`, `Professional` e `Service`) **devem compartilhar o mesmo `EstablishmentId`**.

---

### Entidades principais

#### `Establishment`

- **Descrição**: representa um estabelecimento que oferece serviços e possui profissionais e clientes.
- **Campos**:
  - `Name`: nome do estabelecimento.
  - `Slug`: identificador amigável, ideal para uso em URL.
  - `TimeZoneId`: ID do fuso horário (padrão `"UTC"`).
  - `IsActive`: indica se o estabelecimento está ativo.
- **Relacionamentos**:
  - `Services`: coleção de `Service`.
  - `Professionals`: coleção de `Professional`.
  - `Clients`: coleção de `Client`.
  - `Appointments`: coleção de `Appointment`.
- **Regras de negócio sugeridas**:
  - **Unicidade de `Slug`**: deve ser único no sistema.
  - **Validação de fuso horário**: `TimeZoneId` deve ser válido, usado para converter horários entre local e UTC.
  - **Estabelecimento inativo**: se `IsActive = false`, bloquear criação de novos `Service`, `Professional`, `Client` e `Appointment` ligados a esse estabelecimento.

#### `User`

- **Descrição**: conta de usuário da aplicação (login), podendo ter perfis de cliente e/ou profissional.
- **Campos**:
  - `Name`: nome do usuário.
  - `Email`: email, potencialmente usado como nome de usuário.
  - `PhoneNumber`: telefone opcional.
- **Relacionamentos**:
  - `ProfessionalProfiles`: coleção de `Professional` ligados a esse `User`.
  - `ClientProfiles`: coleção de `Client` ligados a esse `User`.
- **Regras de negócio sugeridas**:
  - **Unicidade de email**: `Email` deve ser único no sistema.
  - **Perfis múltiplos**: um mesmo `User` pode atuar como `Client` e/ou `Professional` em diferentes estabelecimentos.
  - **Normalização**: armazenar `Email` em caixa baixa e validar formato de email/telefone.

#### `Client`

- **Descrição**: cliente de um determinado `Establishment`.
- **Herda**: `TenantEntity`.
- **Campos**:
  - `UserId`: opcional (`Guid?`), vínculo com um `User` (cliente autenticado).
  - `Name`: nome do cliente.
  - `PhoneNumber`: telefone opcional.
- **Relacionamentos**:
  - `Establishment`: estabelecimento ao qual o cliente pertence.
  - `User`: referência opcional ao `User`.
  - `Appointments`: coleção de `Appointment` do cliente.
- **Regras de negócio sugeridas**:
  - **Cliente autenticado**: a combinação (`UserId`, `EstablishmentId`) deve ser única quando `UserId` não for nulo.
  - **Cliente anônimo**: permitir clientes sem `UserId` para agendamentos rápidos apenas com nome e, opcionalmente, telefone.
  - **Consistência de tenant**: clientes só podem ter `Appointments` no mesmo `EstablishmentId`.

#### `Professional`

- **Descrição**: profissional que presta serviços em um estabelecimento.
- **Herda**: `TenantEntity`.
- **Campos**:
  - `UserId`: `Guid` obrigatório, vínculo com um `User`.
  - `DisplayName`: nome exibido para o cliente.
  - `IsActive`: indica se o profissional está ativo para agendamentos.
- **Relacionamentos**:
  - `Establishment`: estabelecimento do profissional.
  - `User`: conta do usuário associada.
  - `ProfessionalServices`: coleção de `ProfessionalService`.
  - `Appointments`: agendamentos do profissional.
- **Regras de negócio sugeridas**:
  - **Disponibilidade**: se `IsActive = false`, o profissional não deve aparecer como opção ao criar um `Appointment`.
  - **Unicidade por estabelecimento**: a combinação (`UserId`, `EstablishmentId`) deve ser única.
  - **Consistência de tenant**: todos os `ProfessionalServices` e `Appointments` relacionados devem compartilhar o mesmo `EstablishmentId`.

#### `Service`

- **Descrição**: tipo de serviço oferecido por um estabelecimento.
- **Herda**: `TenantEntity`.
- **Campos**:
  - `Name`: nome do serviço.
  - `DurationInMinutes`: duração em minutos.
  - `Price`: preço opcional (`decimal?`).
  - `IsActive`: indica se o serviço está ativo.
- **Relacionamentos**:
  - `Establishment`: estabelecimento que oferece o serviço.
  - `ProfessionalServices`: profissionais que executam o serviço.
  - `Appointments`: agendamentos deste serviço.
- **Regras de negócio sugeridas**:
  - **Validação de duração**: `DurationInMinutes` deve ser maior que zero.
  - **Validação de preço**: se informado, `Price` deve ser maior ou igual a zero.
  - **Serviço inativo**: `IsActive = false` deve impedir novos `Appointment` com esse serviço.
  - **Unicidade**: `Name` deve ser único por `Establishment` (ou seguir algum critério de unicidade local).

#### `ProfessionalService`

- **Descrição**: entidade de junção entre `Professional` e `Service`.
- **Herda**: `TenantEntity`.
- **Campos**:
  - `ProfessionalId`: referência ao profissional.
  - `ServiceId`: referência ao serviço.
- **Relacionamentos**:
  - `Professional`: profissional vinculado.
  - `Service`: serviço vinculado.
- **Regras de negócio sugeridas**:
  - **Consistência de tenant**: `Professional`, `Service` e `ProfessionalService` devem compartilhar o mesmo `EstablishmentId`.
  - **Unicidade de vínculo**: a combinação (`EstablishmentId`, `ProfessionalId`, `ServiceId`) deve ser única.
  - **Pré-requisito para agendamento**: somente permitir `Appointment` se existir um `ProfessionalService` correspondente.

#### `Appointment`

- **Descrição**: agendamento de um serviço para um cliente com um profissional em um estabelecimento.
- **Herda**: `TenantEntity`.
- **Campos**:
  - `ProfessionalId`: profissional responsável.
  - `ServiceId`: serviço selecionado.
  - `ClientId`: cliente.
  - `StartUtc`: data/hora de início em UTC.
  - `EndUtc`: data/hora de fim em UTC.
  - `Status`: `AppointmentStatus` (`Scheduled`, `Completed`, `Cancelled`, `NoShow`).
- **Relacionamentos**:
  - `Professional`: profissional do agendamento.
  - `Service`: serviço agendado.
  - `Client`: cliente que realizou o agendamento.
- **Regras de negócio implementadas**:
  - `EndUtc` deve ser estritamente maior que `StartUtc`, caso contrário é lançada `ArgumentException`.
  - `Cancel()`:
    - Não permite cancelar se `Status == Completed`, caso contrário lança `InvalidOperationException`.
    - Altera `Status` para `Cancelled`.
  - `Complete()`:
    - Só permite completar se `Status == Scheduled`, caso contrário lança `InvalidOperationException`.
    - Altera `Status` para `Completed` e chama `Touch()` para atualizar `UpdatedAtUtc`.
- **Regras de negócio adicionais sugeridas**:
  - **Consistência de tenant**: `Appointment.EstablishmentId` deve coincidir com o de `Professional`, `Service` e `Client`.
  - **Disponibilidade**:
    - Bloquear novos agendamentos se `Establishment.IsActive`, `Professional.IsActive` ou `Service.IsActive` forem falsos.
  - **Conflito de agenda**:
    - Impedir agendamentos com sobreposição de horário para o mesmo `Professional`.
    - Opcionalmente, impedir sobreposição para o mesmo `Client`.
  - **Duração coerente**:
    - Recomenda-se validar se `EndUtc - StartUtc` é igual a `DurationInMinutes` do `Service` (ou múltiplo, se o sistema permitir).
  - **Transição para `NoShow`**:
    - Definir política (por exemplo, job que marca como `NoShow` após `EndUtc` caso ainda esteja `Scheduled`).

---

### Enum `AppointmentStatus`

- **Valores**:
  - `Scheduled` (1)
  - `Completed` (2)
  - `Cancelled` (3)
  - `NoShow` (4)
- **Fluxo recomendado**:
  - **Criação**: `Scheduled`.
  - **Cancelamento**: `Scheduled` → `Cancelled`.
  - **Conclusão**: `Scheduled` → `Completed`.
  - **Falta**: `Scheduled` → `NoShow` (regra externa, ex.: job).

---

### Relacionamentos principais (visão resumida)

- **`Establishment` 1 — N `Service`**
- **`Establishment` 1 — N `Professional`**
- **`Establishment` 1 — N `Client`**
- **`Establishment` 1 — N `Appointment`**
- **`User` 1 — N `Professional`**
- **`User` 1 — N `Client`**
- **`Professional` N — N `Service`** via `ProfessionalService`
- **`Client` 1 — N `Appointment`**
- **`Professional` 1 — N `Appointment`**
- **`Service` 1 — N `Appointment`**

---

### Regras de negócio consolidadas

- **Multi-tenancy**
  - **Filtro obrigatório por `EstablishmentId`** em todas as consultas/commands de entidades que herdam `TenantEntity`.
  - **Consistência de tenant** em qualquer relacionamento entre `Client`, `Professional`, `Service`, `ProfessionalService` e `Appointment`.

- **Agendamentos**
  - Horários sempre armazenados em UTC (`StartUtc`/`EndUtc`).
  - Conversão de/para horário local baseada em `Establishment.TimeZoneId`.
  - Proibir sobreposição de agendamentos para o mesmo profissional.
  - Respeitar flags de `IsActive` em `Establishment`, `Professional` e `Service`.

- **Identidade e unicidade**
  - `User.Email` único no sistema.
  - `Establishment.Slug` único no sistema.
  - Combinações sugeridas:
    - (`UserId`, `EstablishmentId`) em `Professional`.
    - (`UserId`, `EstablishmentId`) em `Client` (quando `UserId` não for nulo).
    - (`EstablishmentId`, `ProfessionalId`, `ServiceId`) em `ProfessionalService`.

---

### Próximos passos sugeridos

- **Validações de domínio**: encapsular as regras descritas acima em métodos/fábricas de domínio e/ou services de aplicação.
- **Camada de persistência**: configurar restrições de unicidade e chaves estrangeiras (ex.: via EF Core) para garantir a consistência de dados.
- **Documentação complementar**: adicionar diagramas UML (classes/relacionamentos) e fluxos de estado dos agendamentos conforme o domínio evoluir.


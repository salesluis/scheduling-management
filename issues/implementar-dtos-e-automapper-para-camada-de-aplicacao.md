## Implementar DTOs e AutoMapper para Camada de Aplicação

### Descrição
Esta tarefa envolve a implementação de Data Transfer Objects (DTOs) e a configuração do AutoMapper na camada de aplicação. O objetivo é garantir que a comunicação entre as diferentes camadas da aplicação seja feita de forma eficiente e organizada, utilizando DTOs para transferir dados.

### Critérios de Aceitação
- [ ] DTOs devem ser implementados para todos os modelos de domínio.
- [ ] AutoMapper deve ser configurado para mapear corretamente entre os modelos de domínio e os DTOs.
- [ ] Testes devem ser escritos para validar o mapeamento realizado pelo AutoMapper.

### Exemplos de Implementação

#### DTOs
```csharp
public class UserDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

#### AutoMapper Profiles
```csharp
public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<User, UserDto>();
    }
}
```

#### Testes
```csharp
[Test]
public void Should_Map_User_To_UserDto() {
    var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
    var userDto = _mapper.Map<UserDto>(user);
    Assert.AreEqual(user.Id, userDto.Id);
    Assert.AreEqual(user.Name, userDto.Name);
    Assert.AreEqual(user.Email, userDto.Email);
}
```

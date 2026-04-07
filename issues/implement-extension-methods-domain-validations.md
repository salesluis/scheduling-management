# Implementar Extension Methods para Validações de Domínio

## Descrição
Este issue propõe a implementação de métodos de extensão para a classe `DateTime`, focando em validações de domínio. Os métodos devem permitir verificar se uma data está dentro de um intervalo específico ou se uma data é válida de acordo com regras de negócio.

## Exemplos de Código

### Método de Extensão: IsValidDateRange
```csharp
public static class DateTimeExtensions
{
    public static bool IsValidDateRange(this DateTime date, DateTime startDate, DateTime endDate)
    {
        return date >= startDate && date <= endDate;
    }
}
```

### Uso do Método
```csharp
var dateToCheck = new DateTime(2023, 5, 1);
var startDate = new DateTime(2023, 1, 1);
var endDate = new DateTime(2023, 12, 31);
bool isValid = dateToCheck.IsValidDateRange(startDate, endDate);
// isValid será true
```

## Critérios de Aceitação
- [ ] O método `IsValidDateRange` deve ser implementado e testado.
- [ ] Deve haver um teste unitário que verifica diferentes casos de uso.
- [ ] O método deve estar documentado.

## Casos de Teste
- Testar uma data válida dentro do intervalo.
- Testar uma data fora do intervalo.
- Testar o limite do intervalo (data igual ao início e fim do intervalo).
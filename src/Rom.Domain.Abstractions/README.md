# Rom.Domain.Abstractions

A .NET Standard 2.0 library to help you create cleaner and more robust domain models by handling validation in a smart way.  
It combines the power of C# DataAnnotations with your own custom validation rules, providing a single place to manage and check all domain-related validations.

## Features

- Centralized domain validation logic
- Support for C# DataAnnotations
- Custom validation rules integration
- Clean, maintainable domain models

## Installation

>#### .NET CLI
```bash
dotnet add package Rom.Domain.Abstractions
```
>#### Package Manager
```bash
Install-Package Rom.Domain.Abstractions
```

## Usage

Implement the `IRomValidatableDomain` interface in your domain models to enable validation:


You can use both DataAnnotations and your own custom validation logic within your domain models.

## Data Transfer Objects

The package includes types like `ValidationErrorDataTransfer` to help you communicate validation errors in a structured way.

## License

MIT

## Contribution

Contributions are welcome! Please feel free to submit a Pull Request.

## License

MIT

## Author

Romulo Ribeiro

## Repository

[GitHub Repository](https://github.com/romuloar/rom.domain/tree/main/src/Rom.Domain.Abstractions)



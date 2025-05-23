# 🧠 RomBaseDomain – Your Validatable Domain Base Class

Welcome, dev! 👋  
This package helps you create cleaner and more robust domain models by handling validation in a smart way. It combines the power of C# `DataAnnotations` with your own custom validation rules. The result? A single place to manage and check all domain-related validations. No more clutter. 🚀

---

## 💡 What’s This For?

`RomBaseDomain` is an abstract base class designed for your domain models. By inheriting from it, you automatically get:

- ✅ Validation using `[Required]`, `[Range]`, and other DataAnnotations
- 🧠 Support for custom validation logic
- 📋 Friendly, serialized validation error lists for frontends or APIs
- 🛡️ Internal handling that won’t expose methods or internal validation when converting to JSON

It’s built with a naming convention:  
- Prefixes like `List` for anything that's a list  
- Suffix `Domain` for domain models  
- Suffix `DataTransfer` for DTOs  

---

## 🚀 How To Use It

### 1. Inherit From `RomBaseDomain`

You define your domain model like this:

```csharp
public class UserDomain : RomBaseDomain
{
    [Required]
    public string Name { get; set; }

    [Range(18, 120)]
    public int Age { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [StringLength(12, MinimumLength = 6)]
    public string Username { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    [Url]
    public string Website { get; set; }

    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    public string Password { get; set; }
}
```

### 2. Validate The Entity

```csharp
var user = new UserDomain
{
    Name = "",                 // Required field is empty
    Age = 15,                  // Too young!
    Email = "not-an-email",   // Invalid format
    Username = "usr",         // Too short
    PhoneNumber = "abc",      // Invalid phone
    Website = "not-a-url",    // Invalid URL
    Password = "123456",
    ConfirmPassword = "654321" // Doesn’t match
};

bool isValid = user.IsValidDomain; // false

var errors = user.ListValidationError;

foreach (var error in errors)
{
    Console.WriteLine($"Field: {error.Field} | Message: {error.Message}");
}
```

This will output all the field-level and global errors in a nice, serializable format.

### 3. Custom Validation
You can also add your own validation logic by overriding the `Validate` method:
```csharp
protected override void Validate()
{
    if (string.IsNullOrEmpty(Username))
    {
        AddValidationError("Username", "Username cannot be empty.");
    }
    if (Password != ConfirmPassword)
    {
        AddValidationError("ConfirmPassword", "Passwords do not match.");
    }
}
```
This will add custom validation errors to the list, which can be serialized and returned to the client.
### 4. Serialization
The validation errors are serialized in a way that’s easy to consume by frontends or APIs. You can use them directly in your response models.
```csharp
public class ApiResponse
{
    public bool Success { get; set; }
    public List<ValidationError> Errors { get; set; }
}
public class ValidationError
{
    public string Field { get; set; }
    public string Message { get; set; }
}
```
### 5. Example API Response
```json
{
    "Success": false,
    "Errors": [
        {
            "Field": "Name",
            "Message": "The Name field is required."
        },
        {
            "Field": "Age",
            "Message": "The field Age must be between 18 and 120."
        },
        {
            "Field": "Email",
            "Message": "The Email field is not a valid e-mail address."
        },
        {
            "Field": "Username",
            "Message": "The field Username must be a string or array type with a minimum length of '6'."
        },
        {
            "Field": "PhoneNumber",
            "Message": "The PhoneNumber field is not a valid phone number."
        },
        {
            "Field": "Website",
            "Message": "The Website field is not a valid fully-qualified http, https, or ftp URL."
        },
        {
            "Field": "",
            "Message": "'ConfirmPassword' and 'Password' do not match."
        }
    ]
}
```

This response can be easily consumed by any frontend framework or API client.
### 6. Notes
- The `ListValidationError` property is a list of all validation errors, both field-level and global.
- The `IsValidDomain` property returns `true` if there are no validation errors, and `false` otherwise.
- The `Validate` method is called automatically when you access the `IsValidDomain` property, so you don’t need to call it manually.
- The `AddValidationError` method is used to add custom validation errors to the list. You can use it to add any validation errors that are not covered by the DataAnnotations.
- The `ValidationError` class is used to represent a validation error. It has two properties: `Field` and `Message`. The `Field` property is the name of the field that caused the validation error, and the `Message` property is the error message.
- The `ValidationError` class is serializable, so you can use it directly in your API responses.

### 7. Conclusion
`RomBaseDomain` is a powerful tool for managing domain validation in your C# applications. By using this package, you can ensure that your domain models are clean, maintainable, and easy to validate. It’s a great way to keep your code organized and reduce the risk of validation errors in your application.

## 📦 Installation
You can install the `RomBaseDomain` package via NuGet:
```bash
dotnet add package RomBaseDomain
```
Or by using the NuGet Package Manager in Visual Studio.
```bash
Install-Package RomBaseDomain
```

## 🛠️ Contributing
We welcome contributions! If you have suggestions, bug reports, or feature requests, please open an issue or submit a pull request.

## 📄 License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
```

## 📝 Changelog
See the [CHANGELOG](CHANGELOG.md) file for a detailed list of changes and updates.

## Author
Romulo Ribeiro
    


# Agentic Coding Guidelines for SkillSmpQuery

This document defines the technical standards, workflows, and constraints for AI agents working on the SkillSmpQuery repository.

## 1. Project Overview

- **Type**: Blazor WebAssembly (WASM) Application
- **Framework**: .NET 10
- **Purpose**: Client-side search interface for skillsmp.com API
- **Deployment**: GitHub Pages (Static Hosting)

## 2. Core Commands

Run these commands from the `SkillSmpQuery.Wasm` directory:

| Action | Command |
|--------|---------|
| **Build** | `dotnet build` |
| **Run** | `dotnet run` (Starts dev server at http://localhost:5xxx) |
| **Publish** | `dotnet publish -c Release -o release` |
| **Test** | *(Currently no unit test project)* |

## 3. Architecture & Structure

Follow the established Clean Architecture-lite pattern:

- **`Models/`**: Pure data structures (`record`, `sealed class`). No logic.
- **`Services/`**: Business logic and API communication (`ISkillSmpService`).
- **`Infrastructure/`**: External system adapters (`ISettingsProvider` for LocalStorage).
- **`Pages/`**: Blazor UI components (`.razor`).
- **`Shared/`**: Reusable UI layouts and components.

**Dependency Injection**:
- All services must be defined by an interface (`I...`).
- Register services in `Program.cs` using `builder.Services.AddScoped<I, T>()`.

## 4. Code Style Guidelines

### 4.1. C# Standards
- **Namespaces**: Use file-scoped namespaces (`namespace SkillSmpQuery.Wasm.Models;`).
- **Formatting**: Standard K&R braces, 4-space indentation.
- **Nullability**: Enable nullable reference types (`<Nullable>enable</Nullable>`).
- **Naming**: 
  - `PascalCase` for classes, methods, properties, public constants.
  - `_camelCase` for private fields (e.g., `_apiKey`).
  - `IPascalCase` for interfaces.

### 4.2. Models
- Prefer `sealed record` for data-transfer objects (DTOs) and immutable data.
- Use `init` properties with default values to avoid null warnings.
- **Example**:
  ```csharp
  public sealed record SkillInfo
  {
      public string Name { get; init; } = string.Empty;
  }
  ```

### 4.3. Services & Error Handling
- **Do NOT throw exceptions** for expected operational failures (e.g., API errors).
- **Use the `SearchResponse` pattern**:
  - Return an object containing `Success` (bool), `ErrorMessage` (string?), and `Results` (data).
- **Async**: Always use `async Task` and accept `CancellationToken`.

### 4.4. Blazor Components
- Use `@inject` for dependencies.
- Use `@code { ... }` block for logic.
- **State Management**:
  - Private fields for component state.
  - `OnAfterRenderAsync(bool firstRender)` for JS interop (e.g., LocalStorage access).
  - Call `StateHasChanged()` explicitly only when necessary (outside standard event lifecycle).

## 5. Documentation Rules

- **XML Comments**: Mandatory for all `public` models, interfaces, and service methods.
- **Language**: Traditional Chinese (繁體中文) or English (consistent with existing code).
- **Format**:
  ```csharp
  /// <summary>
  /// Description of what this method does.
  /// </summary>
  /// <param name="arg">Description of argument.</param>
  /// <returns>Description of return value.</returns>
  ```

## 6. Infrastructure & Storage

- **Persistence**: STRICTLY use `ISettingsProvider` wrapping `localStorage`.
- **Secrets**: API Keys are stored in the user's browser (LocalStorage).
  - Do NOT hardcode API keys.
  - Do NOT implement server-side storage logic (this is a client-side app).

## 7. Git & Deployment Workflow

- **Branching**: Push to `main` triggers GitHub Actions.
- **Base Tag**: The deployment workflow modifies `<base href="/" />` to `<base href="/skill-map/" />`.
  - **Do NOT** manually change `index.html` base tag unless the repository name changes.
- **Ignored Files**: Respect `.gitignore` (excludes `bin/`, `obj/`, `.vscode/`).

## 8. Anti-Patterns (Do Not Do)

- ❌ Do not use `Console.WriteLine` for debugging; use logging or UI feedback.
- ❌ Do not suppress warnings with `#pragma` unless absolutely necessary.
- ❌ Do not introduce server-side specific libraries (e.g., `System.IO.File` for disk access) in the WASM project.
- ❌ Do not block the UI thread with `.Wait()` or `.Result`. Use `await`.

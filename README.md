# Game339

A Unity game project with a shared C# library demonstrating observable data, dependency injection, and an MVVM-style view layer.

---

## Repository Structure

```
/
├── src/
│   ├── Shared/                  # Platform-agnostic C# library and tests
│   │   ├── Game339.Shared/      # Core library (netstandard2.1)
│   │   └── Game339.Tests/       # NUnit test project
│   └── UnityGame339/            # Unity project
│       └── Assets/Game/Runtime/ # Game scripts
└── README.md
```

---

## Shared Library — `Game339.Shared`

The shared library is organised into three areas:

### Infrastructure

Foundational building blocks used across all features.

| Type | Purpose |
|------|---------|
| `ObservableValue<T>` | Wraps a value and fires a `ChangeEvent` whenever it changes. Subscribers are called immediately on registration with the current value. |
| `IGameLog` | Logging abstraction (`Info`, `Warn`, `Error`). |
| `IMiniContainer` / `MiniContainer` | Lightweight DI container supporting singleton instances, singleton factories, and transient factories. |
| `DuplicateRegistrationException` | Thrown when the same type is registered more than once. |

### StringReverse

A simple feature that reverses a string.

| Type | Purpose |
|------|---------|
| `IStringService` | Interface with a single `Reverse(string)` method. |
| `StringService` | Implementation — reverses the characters and logs the operation. |

### Cookie

Models and service for a cookie-baking inventory system.

| Type | Purpose |
|------|---------|
| `CookieIngredient` | Enum of available ingredients: Chocolate, Nuts, PeanutButter, Butterscotch, Sugar. |
| `CookieIngredientInventory` | Holds an `ObservableValue<int>` count per ingredient. Access via `Get(ingredient)`. |
| `ICookieService` | Interface with `TryMakeCookie(string cookieName)`. |
| `CookieService` | Matches ingredient names found in the cookie name string (case-insensitive), checks stock, and deducts if all are available. Returns `false` without deducting if any ingredient is short. |

---

## Unit Tests — `Game339.Tests`

Tests mirror the shared library's folder structure:

```
Game339.Tests/
├── Infrastructure/
│   ├── EmptyGameLog.cs          # No-op IGameLog for use in tests
│   ├── MiniContainerTests.cs    # DI container behaviour
│   └── ObservableValueTests.cs  # Observable notification behaviour
├── StringReverse/
│   └── StringServiceTests.cs    # Reverse correctness, null handling
└── Cookie/
    └── CookieServiceTests.cs    # Stock deduction, partial-stock failure, case insensitivity
```

Run all tests from the `Shared/` directory:

```bash
dotnet test
```

---

## MVVM in Unity

The Unity project follows an MVVM-inspired pattern adapted for MonoBehaviour's lifecycle.

### ServiceResolver (Composition Root)

`Infrastructure/ServiceResolver.cs` is a static lazy-initialized composition root. It creates all services once and registers them in `MiniContainer`. Any script can call `ServiceResolver.Resolve<T>()` to obtain a service.

### ObserverMonoBehaviour (ViewModel)

`Infrastructure/ObserverMonoBehaviour.cs` acts as a lightweight ViewModel. Subclasses bind to model events and push updates to their serialized UI references. Unity's scene hierarchy is the View itself, so no separate View layer is needed.

It is an abstract `MonoBehaviour` base class that manages subscription lifecycle:

- `Subscribe()` is called once after `Start` and on `OnEnable` (but only after Start has run).
- `Unsubscribe()` is called on `OnDisable`.
- Subclasses implement `Subscribe` and `Unsubscribe` to wire and unwire event handlers.

This eliminates boilerplate around Unity's enable/disable cycle and ensures ViewModels never miss or double-subscribe.

### Pattern Summary

```
Model      →  ObservableValue<T> (fires ChangeEvent on change)
ViewModel  →  ObserverMonoBehaviour subclass (subscribes in Subscribe, pushes updates to UI references)
View       →  Unity scene hierarchy (GameObjects and UI components configured in the Editor)
Service    →  Registered in ServiceResolver, resolved on demand
```

---

## Examples

### String Reverse — `StringReverseScene`

Demonstrates `IStringService` wired to a UI button.

**Scripts involved:**
- `StringReverse/ReverseNameView.cs` — an `ObserverMonoBehaviour` attached to the *Reverse It* button. On click, resolves `IStringService` and sets the name label's text to the reversed string.

**Scene objects:**
- *Name* — a TMP label displaying the current name ("Sandy The Corgi").
- *Reverse It* — a button with `ReverseNameView` attached, referencing both the Button component and the Name label.

Clicking the button repeatedly toggles the name between its original and reversed forms.

---

### Cookie Shopper — `CookieShopperScene`

Demonstrates `ObservableValue<int>` driving live UI labels and `ICookieService` consuming inventory.

**Scripts involved:**
- `Cookie/CookieIngredientLabelView.cs` — an `ObserverMonoBehaviour` that subscribes to a specific ingredient's `ObservableValue<int>` via `CookieIngredientInventory.Get(ingredient).ChangeEvent`. The label updates automatically whenever the count changes.
- `Cookie/CookieButtonView.cs` — an `ObserverMonoBehaviour` attached to each cookie button. On click, resolves `ICookieService`, calls `TryMakeCookie(cookieName)`, and logs the result via `IGameLog`.

**Scene objects:**
- *Ingredient Panel* — five labels (Chocolate, Nuts, PeanutButter, Butterscotch, Sugar), each with a `CookieIngredientLabelView` and a serialized `ingredientName` field.
- *Cookie Panel* — five buttons (Chocolate Chip, PeanutButter Cookie, Butterscotch Sugar Cookie, Chocolate Nuts Brownie, Sugar Crumble), each with a `CookieButtonView` and a serialized `cookieName` field.

When a cookie button is clicked, the service checks and deducts the required ingredients, and the corresponding labels update immediately via the observable change events.

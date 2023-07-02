# HiddenImports
Библиотека для скрытого вызова Windows API функций на C#, преимущественно на C#

# Установка
Для использования вам достаточно добавить файл `HiddenImportsUtil.cs` в свой проект.

# Использование
Для вызова функции из скрытой DLL-библиотеки, следуйте следующим шагам:

- Создайте делегат, соответствующий сигнатуре вызываемой функции.

```csharp
public delegate int MyMessageBoxDelegate(IntPtr hWnd, string lpText, string lpCaption, uint uType);
```

- Объявите оригинальную функцию из Windows API

```csharp
[DllImport("user32.dll", CharSet = CharSet.Unicode)]
private static extern int MessageBoxW(IntPtr hWnd, string lpText, string lpCaption, uint uType);
```

- Вызовите функцию с помощью метода `Call`, передав название DLL-библиотеки, название функции, созданный делегат и параметры.

```csharp
var MessageBoxAv = HiddenImportsUtil.Call("user32.dll", "MessageBoxA",
    new MyMessageBoxDelegate(MessageBoxW),
    IntPtr.Zero, "Hello", "Message", 0u);
```
- Вы можете получить результат выполнения функции с помощью обращения к переменной.

```csharp
Console.WriteLine("MessageBoxA result: {0}", MessageBoxAv);
```

# Заключение

С другими примерами вы можете познакомится в файле `Examples.cs`. 

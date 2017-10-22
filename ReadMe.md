# Prism Extensions

This library provides common extensions for Prism projects using either Prism Core or Prism Forms.

[![Build status](https://avantipoint.visualstudio.com/_apis/public/build/definitions/9ae3c52d-a8d5-4184-b4fe-94f6625d7f93/30/badge)](https://avantipoint.visualstudio.com/_apis/public/build/definitions/9ae3c52d-a8d5-4184-b4fe-94f6625d7f93/30/badge)

| Package | Version |
| ------- | ------- |
| [Prism.Core.Extensions][11] | [![21]][11] |
| [Prism.Forms.Extensions][12] | [![22]][12] |

## Logging

Logging should be easy. With the Logging Extensions you can pass any object into the logger without having to explicitly call `ToString()`. By default all Exceptions passed in will be logged as Exceptions, while all other objects will be logged as Debug outputs. You can optionally override the logging Category for any object being logged.

## Prism Forms Navigation

Prism Forms users can make navigation just a little easier with the Navigation extensions.

```cs
public class TodoItem { }

// pass the item directly with the parameter key todoItem
private async void OnTodoItemTapped(TodoItem todoItem) =>
    await _navigationService.NavigateAsync("TodoItemDetail", todoItem);

// pass the item directly with a specified key
private async void OnTodoItemTapped(TodoItem todoItem) =>
    await _navigationService.NavigateAsync("TodoItemDetail", "tappedItem", todoItem);


private async void NavigateToTodoItemList() =>
    await _navigationService.NavigateAsync("TodoItemList", "todoItems", todoItem1, todoItem2, todoItem3;)

private async void NavigateToTodoItemList()
{
    var items = new List<TodoItem>()
    {
        new TodoItem(),
        new TodoItem()
    }
    // Adds the Todo Item list with the key 'todoItem'
    await _navigationService.NavigateAsync("TodoItemList", items )
}
```

[11]: https://www.nuget.org/packages/Prism.Core.Extensions
[12]: https://www.nuget.org/packages/Prism.Forms.Extensions

[21]: https://img.shields.io/nuget/vpre/Prism.Core.Extensions.svg
[22]: https://img.shields.io/nuget/vpre/Prism.Forms.Extensions.svg
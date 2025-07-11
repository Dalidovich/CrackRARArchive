# README - CrackRARArchive

## Описание проекта
Проект CrackRARArchive представляет собой инструмент для подбора паролей к RAR-архивам с использованием методов грубой силы (brute-force). Проект включает в себя как однопоточный, так и многопоточный подходы для ускорения процесса подбора паролей.

## Особенности
- Поддержка многопоточного подбора паролей для увеличения скорости работы.
- Гибкая настройка диапазона символов для подбора паролей.
- Возможность тестирования производительности с использованием тестовых паролей.
- Интеграция с библиотекой SharpCompress для работы с RAR-архивами.

## Использование
### **Основные классы**
- IteratorOnArray: Генерирует последовательности символов для подбора паролей.
- IteratorTest: Тестирует производительность подбора паролей на тестовых данных.
- ArchiveWorker: Осуществляет работу с RAR-архивами, включая попытки извлечения с разными паролями.

### Примеры
Тестирование подбора паролей
```csharp
var iteratorTest = new IteratorTest();
await iteratorTest.Tests();
```
Подбор пароля для RAR-архива
```csharp
var archiveWorker = new ArchiveWorker("", @"C:\path\to\archive.rar", @"C:\workspace\", @"C:\extract\");
await archiveWorker.WorkMultyTask(); // Многопоточный режим
archiveWorker.WorkWithSingleTask(); // Однопоточный режим
```

### Настройка параметров
- **line** в классе IteratorOnArray: строка, содержащая символы, используемые для подбора паролей.
- **CountSymbolsForPassword**: длина пароля для тестирования.
- **RarFilePath, WorkSpacePath, ExtractPath**: пути к архиву, рабочей директории и директории для извлечения файлов.

## Производительность
Проект поддерживает многопоточность, что значительно ускоряет процесс подбора паролей. Для оценки производительности можно использовать класс **`IteratorTest`**, который замеряет время подбора тестового пароля.

## Ограничения
- Эффективность подбора паролей зависит от их длины и сложности.
- Для длинных паролей процесс может занять значительное время даже в многопоточном режиме.
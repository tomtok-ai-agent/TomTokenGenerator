# TomTokenGenerator - Архитектура проекта

## Общее описание
TomTokenGenerator - это CLI-приложение на C#, которое генерирует, сериализует и записывает последовательность токенов TomToken в формате JSON. Приложение обеспечивает потоковую обработку данных без буферизации всего объема токенов в памяти.

## Компоненты архитектуры

### 1. Модель данных (Models)
- **TomToken** - базовый абстрактный класс для всех типов токенов
- **TextToken** - токен типа "текст"
- **NewLineToken** - токен типа "перевод строки"
- **RepeatToken** - токен типа "повтор другого токена N раз"
- **ReferenceToken** - токен типа "числовая ссылка на другую сущность"
- **TokenType** - перечисление типов токенов

### 2. Генератор токенов (Generators)
- **ITokenGenerator** - интерфейс для генераторов токенов
- **RandomTokenGenerator** - реализация генератора случайных токенов
- **TokenGeneratorOptions** - настройки для генератора токенов

### 3. Сериализация (Serialization)
- **ITokenSerializer** - интерфейс для сериализаторов токенов
- **JsonTokenSerializer** - реализация сериализатора токенов в JSON
- **SerializationOptions** - настройки для сериализации

### 4. Таблица трансляции (Translation)
- **TranslationTable** - класс для хранения и управления таблицей трансляции числовых ссылок
- **TranslationEntry** - запись в таблице трансляции

### 5. Метаданные (Metadata)
- **Metadata** - класс для хранения метаданных JSON

### 6. CLI (CommandLine)
- **CommandLineOptions** - класс для парсинга аргументов командной строки
- **Program** - основной класс приложения

## Потоки данных

1. Пользователь запускает приложение с аргументами командной строки
2. CommandLineOptions парсит аргументы
3. Program создает экземпляры TokenGenerator и JsonTokenSerializer
4. TokenGenerator генерирует токены по одному, без буферизации всего объема
5. JsonTokenSerializer сериализует токены в JSON в потоковом режиме
6. Результат записывается в файл или stdout в зависимости от аргументов

## Структура файлов проекта

```
TomTokenGenerator/
├── src/
│   ├── Models/
│   │   ├── TomToken.cs
│   │   ├── TextToken.cs
│   │   ├── NewLineToken.cs
│   │   ├── RepeatToken.cs
│   │   ├── ReferenceToken.cs
│   │   └── TokenType.cs
│   ├── Generators/
│   │   ├── ITokenGenerator.cs
│   │   ├── RandomTokenGenerator.cs
│   │   └── TokenGeneratorOptions.cs
│   ├── Serialization/
│   │   ├── ITokenSerializer.cs
│   │   ├── JsonTokenSerializer.cs
│   │   └── SerializationOptions.cs
│   ├── Translation/
│   │   ├── TranslationTable.cs
│   │   └── TranslationEntry.cs
│   ├── Metadata/
│   │   └── Metadata.cs
│   ├── CommandLine/
│   │   └── CommandLineOptions.cs
│   └── Program.cs
├── TomTokenGenerator.csproj
└── README.md
```

## Особенности реализации

1. **Потоковая обработка**: Использование yield return для генерации токенов и JsonTextWriter для потоковой записи JSON.
2. **Ограничение длины строк**: Контроль длины строк в JsonTextWriter.
3. **Эффективное использование памяти**: Отсутствие буферизации всего объема токенов.
4. **Модульность**: Четкое разделение ответственности между компонентами.
5. **Расширяемость**: Возможность добавления новых типов токенов и сериализаторов.

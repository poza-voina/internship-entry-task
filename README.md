# Описание проекта

Это REST API для игры в крестики-нолики, реализованное на платформе .NET

## Условия игры

1. Играют два игрока
2. Каждый третий ход существует вероятность 10%, что поставленный игроком символ будет заменён на символ противника

## Запуск
```
docker compose up -d --build
```

**Работающее приложение:** http://localhost:8080/swagger/index.html

**Для проверки работы можно воспользоваться:** [Postman](./InternshipEntryTask.Api.postman_collection.json)

**Запуск тестов:** 
```
dotnet test
```

**Запуск тестов с отчетом:**
```
dotnet test --collect:"XPlat Code Coverage"
```

```
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport"
```

## Сценарий использования
[Описание API](docs/api.md)

Создать игру:
- [POST /api/v1/game (запомнить joinKey)](https://github.com/poza-voina/internship-entry-task/blob/main/docs/api.md#post__v1_games)

Присоединиться к игре:
- [POST /api/v1/game/join (запомнить accessKey)](https://github.com/poza-voina/internship-entry-task/blob/main/docs/api.md#post__v1_games_join)  

Делать ходы:
- [POST /api/v1/game/{id}/move](https://github.com/poza-voina/internship-entry-task/blob/main/docs/api.md#post__v1_games_gameid_move)

Чтобы посмотреть на доску в виде двумерного массива нужно передавать заголовок X-ShowBoard

## Архитектура

### Набор технологий

- .NET Core
- ASP.Net Core
- Swashbuckle.AspNetCore
- Serilog
- Ef Core
- Postgres
- xunit
- FluentAssertions
- coverlet.collector
- Testcontainers

Проект построен на основе слоистой архитектуры и состоит из следующих сборок:

### 1. `InternshipEntryTask.Api` — API-слой

Отвечает за обработку HTTP-запросов и делегирование выполнения бизнес-логики слоям ниже. 
Для реализации Etag был использован MemmoryCash.

Особенности:
- Реализован глобальный обработчик исключений (`ExceptionHandler`), который преобразует исключения в `ProblemDetails`.

### 2. `InternshipEntryTask.Core` — бизнес-логика

Содержит основную игровую логику:
- Сервисы для управления состоянием игры;
- Фабрика для генерации и оценки игровой доски;
- Правила игры.

### 3. `InternshipEntryTask.Infrastructure` — инфраструктурный слой

Отвечает за работу с базой данных:
- Контекст Entity Framework Core;
- Конфигурации моделей;
- Репозитории и взаимодействие с БД.

### 3. `InternshipEntryTask.Abstractions`

Хранит константы, которые используются во всем приложении

## Тестирование

### Интеграционные тесты

Используемые библиотеки: `xUnit`, `FluentAssertions`, `Microsoft.TestPlatform`, `TestContainers`.

Проблема:
- Возникла задача изолировать данные при запуске тестов в одном Docker-контейнере, закреплённом за классом.

Решение:
- Использование отдельных схем базы данных PostgreSQL на каждый тест. Это позволило запускать тесты независимо и параллельно.

### Юнит-тесты

Библиотеки те же, за исключением `TestContainers`. Покрывает логику работы  `GameBoardEvaluator`

## ТЗ
- ✅ Создание новой игры
- ✅ Ходы двух игроков
- ✅ Фиксация победы или ничьей
- ✅ Размер поля и условия победы задаются через переменные окружения
- ✅ Платформа: .NET 9
- ✅ В репозитории есть Dockerfile (обязательно) и при необходимости docker-compose.yml
- ✅ Приложение внутри контейнера слушает порт 8080.
- ✅ На запрос GET /health отвечает 200 OK.
- ✅ Есть интеграционные и unit тесты
- ✅ Минимальное покрытие кода — 30 %.
- ✅ Запуск тестов в CI и локально — dotnet test (или аналогичная команда).
- ✅ При гонке двух POST /moves с одинаковым телом второй запрос обязан вернуть 200 OK и тот же ETag.
- ✅ После перезапуска контейнера мы выполняем GET /games/{id} и делаем следующий ход. Сессия игрока сохраняется.
- ✅ Любой некорректный JSON (пример ниже) → HTTP 400 (RFC 7807)

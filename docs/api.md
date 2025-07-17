<!-- Generator: Widdershins v4.0.1 -->

<h1 id="internshipentrytask-api">InternshipEntryTask.Api v1.0</h1>

> Scroll down for code samples, example requests and responses. Select a language for code samples from the tabs above or the mobile navigation menu.

<h1 id="internshipentrytask-api-game">Game</h1>

## get__v1_games_{gameId}

> Code samples

`GET /v1/games/{gameId}`

*Получает игру по идентификатору*

<h3 id="get__v1_games_{gameid}-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|gameId|path|integer(int64)|true|Идентификатор игры|
|Show-Board|header|boolean|false|Флаг определяющий, следует ли отображать игровую доску|

> Example responses

> 200 Response

```
{"id":0,"nextMove":"X","status":"Progress","winner":"X","width":0,"height":0,"joinKey":"21319e90-c907-491f-b6b6-13c537b1c96e","accessKey":"b89f830e-b9a9-4bfa-bbd4-ee4bca116370","board":[["string"]],"moves":[{"row":0,"column":0,"cellValue":"X"}]}
```

```json
{
  "id": 0,
  "nextMove": "X",
  "status": "Progress",
  "winner": "X",
  "width": 0,
  "height": 0,
  "joinKey": "21319e90-c907-491f-b6b6-13c537b1c96e",
  "accessKey": "b89f830e-b9a9-4bfa-bbd4-ee4bca116370",
  "board": [
    [
      "string"
    ]
  ],
  "moves": [
    {
      "row": 0,
      "column": 0,
      "cellValue": "X"
    }
  ]
}
```

<h3 id="get__v1_games_{gameid}-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[GameDto](#schemagamedto)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|

<aside class="success">
This operation does not require authentication
</aside>

## post__v1_games

> Code samples

`POST /v1/games`

*Создает игру*

> Example responses

> 200 Response

```
{"id":0,"nextMove":"X","status":"Progress","winner":"X","width":0,"height":0,"joinKey":"21319e90-c907-491f-b6b6-13c537b1c96e","accessKey":"b89f830e-b9a9-4bfa-bbd4-ee4bca116370","board":[["string"]],"moves":[{"row":0,"column":0,"cellValue":"X"}]}
```

```json
{
  "id": 0,
  "nextMove": "X",
  "status": "Progress",
  "winner": "X",
  "width": 0,
  "height": 0,
  "joinKey": "21319e90-c907-491f-b6b6-13c537b1c96e",
  "accessKey": "b89f830e-b9a9-4bfa-bbd4-ee4bca116370",
  "board": [
    [
      "string"
    ]
  ],
  "moves": [
    {
      "row": 0,
      "column": 0,
      "cellValue": "X"
    }
  ]
}
```

<h3 id="post__v1_games-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[GameDto](#schemagamedto)|

<aside class="success">
This operation does not require authentication
</aside>

## post__v1_games_join

> Code samples

`POST /v1/games/join`

*Присоединиться к игре*

> Body parameter

```json
{
  "playerSymbol": "X"
}
```

<h3 id="post__v1_games_join-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|X-Join-Key|header|string(uuid)|true|Ключ, используемый для присоедининения к игре|
|body|body|[JoinRequest](#schemajoinrequest)|false|Запрос на присоединени|

> Example responses

> 200 Response

```
{"id":0,"nextMove":"X","status":"Progress","winner":"X","width":0,"height":0,"joinKey":"21319e90-c907-491f-b6b6-13c537b1c96e","accessKey":"b89f830e-b9a9-4bfa-bbd4-ee4bca116370","board":[["string"]],"moves":[{"row":0,"column":0,"cellValue":"X"}]}
```

```json
{
  "id": 0,
  "nextMove": "X",
  "status": "Progress",
  "winner": "X",
  "width": 0,
  "height": 0,
  "joinKey": "21319e90-c907-491f-b6b6-13c537b1c96e",
  "accessKey": "b89f830e-b9a9-4bfa-bbd4-ee4bca116370",
  "board": [
    [
      "string"
    ]
  ],
  "moves": [
    {
      "row": 0,
      "column": 0,
      "cellValue": "X"
    }
  ]
}
```

<h3 id="post__v1_games_join-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[GameDto](#schemagamedto)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|
|409|[Conflict](https://tools.ietf.org/html/rfc7231#section-6.5.8)|Conflict|[ProblemDetails](#schemaproblemdetails)|

<aside class="success">
This operation does not require authentication
</aside>

## post__v1_games_{gameId}_move

> Code samples

`POST /v1/games/{gameId}/move`

*Сделать ход*

> Body parameter

```json
{
  "row": 0,
  "column": 0
}
```

<h3 id="post__v1_games_{gameid}_move-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|gameId|path|integer(int64)|true|Идентификатор игры|
|X-Access-Key|header|string(uuid)|true|Ключ авторизации|
|Show-Board|header|boolean|false|Флаг определяющий, следует ли отображать игровую доску|
|body|body|[MoveRequest](#schemamoverequest)|false|Запрос на создание хода|

> Example responses

> 200 Response

```
{"id":0,"nextMove":"X","status":"Progress","winner":"X","width":0,"height":0,"joinKey":"21319e90-c907-491f-b6b6-13c537b1c96e","accessKey":"b89f830e-b9a9-4bfa-bbd4-ee4bca116370","board":[["string"]],"moves":[{"row":0,"column":0,"cellValue":"X"}]}
```

```json
{
  "id": 0,
  "nextMove": "X",
  "status": "Progress",
  "winner": "X",
  "width": 0,
  "height": 0,
  "joinKey": "21319e90-c907-491f-b6b6-13c537b1c96e",
  "accessKey": "b89f830e-b9a9-4bfa-bbd4-ee4bca116370",
  "board": [
    [
      "string"
    ]
  ],
  "moves": [
    {
      "row": 0,
      "column": 0,
      "cellValue": "X"
    }
  ]
}
```

<h3 id="post__v1_games_{gameid}_move-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[GameDto](#schemagamedto)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ProblemDetails](#schemaproblemdetails)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[ProblemDetails](#schemaproblemdetails)|
|409|[Conflict](https://tools.ietf.org/html/rfc7231#section-6.5.8)|Conflict|[ProblemDetails](#schemaproblemdetails)|
|422|[Unprocessable Entity](https://tools.ietf.org/html/rfc2518#section-10.3)|Unprocessable Content|[ProblemDetails](#schemaproblemdetails)|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="internshipentrytask-api-healthcheck">HealthCheck</h1>

## get__health

> Code samples

`GET /health`

*Проверяет состояние работоспособности API.*

<h3 id="get__health-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

<aside class="success">
This operation does not require authentication
</aside>

# Schemas

<h2 id="tocS_CellValue">CellValue</h2>
<!-- backwards compatibility -->
<a id="schemacellvalue"></a>
<a id="schema_CellValue"></a>
<a id="tocScellvalue"></a>
<a id="tocscellvalue"></a>

```json
"X"

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|string|false|none|none|

#### Enumerated Values

|Property|Value|
|---|---|
|*anonymous*|X|
|*anonymous*|O|

<h2 id="tocS_GameDto">GameDto</h2>
<!-- backwards compatibility -->
<a id="schemagamedto"></a>
<a id="schema_GameDto"></a>
<a id="tocSgamedto"></a>
<a id="tocsgamedto"></a>

```json
{
  "id": 0,
  "nextMove": "X",
  "status": "Progress",
  "winner": "X",
  "width": 0,
  "height": 0,
  "joinKey": "21319e90-c907-491f-b6b6-13c537b1c96e",
  "accessKey": "b89f830e-b9a9-4bfa-bbd4-ee4bca116370",
  "board": [
    [
      "string"
    ]
  ],
  "moves": [
    {
      "row": 0,
      "column": 0,
      "cellValue": "X"
    }
  ]
}

```

Модель DTO "Игра"

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int64)|false|none|Идентификатор игры|
|nextMove|[CellValue](#schemacellvalue)|false|none|none|
|status|[GameStatus](#schemagamestatus)|false|none|none|
|winner|[WinnerStatus](#schemawinnerstatus)|false|none|none|
|width|integer(int32)|false|none|Ширина поля|
|height|integer(int32)|false|none|Высота поля|
|joinKey|string(uuid)¦null|false|none|Ключ для присоединия к игре|
|accessKey|string(uuid)¦null|false|none|Ключ авторизации игрока|
|board|[array]¦null|false|none|Доска|
|moves|[[MoveDto](#schemamovedto)]¦null|false|none|Список ходов|

<h2 id="tocS_GameStatus">GameStatus</h2>
<!-- backwards compatibility -->
<a id="schemagamestatus"></a>
<a id="schema_GameStatus"></a>
<a id="tocSgamestatus"></a>
<a id="tocsgamestatus"></a>

```json
"Progress"

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|string|false|none|none|

#### Enumerated Values

|Property|Value|
|---|---|
|*anonymous*|Progress|
|*anonymous*|Finished|

<h2 id="tocS_JoinRequest">JoinRequest</h2>
<!-- backwards compatibility -->
<a id="schemajoinrequest"></a>
<a id="schema_JoinRequest"></a>
<a id="tocSjoinrequest"></a>
<a id="tocsjoinrequest"></a>

```json
{
  "playerSymbol": "X"
}

```

Модель запроса на присоединение к игре

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|playerSymbol|[CellValue](#schemacellvalue)|true|none|none|

<h2 id="tocS_MoveDto">MoveDto</h2>
<!-- backwards compatibility -->
<a id="schemamovedto"></a>
<a id="schema_MoveDto"></a>
<a id="tocSmovedto"></a>
<a id="tocsmovedto"></a>

```json
{
  "row": 0,
  "column": 0,
  "cellValue": "X"
}

```

Модель DTO "Ход"

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|row|integer(int32)|false|none|Индекс строки хода|
|column|integer(int32)|false|none|Индекс колонки хода|
|cellValue|[CellValue](#schemacellvalue)|false|none|none|

<h2 id="tocS_MoveRequest">MoveRequest</h2>
<!-- backwards compatibility -->
<a id="schemamoverequest"></a>
<a id="schema_MoveRequest"></a>
<a id="tocSmoverequest"></a>
<a id="tocsmoverequest"></a>

```json
{
  "row": 0,
  "column": 0
}

```

Запрос на ход

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|row|integer(int32)|false|none|Индекс строки хода|
|column|integer(int32)|false|none|Индекс колонки хода|

<h2 id="tocS_ProblemDetails">ProblemDetails</h2>
<!-- backwards compatibility -->
<a id="schemaproblemdetails"></a>
<a id="schema_ProblemDetails"></a>
<a id="tocSproblemdetails"></a>
<a id="tocsproblemdetails"></a>

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string",
  "property1": null,
  "property2": null
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|**additionalProperties**|any|false|none|none|
|type|string¦null|false|none|none|
|title|string¦null|false|none|none|
|status|integer(int32)¦null|false|none|none|
|detail|string¦null|false|none|none|
|instance|string¦null|false|none|none|

<h2 id="tocS_WinnerStatus">WinnerStatus</h2>
<!-- backwards compatibility -->
<a id="schemawinnerstatus"></a>
<a id="schema_WinnerStatus"></a>
<a id="tocSwinnerstatus"></a>
<a id="tocswinnerstatus"></a>

```json
"X"

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|string|false|none|none|

#### Enumerated Values

|Property|Value|
|---|---|
|*anonymous*|X|
|*anonymous*|O|
|*anonymous*|Draw|


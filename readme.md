# Diff API

## Concerns to the specification
1. Spec uses specific HTTP verbs instead of general requirements
2. Immutability of inputs is questionable (POST can't mutate resources)

## Decisions
1. As per specification, the POST methods are specified to create records. Since POST is not idempotent and must return 409 in case of duplicate record (same ID), I considered records as immutable. To mutate the records, the PUT/PATCH methods would be implemented
2. Since excessive documentation in code is a sign of bad code, I decided to comment only parts that might not be easily understandable from the code itself
3. I did not implemented any functional additions on top of the current spec, I would comment on the design first

## Data
I decided to output all found differences with offsets and diffed characters so that required information can be inferred from the collection on client side

```JSON
{
  "$schema": "http://json-schema.org/draft-04/schema#",
  "type": "object",
  "properties": {
    "id": {
      "type": "string"
    },
    "description": {
      "type": "string"
    },
    "differences": {
      "type": "array",
      "items": [
        {
          "type": "object",
          "properties": {
            "index": {
              "type": "integer"
            },
            "leftCharacter": {
              "type": "string"
            },
            "rightCharacter": {
              "type": "string"
            }
          },
          "required": [
            "index",
            "leftCharacter",
            "rightCharacter"
          ]
        },
      ]
    }
  },
  "required": [
    "id",
    "description",
    "differences"
  ]
}
```

## Unit Tests
1. I prefer unit tests to be verbose and de-normalized, so they can be read as a use-case description without referring to various utility functions
2. `[DataRow()]` attribute has a bug that doesn't where it doesn't support enum values, the solution would be use more sophisticated test framework such as *xUnit*
3. Test coverage should be implemented to meet 100% coverage

## Integration Tests
There is an integration test in `diffApiIntegrationTest`. It requires the web api to be running.
Integration test can be executed only once while the web api is running because posting records with the same id causes HTTP conflict error. Ideally with .NET 7, it can be rewritten to memory hosting.
Integration test tests only the happy path.

## Manual Test
When running the API locally, go to https://localhost:7232/swagger to play with the API in generated client. Since Swagger can't understand the custom formatter for content types, the body for `POST` endpoints must be converted to base64, rather than plain as Swagger suggests.

```BASH
# left value for 999 is {"input":"testValue"}
curl -X 'POST' \
  'https://localhost:7232/v1/Diff/999/left' \
  -H 'accept: */*' \
  -H 'Content-Type: application/custom' \
  -d 'eyJpbnB1dCI6InRlc3RWYWx1ZSJ9'

# right value for 999 is {"input":"tesXYalue"}
curl -X 'POST' \
  'https://localhost:7232/v1/Diff/999/right' \
  -H 'accept: */*' \
  -H 'Content-Type: application/custom' \
  -d 'eyJpbnB1dCI6InRlc1hZYWx1ZSJ9'

## result
curl -X 'GET' \
  'https://localhost:7232/v1/Diff/999' \
  -H 'accept: */*'
```

```JSON
{
  "id": "999",
  "description": "",
  "differences": [
    {
      "index": 3,
      "leftCharacter": "t",
      "rightCharacter": "X"
    },
    {
      "index": 4,
      "leftCharacter": "V",
      "rightCharacter": "Y"
    }
  ]
}
```

## Limitation
- Only naive storage implementation is provided, interface allows to upgrade to the proper solution without changing the service and controller. In memory storage is great for testing locally without any dependencies.
- All cross-cutting concerns are omitted (authentication, authorization, logging, telemetry, perf tests etc.)

## Custom content type caveat
Since the requirement is to use base64 encoded JSON and `application/custom` Content-Type, the endpoint returns 400 unsupported media type in case the data in body is not base64 formatted JSON with specific format. The custom media type format is implemented in `CustomFormatter.cs`. Because the conversion is provided by the formatter which is set as attribute on the actions, the controller unit tests are different than actual data sent from the HTTP client (raw JSON vs encoded JSON).

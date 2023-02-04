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

## Limitation
- Only naive storage implementation is provided, interface allows to upgrade to the proper solution without changing the service and controller. In memory storage is great for testing locally without any dependencies.
- All cross-cutting concerns are omitted (atuhentication, authorization, logging, telemetry etc.)

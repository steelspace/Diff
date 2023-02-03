# Diff API

## Concerns to the specification
1. Spec uses specific HTTP verbs instead of general requirements
2. 

## Decisions
1. As per specification, the POST methods are specified to create records. Since POST is not idempotent and must return 409 in case of duplicate record (same ID), I considered records as immutable. To mutate the records, the PUT/PATCH methods would be implemented.
2. Since excessive documentation in code is a sign of bad code, I decided to comment only parts that might not be easily understandable from the code itself.

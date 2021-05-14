Your Solution Documentation
===========================
# Architecture
I chose the "Onion" architecture which has the Business Domain at the centre of the Solution. All other Layers depend inwards on the Business Domain.

#### Domain Layer
This layer is designed based on DDD principles It is not dependent on any other Layers. It contains the following types of classes:
* Business Entities
* Domain Events for communication between bounded contexts
* Value objects
#### Application Layer
This layer contains concepts that the application uses to communicate between it's layers that are not directly related to the business domain. This layer is only dependent on the Domain Layer. It contains:
* CQRS Queries and QueryHandlers
* CQRS Commands and Command Handlers
* DTO models used in the API responses
* Interfaces for repositories (To be implemented in Application Layer)

#### Infrastructure Layer
This layer is responsible for the concrete implementation of DBContext and is dependent on MySQL. It contains:
* Repository Implementation
* DBContext Implementation
#### WebApi Layer
That layer is responsible for serving clients over Http. It contains:
* WebApi Controllers
* Packaged React SPA
#### UI Layer
React SPA (Single Page Application) that is based on create-react-app


# Running
From the root of the project, run `docker-compose up -d`
* You should now have the UI running at http://localhost:8090

# Notes
Given more time, I would have done a few things differently:
### Repository
I would adjust the implementation of my repository to be more elegant. Changes I might make:
* Use a generic repository that works with AggregateRoutes instead of Lead
* Pass predicates as parameters
* Perform server side evaluation of enums.
* Use Enumerator classes instead of enums

### Tests
I would build more tests to attain more coverage.
I would also try to follow a more structure approach to testing and group the tests by Unit, Component and Integration

The current tests are just a small set I used while developing in a TDD approach as far as practical.

### React App Refactoring
The React App can do with some refactoring as there is a bit of duplication between the Invited and Accepted components

### Security
I did not implement any form of authentication or authorization. Both of these would need to be looked at in detail before the system can be productionised.

### Logging
I only relied on basic console logging from the WebApi. I would introduce additional explicit logging in the other layers given more time.